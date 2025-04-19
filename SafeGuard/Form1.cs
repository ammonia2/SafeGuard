using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO.Compression;
using Microsoft.Data.SqlClient;

namespace SafeGuard
{
    public partial class Form1 : Form
    {
        private string connectionString = "";

        public Form1()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            EnsureFileTableExists();
            EnsureEncryptedTableExists();
            EnsureCompressedFilesTableExists(); // *** ADD THIS LINE ***

            decryptionMethodSelection.Items.Clear();
            decryptionMethodSelection.Items.Add("AES");
            decryptionMethodSelection.Items.Add("Pixel Scrambling");

            LoadRecentFiles();
            fileSelectionEncryption.MaxDropDownItems = 5;
            fileSelectionEncryption.DropDownHeight = 100;

            // *** ADD "Compressed Files" TO THE DROPDOWN ***
            // Do this *before* setting the event handler if you want the initial load to work correctly
            cmbTables.Items.Clear(); // Clear any design-time items
            cmbTables.Items.Add("All Files");
            cmbTables.Items.Add("Encrypted Files");
            cmbTables.Items.Add("Compressed Files"); // Add the new option

            cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged; // event handler
                                                                              // Consider setting a default index *after* adding items if you cleared them
                                                                              // cmbTables.SelectedIndex = 0; // If you want "All Files" selected initially
        }

        private void EnsureFileTableExists()
        {
            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS `files` (
                    `file_id` INT AUTO_INCREMENT PRIMARY KEY,
                    `file_name` VARCHAR(255) NOT NULL,
                    `file_size` FLOAT,
                    `file_type` VARCHAR(50),
                    `file_path` VARCHAR(500),
                    `uploaded_on` DATETIME DEFAULT CURRENT_TIMESTAMP
                );";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(createTableSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EnsureEncryptedTableExists()
        {
            string createTableSql = @"
            CREATE TABLE IF NOT EXISTS `encrypted` (
              `encrypted_id` INT AUTO_INCREMENT PRIMARY KEY,
              `file_id` INT NOT NULL,
              `encryption_key` VARCHAR(255) NOT NULL,
              `encrypted_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
              `original_file_id` INT NOT NULL,
              FOREIGN KEY (`file_id`) REFERENCES `files` (`file_id`)
            );";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(createTableSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EnsureCompressedFilesTableExists()
        {
            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS `compressed_files` (
                  `compressed_file_id` INT AUTO_INCREMENT PRIMARY KEY,
                  `file_id` INT NOT NULL,               -- Foreign key to the 'files' table entry for the COMPRESSED file
                  `original_file_id` INT NOT NULL,      -- Foreign key to the 'files' table entry for the ORIGINAL file
                  `compressed_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                  FOREIGN KEY (`file_id`) REFERENCES `files` (`file_id`) ON DELETE CASCADE,          -- Optional: If compressed file record is deleted, delete this link too
                  FOREIGN KEY (`original_file_id`) REFERENCES `files` (`file_id`) ON DELETE CASCADE   -- Optional: If original file record is deleted, delete this link too
                );";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(createTableSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a file to upload";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Display info in the panel
                    FileInfo fi = new FileInfo(ofd.FileName);
                    double fileSizeMB = Math.Round(fi.Length / (1024.0 * 1024.0), 2);
                    string fileExtension = fi.Extension;


                    // Insert into database
                    InsertFileRecord(ofd.FileName, fileSizeMB, fileExtension);

                    // Refresh recent files
                    LoadRecentFiles();
                }
            }
        }

        private void InsertFileRecord(string filePath, double size, string extension)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO files (file_name, file_size, file_type, file_path) " +
                               "VALUES (@name, @size, @type, @path)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", Path.GetFileName(filePath));
                    cmd.Parameters.AddWithValue("@size", size);
                    cmd.Parameters.AddWithValue("@type", extension);
                    cmd.Parameters.AddWithValue("@path", filePath);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadRecentFiles()
        {
            flowLayoutPanelRecentFiles.Controls.Clear(); // Clear old items

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT file_name, file_size, file_type FROM files ORDER BY uploaded_on DESC LIMIT 3";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fName = reader.GetString("file_name");
                            double fSize = reader.GetDouble("file_size");
                            string fType = reader.GetString("file_type");

                            // Create a panel or user control for each file item
                            Panel filePanel = CreateFilePanel(fName, fSize, fType);
                            flowLayoutPanelRecentFiles.Controls.Add(filePanel);
                        }
                    }
                }
            }
        }

        private void LoadAllFiles()
        {
            // which table is selected
            if (cmbTables.SelectedItem == null) return; // Prevent error if nothing is selected
            string selectedTable = cmbTables.SelectedItem.ToString();

            string query = "";
            if (selectedTable == "All Files")
            {
                query = @"SELECT
                     file_id,
                     file_name,
                     file_size,
                     file_type,
                     file_path,
                     uploaded_on
                  FROM files
                  ORDER BY uploaded_on DESC";
            }
            else if (selectedTable == "Encrypted Files")
            {
                // Join to get the encrypted file's name and path
                query = @"SELECT
                     e.encrypted_id,
                     e.file_id AS encrypted_entry_file_id, -- ID in files table for the encrypted file
                     f.file_name AS encrypted_file_name,
                     f.file_path AS encrypted_file_path,
                     e.encryption_key,
                     e.encrypted_on,
                     e.original_file_id
                  FROM encrypted e
                  JOIN files f ON e.file_id = f.file_id
                  ORDER BY e.encrypted_on DESC"; // Order by when it was encrypted
            }
            // *** ADD THIS ELSE IF BLOCK ***
            else if (selectedTable == "Compressed Files")
            {
                // Join to get details about the compressed file and the original file
                query = @"SELECT
                     cf.compressed_file_id,
                     cf.file_id AS compressed_entry_file_id, -- ID in files table for the compressed file
                     f_comp.file_name AS compressed_file_name,
                     f_comp.file_size AS compressed_file_size,
                     f_comp.file_path AS compressed_file_path,
                     cf.original_file_id,
                     f_orig.file_name AS original_file_name, -- Get original name for reference
                     cf.compressed_on
                  FROM compressed_files cf
                  JOIN files f_comp ON cf.file_id = f_comp.file_id           -- Join to get compressed file details
                  JOIN files f_orig ON cf.original_file_id = f_orig.file_id   -- Join to get original file details
                  ORDER BY cf.compressed_on DESC"; // Order by when it was compressed
            }
            else
            {
                // Optional: Handle unexpected selection or clear the grid
                dataGridViewFiles.DataSource = null;
                return;
            }


            // display in DataGridView
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try // Add try-catch around Fill in case the query is bad
                    {
                        adapter.Fill(dt);
                        dataGridViewFiles.DataSource = dt;

                        // Optional: Auto-size columns for better viewing
                        dataGridViewFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error loading data: {ex.Message}\nQuery: {query}");
                        dataGridViewFiles.DataSource = null; // Clear grid on error
                    }

                }
            }
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllFiles();
        }

        private Panel CreateFilePanel(string fileName, double fileSize, string fileType)
        {
            Panel filePanel = new Panel
            {
                BackColor = Color.LightBlue,
                Margin = new Padding(15, 0, 15, 0),
                Size = new Size(110, 60)
            };

            Label lblFileName = new Label();
            lblFileName.Text = fileName;
            lblFileName.Location = new Point(5, 10);
            lblFileName.AutoSize = true;

            Label lblFileSize = new Label();
            lblFileSize.Text = fileSize.ToString("F2") + " MB";
            lblFileSize.Location = new Point(5, 25);
            lblFileSize.AutoSize = true;

            filePanel.Controls.Add(lblFileName);
            filePanel.Controls.Add(lblFileSize);

            return filePanel;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelContent.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;

            panelEncryptionSettings.Visible = true;
            compressionPanel.Visible = false;    // Added
            decompressionPanel.Visible = false;  // Added
            encryptionMethodSelection.SelectedIndex = 0;
            LoadImageFileNames();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;    // Added
            decompressionPanel.Visible = false;  // Added

            panelContent.Visible = true; // Show Home panel
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;    // Added
            decompressionPanel.Visible = false;  // Added
            decryptionPanel.Visible = true;
            LoadEncryptedFileNames();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            compressionPanel.Visible = false;    // Added
            decompressionPanel.Visible = false;  // Added

            cmbTables.SelectedIndex = 0; // Reset selection
            panelFileManagement.Visible = true; // Show File Management panel
            LoadAllFiles(); // Load data for the selected table
        }



        // Implement the link click handler
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Hide all other panels
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            decompressionPanel.Visible = false;  // Added

            // Show compression panel and load files
            compressionPanel.Visible = true;
            LoadFilesForCompression();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Hide all other panels
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;

            // Show decompression panel
            decompressionPanel.Visible = true;
            LoadCompressedFilesForDecompression();
        }

        private void LoadCompressedFilesForDecompression()
        {
            fileSelectionDecompression.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Join to get details about compressed files
                string query = @"
    SELECT cf.compressed_file_id, cf.file_id, f.file_name 
    FROM compressed_files cf
    JOIN files f ON cf.file_id = f.file_id
    ORDER BY cf.compressed_on DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int compressedFileId = reader.GetInt32("compressed_file_id");
                        int fileId = reader.GetInt32("file_id");
                        string fileName = reader.GetString("file_name");

                        // Add to dropdown
                        fileSelectionDecompression.Items.Add(new CompressionComboboxItem
                        {
                            Text = fileName,
                            FileId = fileId,
                            CompressedFileId = compressedFileId
                        });
                    }
                }
            }

            fileSelectionDecompression.MaxDropDownItems = 5;
            fileSelectionDecompression.DropDownHeight = 100;
        }

        public class CompressionComboboxItem
        {
            public string Text { get; set; }
            public int FileId { get; set; }
            public int CompressedFileId { get; set; }

            public override string ToString() => Text; // ensures ComboBox displays Text
        }

        private void decompressButton_Click(object sender, EventArgs e)
        {
            if (fileSelectionDecompression.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a compressed file to decompress first.");
                return;
            }

            CompressionComboboxItem selectedItem = (CompressionComboboxItem)fileSelectionDecompression.SelectedItem;
            int compressedFileId = selectedItem.CompressedFileId;
            int fileId = selectedItem.FileId;

            // Get paths and original file info
            string compressedFilePath = GetFilePathFromDB(fileId);
            int originalFileId = GetOriginalFileIdForCompressed(compressedFileId);
            string originalFileName = GetFileNameFromDB(originalFileId) ?? "OriginalFile";
            string originalExtension = GetOriginalFileExtension(originalFileId);

            if (string.IsNullOrEmpty(compressedFilePath) || !File.Exists(compressedFilePath))
            {
                MessageBox.Show("Compressed file not found on disk!");
                return;
            }

            // Prompt user to choose save location
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Decompressed File";
                string filterExt = (originalExtension?.TrimStart('.') ?? "dat").ToUpper();
                sfd.Filter = $"{filterExt} files (*{originalExtension})|*{originalExtension}|All Files (*.*)|*.*";
                sfd.FileName = Path.GetFileNameWithoutExtension(originalFileName) + "_decompressed" + originalExtension;
                sfd.DefaultExt = originalExtension?.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Decompress file
                        DecompressImage(compressedFilePath, sfd.FileName);

                        // Insert decompressed file record into database
                        InsertDecompressedFileRecord(sfd.FileName, originalFileId);

                        MessageBox.Show("File decompressed successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Decompression failed: {ex.Message}");
                    }
                }
            }
        }

        private void DecompressImage(string compressedFilePath, string outputFilePath)
        {
            // For images, "decompression" is essentially opening the compressed file and 
            // saving it in its original/requested format at original/higher quality
            using (Bitmap compressedBitmap = new Bitmap(compressedFilePath))
            {
                // Determine the target format based on the destination extension
                string destExtension = Path.GetExtension(outputFilePath).ToLower();
                ImageFormat format;

                switch (destExtension)
                {
                    case ".jpg":
                    case ".jpeg":
                        // For JPEG, use high quality when decompressing
                        using (EncoderParameters encoderParams = new EncoderParameters(1))
                        {
                            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L); // Maximum quality
                            ImageCodecInfo jpegEncoder = GetEncoder(ImageFormat.Jpeg);

                            if (jpegEncoder != null)
                            {
                                compressedBitmap.Save(outputFilePath, jpegEncoder, encoderParams);
                                return;
                            }
                        }
                        format = ImageFormat.Jpeg;
                        break;

                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;

                    case ".png":
                        format = ImageFormat.Png;
                        break;

                    case ".gif":
                        format = ImageFormat.Gif;
                        break;

                    default:
                        format = ImageFormat.Png; // Default to PNG
                        break;
                }

                // Save with the determined format
                compressedBitmap.Save(outputFilePath, format);
            }
        }

        private int GetOriginalFileIdForCompressed(int compressedFileId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT original_file_id FROM compressed_files WHERE compressed_file_id = @cfId LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cfId", compressedFileId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return -1; // Default value if not found
        }

        private void InsertDecompressedFileRecord(string decompressedFilePath, int originalFileId)
        {
            FileInfo fi = new FileInfo(decompressedFilePath);
            double fileSizeMB = Math.Round(fi.Length / (1024.0 * 1024.0), 2);
            string fileName = fi.Name;
            string extension = fi.Extension;

            // Insert into files table
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
    INSERT INTO files (file_name, file_size, file_type, file_path, uploaded_on)
    VALUES (@name, @size, @type, @path, NOW())";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", fileName);
                    cmd.Parameters.AddWithValue("@size", fileSizeMB);
                    cmd.Parameters.AddWithValue("@type", extension);
                    cmd.Parameters.AddWithValue("@path", decompressedFilePath);
                    cmd.ExecuteNonQuery();
                }

                // Optionally - add a record to a decompressed_files table if you want to track decompression history
            }
        }



        // Load files into the compression dropdown
        // Replace the LoadFilesForCompression method
        // Replace the LoadFilesForCompression method
        private void LoadFilesForCompression()
        {
            fileSelectionCompression.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // *** ADD .bmp HERE ***
                string query = "SELECT file_id, file_name FROM files " +
                               "WHERE file_type IN ('.png','.jpg','.jpeg', '.bmp')"; // Added '.bmp'
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int fileId = reader.GetInt32("file_id");
                        string fileName = reader.GetString("file_name");

                        // Adding to dropdown
                        fileSelectionCompression.Items.Add(new ComboboxItem
                        {
                            Text = fileName,
                            Value = fileId
                        });
                    }
                }
            }
            fileSelectionCompression.MaxDropDownItems = 5;
            fileSelectionCompression.DropDownHeight = 100;
        }

        // Replace the compressButton_Click handler
        // Replace the compressButton_Click handler
        private void compressButton_Click(object sender, EventArgs e)
        {
            if (fileSelectionCompression.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an image file to optimize first.");
                return;
            }

            ComboboxItem selectedItem = (ComboboxItem)fileSelectionCompression.SelectedItem;
            int fileId = (int)selectedItem.Value;

            string filePath = GetFilePathFromDB(fileId);
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("File not found on disk!");
                return;
            }

            // Prompt user to choose save location for compressed file
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Optimized Image";

                // Get original file extension
                string originalExtension = Path.GetExtension(filePath).ToLower();
                string originalFileName = Path.GetFileNameWithoutExtension(filePath);

                // *** ADDED BMP HANDLING HERE ***
                if (originalExtension == ".bmp")
                {
                    // For BMP, suggest PNG for lossless compression, offer JPG for lossy, or keep BMP
                    sfd.Filter = "PNG Image (Lossless)|*.png|JPEG Image (Lossy)|*.jpg|BMP Image (Uncompressed)|*.bmp|All Files|*.*";
                    sfd.DefaultExt = "png"; // Recommend PNG as the best "compression" for BMP
                    sfd.FileName = originalFileName + "_optimized.png"; // Suggest PNG filename
                }
                else if (originalExtension == ".jpg" || originalExtension == ".jpeg")
                {
                    sfd.Filter = "JPEG Image|*.jpg|All Files|*.*";
                    sfd.DefaultExt = "jpg";
                    sfd.FileName = originalFileName + "_optimized.jpg";
                }
                else // Assuming PNG if not JPG/BMP
                {
                    sfd.Filter = "PNG Image|*.png|All Files|*.*";
                    sfd.DefaultExt = "png";
                    sfd.FileName = originalFileName + "_optimized.png";
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Apply compression based on the *destination* format chosen in SaveFileDialog
                        // The originalExtension is less relevant here now, CompressImage handles the target format
                        CompressImage(filePath, sfd.FileName, originalExtension); // Pass originalExtension for context if needed, but destination drives the save format

                        // Insert the compressed file record into the database
                        InsertCompressedFileRecord(sfd.FileName, fileId); // Pass originalFileId for tracking if needed

                        MessageBox.Show("Image optimized and saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Optimization failed: {ex.Message}");
                    }
                }
            }
        }
        private void CompressImage(string sourceFile, string destinationFile, string originalExtension) // originalExtension might be less critical now
        {
            // Load the original image
            using (Bitmap originalBitmap = new Bitmap(sourceFile))
            {
                string destinationExtension = Path.GetExtension(destinationFile).ToLower();

                if (destinationExtension == ".jpg" || destinationExtension == ".jpeg")
                {
                    // --- Save as JPEG (Lossy) ---
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 85L); // Quality 85
                    ImageCodecInfo jpegEncoder = GetEncoder(ImageFormat.Jpeg);

                    if (jpegEncoder != null)
                    {
                        originalBitmap.Save(destinationFile, jpegEncoder, encoderParams);
                    }
                    else
                    {
                        // Fallback if encoder somehow not found
                        originalBitmap.Save(destinationFile, ImageFormat.Jpeg);
                    }
                }
                else if (destinationExtension == ".png")
                {
                    // --- Save as PNG (Lossless) ---
                    // Use high compression effort for PNG.
                    // Note: This is lossless, so size reduction depends on original file's optimization level.
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    // Setting Compression level (like 9L) might not directly map as expected via standard enums.
                    // Often, simply saving as PNG uses a reasonable default compression.
                    // Let's try setting the parameter for potentially better (but slower) compression effort.
                    // If this causes issues or doesn't compile correctly with your .NET version/setup,
                    // you might revert to just: originalBitmap.Save(destinationFile, ImageFormat.Png);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, 9L); // Use 9L for max effort (if supported)

                    ImageCodecInfo pngEncoder = GetEncoder(ImageFormat.Png);
                    if (pngEncoder != null)
                    {
                        originalBitmap.Save(destinationFile, pngEncoder, encoderParams);
                    }
                    else
                    {
                        originalBitmap.Save(destinationFile, ImageFormat.Png); // Fallback
                    }
                }
                else if (destinationExtension == ".bmp")
                {
                    // --- Save as BMP (Uncompressed) ---
                    // Saving as BMP generally doesn't apply significant compression with standard GDI+.
                    originalBitmap.Save(destinationFile, ImageFormat.Bmp);
                }
                else
                {
                    // --- Unsupported Target Format ---
                    // Throw an exception if the user somehow chose an unsupported extension via the dialog
                    throw new NotSupportedException($"Saving to '{destinationExtension}' format is not explicitly supported by this function.");
                }
            }
        }

        // Check if an image has transparent pixels
        private bool HasTransparency(Bitmap bitmap)
        {
            // Only check for transparency in 32bpp images (with alpha channel)
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb &&
                bitmap.PixelFormat != PixelFormat.Format32bppPArgb)
                return false;

            // Check a sample of pixels (checking every pixel could be slow for large images)
            int stride = 10; // Check every 10th pixel
            for (int y = 0; y < bitmap.Height; y += stride)
            {
                for (int x = 0; x < bitmap.Width; x += stride)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.A < 255)
                        return true;
                }
            }

            return false;
        }

        // Helper method to get encoder info
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        // Update InsertCompressedFileRecord to work with MySQL instead of SQL Server
        // Update InsertCompressedFileRecord to insert into both 'files' and 'compressed_files'
        private void InsertCompressedFileRecord(string compressedFilePath, int originalFileId)
        {
            FileInfo fi = new FileInfo(compressedFilePath);
            double fileSizeMB = Math.Round(fi.Length / (1024.0 * 1024.0), 2);
            string fileName = fi.Name;
            string extension = fi.Extension;
            long newCompressedFileId = -1; // Variable to store the ID of the newly inserted compressed file record

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 1. Insert record for the compressed file into the 'files' table
                string insertFileQuery = @"
            INSERT INTO files (file_name, file_size, file_type, file_path, uploaded_on)
            VALUES (@name, @size, @type, @path, NOW())";

                using (MySqlCommand cmdFile = new MySqlCommand(insertFileQuery, conn))
                {
                    cmdFile.Parameters.AddWithValue("@name", fileName);
                    cmdFile.Parameters.AddWithValue("@size", fileSizeMB);
                    cmdFile.Parameters.AddWithValue("@type", extension);
                    cmdFile.Parameters.AddWithValue("@path", compressedFilePath);
                    cmdFile.ExecuteNonQuery();

                    // Get the ID of the record just inserted into the 'files' table
                    newCompressedFileId = cmdFile.LastInsertedId;
                }

                // 2. Insert record into the 'compressed_files' table to link original and compressed
                if (newCompressedFileId > 0) // Ensure the file record was inserted successfully
                {
                    string insertCompressedLinkQuery = @"
                INSERT INTO compressed_files (file_id, original_file_id, compressed_on)
                VALUES (@compressed_file_id, @original_file_id, NOW())";

                    using (MySqlCommand cmdLink = new MySqlCommand(insertCompressedLinkQuery, conn))
                    {
                        cmdLink.Parameters.AddWithValue("@compressed_file_id", newCompressedFileId); // The ID of the file record we just created
                        cmdLink.Parameters.AddWithValue("@original_file_id", originalFileId);      // The ID of the file it was compressed FROM
                        cmdLink.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Handle error - could not get the ID of the newly inserted file record
                    MessageBox.Show("Error: Could not retrieve the ID for the compressed file record. Compression link not created.");
                }
            }
        }



        private void viewAllFilesButton_Click(object sender, EventArgs e)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            compressionPanel.Visible = false;    // Added
            decompressionPanel.Visible = false;  // Added

            panelFileManagement.Visible = true; // Show File Management panel
            cmbTables.SelectedIndex = 0; // Ensure "All Files" is selected
            LoadAllFiles(); // Load the files into the grid
        }

        private int? GetOriginalFileId(int encryptedId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT original_file_id FROM encrypted WHERE encrypted_id = @encId LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@encId", encryptedId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return null; // in case not found or null
        }

        private string GetOriginalFileExtension(int originalFileId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT file_type FROM files WHERE file_id = @origId LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@origId", originalFileId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }
            return null;
        }

        private void LoadImageFileNames()
        {
            fileSelectionEncryption.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // *** ADD .bmp HERE ***
                string query = "SELECT file_id, file_name FROM files " +
                               "WHERE file_type IN ('.png','.jpg','.jpeg', '.bmp')"; // Added '.bmp'
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int fileId = reader.GetInt32("file_id");
                        string fileName = reader.GetString("file_name");

                        // Adding to dropdown
                        fileSelectionEncryption.Items.Add(new ComboboxItem
                        {
                            Text = fileName,
                            Value = fileId
                        });
                    }
                }
            }
            // Keep dropdown settings if needed
            fileSelectionEncryption.MaxDropDownItems = 5;
            fileSelectionEncryption.DropDownHeight = 100;
        }

        private void LoadEncryptedFileNames()
        {
            decryptionFileSelection.Items.Clear();

            // gets all records from `encrypted` joined with `files`
            // so we can display the file_name but track the encrypted_id or file_id
            string query = @"
                SELECT e.encrypted_id, f.file_id, f.file_name
                FROM encrypted e
                JOIN files f ON e.file_id = f.file_id
                ORDER BY e.encrypted_id DESC
            ";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int encryptedId = reader.GetInt32("encrypted_id");
                        int fileId = reader.GetInt32("file_id");
                        string fileName = reader.GetString("file_name");

                        decryptionFileSelection.Items.Add(new ComboBoxEncryptedItem
                        {
                            EncryptedId = encryptedId,
                            FileId = fileId,
                            Text = fileName
                        });
                    }
                }
            }

            decryptionFileSelection.MaxDropDownItems = 5;
            decryptionFileSelection.DropDownHeight = 100;
        }

        // Helper class to store the encrypted_id and file_id, plus display text
        public class ComboBoxEncryptedItem
        {
            public int EncryptedId { get; set; }
            public int FileId { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text; // ensures the ComboBox displays the file name
            }
        }

        // Helper class to store both display text & ID
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text; // ensures ComboBox displays Text
        }

        // Helper to retrieve the file path from DB for the chosen file
        private string GetFilePathFromDB(int fileId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT file_path FROM files WHERE file_id = @id LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", fileId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return result.ToString();
                }
            }
            return null;
        }

        private string GetStoredEncryptionKey(int encryptedId)
        {
            string query = "SELECT encryption_key FROM encrypted WHERE encrypted_id = @encId LIMIT 1";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@encId", encryptedId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString(); // might be null if not found
                }
            }
        }

        private byte[] AesEncrypt(byte[] data, string keyString)
        {
            // Convert string to byte[] for AES Key
            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyString);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                // Typically use a random IV for security:
                aes.GenerateIV();
                byte[] iv = aes.IV;
                // We'll store the IV at the start of the output so we can decrypt later:

                using (MemoryStream ms = new MemoryStream())
                {
                    // Write IV first
                    ms.Write(iv, 0, iv.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        private byte[] AesDecrypt(byte[] encryptedData, string keyString)
        {
            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyString);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                // The IV is stored in the first 16 bytes
                byte[] iv = new byte[aes.BlockSize / 8]; // Typically 16 bytes
                Array.Copy(encryptedData, iv, iv.Length);

                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a CryptoStream that decrypts the data (skipping the IV part)
                    using (CryptoStream cs = new CryptoStream(
                           ms,
                           aes.CreateDecryptor(aes.Key, iv),
                           CryptoStreamMode.Write))
                    {
                        // Write the ciphertext from encryptedData (after the IV)
                        cs.Write(encryptedData, iv.Length, encryptedData.Length - iv.Length);
                    }
                    return ms.ToArray();
                }
            }
        }


        public static Bitmap PixelScramble(Bitmap bmp, int seed)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Bitmap scrambled = new Bitmap(width, height);
            Random rng = new Random(seed);

            // Create a list of all pixel coordinates
            List<Point> points = new List<Point>(width * height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    points.Add(new Point(x, y));

            // Shuffle the list
            var shuffled = points.OrderBy(p => rng.Next()).ToList();

            // Lock the bits of both bitmaps for fast access
            Rectangle rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            System.Drawing.Imaging.BitmapData scrambledData =
                scrambled.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

            int bytesPerPixel = System.Drawing.Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int byteCount = bmpData.Stride * height;
            byte[] pixels = new byte[byteCount];
            byte[] scrambledPixels = new byte[byteCount];

            // Copy all the pixels into the array
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixels, 0, byteCount);

            // Process the pixels
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Calculate positions in original array
                    int position = y * bmpData.Stride + x * bytesPerPixel;

                    // Calculate scrambled position
                    Point scrambledPoint = shuffled[index++];
                    int scrambledPosition = scrambledPoint.Y * bmpData.Stride + scrambledPoint.X * bytesPerPixel;

                    // Copy pixel data (all channels - R,G,B,A if present)
                    for (int i = 0; i < bytesPerPixel; i++)
                    {
                        scrambledPixels[scrambledPosition + i] = pixels[position + i];
                    }
                }
            }

            // Copy the scrambled pixels back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(scrambledPixels, 0, scrambledData.Scan0, byteCount);

            // Unlock the bits
            bmp.UnlockBits(bmpData);
            scrambled.UnlockBits(scrambledData);

            return scrambled;
        }

        public static Bitmap PixelUnscramble(Bitmap scrambledBmp, int seed)
        {
            int width = scrambledBmp.Width;
            int height = scrambledBmp.Height;
            Bitmap original = new Bitmap(width, height);
            Random rng = new Random(seed);

            // Create and shuffle the points list the same way as in scrambling
            List<Point> points = new List<Point>();
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    points.Add(new Point(x, y));

            var shuffled = points.OrderBy(p => rng.Next()).ToList();

            // Lock the bits of both bitmaps for fast access
            Rectangle rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData scrambledData =
                scrambledBmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, scrambledBmp.PixelFormat);
            System.Drawing.Imaging.BitmapData originalData =
                original.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, scrambledBmp.PixelFormat);

            int bytesPerPixel = System.Drawing.Image.GetPixelFormatSize(scrambledBmp.PixelFormat) / 8;
            int byteCount = scrambledData.Stride * height;
            byte[] scrambledPixels = new byte[byteCount];
            byte[] originalPixels = new byte[byteCount];

            // Copy all the pixels into the array
            System.Runtime.InteropServices.Marshal.Copy(scrambledData.Scan0, scrambledPixels, 0, byteCount);

            // Process the pixels in reverse - for each shuffled position, put the pixel back to its original position
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Get the scrambled position for this original position
                    Point scrambledPoint = shuffled[index++];

                    // Calculate positions in arrays
                    int originalPosition = y * originalData.Stride + x * bytesPerPixel;
                    int scrambledPosition = scrambledPoint.Y * scrambledData.Stride + scrambledPoint.X * bytesPerPixel;

                    // Copy pixel data from scrambled to original (all channels)
                    for (int i = 0; i < bytesPerPixel; i++)
                    {
                        originalPixels[originalPosition + i] = scrambledPixels[scrambledPosition + i];
                    }
                }
            }

            // Copy the original pixels back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(originalPixels, 0, originalData.Scan0, byteCount);

            // Unlock the bits
            scrambledBmp.UnlockBits(scrambledData);
            original.UnlockBits(originalData);

            return original;
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            // ... (initial checks for selection, method, key validation remain the same) ...

            ComboBoxEncryptedItem selectedItem = (ComboBoxEncryptedItem)decryptionFileSelection.SelectedItem;
            int encryptedId = selectedItem.EncryptedId;
            int fileId = selectedItem.FileId; // ID of the encrypted file entry in 'files' table
            string method = decryptionMethodSelection.SelectedItem.ToString();
            string typedKey = decryptionKey.Text.Trim();
            // ... (key validation against storedKey remains the same) ...

            // Finding the original file ID
            int? origFileId = GetOriginalFileId(encryptedId);
            if (origFileId == null)
            {
                MessageBox.Show("No original_file_id found. Cannot determine original extension.");
                return;
            }

            // Finding that original extension (e.g., ".bmp", ".png", ".jpg")
            string originalExtension = GetOriginalFileExtension(origFileId.Value);
            if (string.IsNullOrEmpty(originalExtension))
            {
                // Fallback if somehow missing from DB
                originalExtension = ".dat";
                MessageBox.Show("Warning: Could not determine original file extension. Defaulting to '.dat'.");
            }

            // Load the encrypted file from disk
            string encryptedFilePath = GetFilePathFromDB(fileId);
            if (string.IsNullOrEmpty(encryptedFilePath) || !File.Exists(encryptedFilePath))
            {
                MessageBox.Show("Encrypted file not found on disk!");
                return;
            }

            byte[] decryptedData;
            try
            {
                if (method == "AES")
                {
                    // ... (AES key length validation remains the same) ...

                    byte[] encryptedBytes = File.ReadAllBytes(encryptedFilePath);
                    decryptedData = AesDecrypt(encryptedBytes, typedKey);
                    // AES decryption returns raw bytes, suitable for any file type including BMP.
                }
                else if (method == "Pixel Scrambling")
                {
                    // ... (numeric key validation remains the same) ...
                    if (!int.TryParse(typedKey, out int unscrambleSeed))
                    {
                        MessageBox.Show("For Pixel Scrambling, please enter a numeric key.");
                        return;
                    }

                    // Load the scrambled image (which we saved as PNG)
                    using (Bitmap scrambledBitmap = new Bitmap(encryptedFilePath))
                    {
                        // Apply pixel unscrambling
                        using (Bitmap unscrambledBitmap = PixelUnscramble(scrambledBitmap, unscrambleSeed))
                        {
                            // Convert the *unscrambled* bitmap back to a byte array
                            // *** THIS IS WHERE WE HANDLE THE ORIGINAL FORMAT ***
                            using (MemoryStream ms = new MemoryStream())
                            {
                                // Determine the correct format based on the ORIGINAL file extension
                                ImageFormat format;
                                string ext = originalExtension.ToLower();

                                if (ext == ".jpg" || ext == ".jpeg")
                                    format = ImageFormat.Jpeg;
                                else if (ext == ".bmp") // *** ADDED BMP CASE ***
                                    format = ImageFormat.Bmp;
                                else if (ext == ".gif")
                                    format = ImageFormat.Gif;
                                else // Default to PNG if not JPG, BMP, or GIF (or add more formats)
                                    format = ImageFormat.Png;

                                // Save the unscrambled bitmap to the stream IN THE ORIGINAL FORMAT
                                unscrambledBitmap.Save(ms, format);
                                decryptedData = ms.ToArray();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Unsupported decryption method: " + method);
                    return;
                }
            }
            catch (Exception ex)
            {
                // More specific error for bitmap loading issues common with non-image files
                if (ex is ArgumentException && method == "Pixel Scrambling")
                {
                    MessageBox.Show($"Decryption failed: Could not load the file as an image.\nEnsure the selected file '{Path.GetFileName(encryptedFilePath)}' is the correct scrambled image (usually a PNG).\nError: {ex.Message}");
                }
                else
                {
                    MessageBox.Show($"Decryption failed: {ex.Message}");
                }
                return;
            }

            // Prompt user to choose save location
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Decrypted File";
                // Filter is set based on the originalExtension found earlier
                string filterExt = originalExtension.TrimStart('.').ToUpper();
                sfd.Filter = $"{filterExt} files (*{originalExtension})|*{originalExtension}|All Files (*.*)|*.*";
                sfd.FileName = Path.GetFileNameWithoutExtension(GetFileNameFromDB(origFileId.Value) ?? "DecryptedFile") + originalExtension; // Suggest original name + original extension
                sfd.DefaultExt = originalExtension.TrimStart('.');


                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, decryptedData);

                    InsertDecryptedFileRecord(sfd.FileName); // Log the new decrypted file
                    MessageBox.Show("File decrypted and saved successfully!");
                }
            }
        }

        // Helper function assumed to exist (or add it) to get original filename for Save dialog suggestion
        private string GetFileNameFromDB(int fileId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT file_name FROM files WHERE file_id = @id LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", fileId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
            // Return null if not found
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if (fileSelectionEncryption.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an image file first.");
                return;
            }

            ComboboxItem selectedItem = (ComboboxItem)fileSelectionEncryption.SelectedItem;
            int originalFileId = (int)selectedItem.Value;

            string filePath = GetFilePathFromDB(originalFileId);
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("File not found on disk!");
                return;
            }

            if (encryptionMethodSelection.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an encryption method.");
                return;
            }
            string method = encryptionMethodSelection.SelectedItem.ToString();

            // Process based on encryption method
            byte[] processedData;
            string keyString = encryptionKeyBox.Text.Trim();
            string suggestedExtension = ".bin"; // Default for AES

            try
            {
                if (method == "AES")
                {
                    if (keyString.Length != 16 && keyString.Length != 32)
                    {
                        MessageBox.Show("Encryption Key must be 16 or 32 characters (128/256-bit).");
                        return;
                    }

                    // Read file from disk
                    byte[] originalData = File.ReadAllBytes(filePath);

                    // Encrypt the data
                    processedData = AesEncrypt(originalData, keyString);

                    // For AES, use .enc or .bin extension
                    suggestedExtension = ".enc";
                }
                else if (method == "Pixel Scrambling")
                {
                    // For pixel scrambling we need a numeric key
                    if (!int.TryParse(keyString, out int scrambleSeed))
                    {
                        MessageBox.Show("For Pixel Scrambling, please enter a numeric key.");
                        return;
                    }

                    // Load as bitmap for pixel scrambling
                    using (Bitmap originalBitmap = new Bitmap(filePath))
                    {
                        // Apply pixel scrambling
                        using (Bitmap scrambledBitmap = Form1.PixelScramble(originalBitmap, scrambleSeed))
                        {
                            // Convert to byte array
                            using (MemoryStream ms = new MemoryStream())
                            {
                                // Save in PNG format to preserve quality
                                scrambledBitmap.Save(ms, ImageFormat.Png);
                                processedData = ms.ToArray();
                            }
                        }
                    }

                    // For Pixel Scrambling, always use PNG extension
                    suggestedExtension = ".png";
                }
                else
                {
                    MessageBox.Show("Unsupported encryption method: " + method);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Processing failed: {ex.Message}");
                return;
            }

            // Prompt user to choose save location
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Processed File";

                // Set appropriate filter and default extension based on method
                if (method == "AES")
                {
                    sfd.Filter = "Encrypted Files|*.enc|Binary Files|*.bin|All Files|*.*";
                    sfd.DefaultExt = "enc";
                }
                else if (method == "Pixel Scrambling")
                {
                    sfd.Filter = "PNG Image|*.png|All Files|*.*";
                    sfd.DefaultExt = "png";
                }

                // Suggest a filename
                string originalFileName = Path.GetFileNameWithoutExtension(filePath);
                sfd.FileName = originalFileName + "_scrambled" + suggestedExtension;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Write the processed bytes to chosen file
                    File.WriteAllBytes(sfd.FileName, processedData);

                    // Insert record into database
                    InsertEncryptedRecord(originalFileId, keyString, sfd.FileName);

                    MessageBox.Show("File processed and saved successfully!");
                }
            }
        }

        private void InsertEncryptedRecord(int originalFileId, string encryptionKey, string encryptedFilePath)
        {
            FileInfo fi = new FileInfo(encryptedFilePath);
            double fileSizeMB = Math.Round(fi.Length / (1024.0 * 1024.0), 2);
            string fileName = fi.Name;
            string extension = fi.Extension;

            long newFileId;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string insertFileQuery = @"
                    INSERT INTO files (file_name, file_size, file_type, file_path, uploaded_on)
                    VALUES (@fname, @fsize, @ftype, @fpath, NOW())";

                using (MySqlCommand cmdFile = new MySqlCommand(insertFileQuery, conn))
                {
                    cmdFile.Parameters.AddWithValue("@fname", fileName);
                    cmdFile.Parameters.AddWithValue("@fsize", fileSizeMB);
                    cmdFile.Parameters.AddWithValue("@ftype", extension);
                    cmdFile.Parameters.AddWithValue("@fpath", encryptedFilePath);
                    cmdFile.ExecuteNonQuery();

                    newFileId = cmdFile.LastInsertedId;
                }

                string insertEncryptedQuery = @"
                    INSERT INTO encrypted (file_id, encryption_key, original_file_id)
                    VALUES (@fid, @ekey, @origFileId)";

                using (MySqlCommand cmdEnc = new MySqlCommand(insertEncryptedQuery, conn))
                {
                    cmdEnc.Parameters.AddWithValue("@fid", newFileId);
                    cmdEnc.Parameters.AddWithValue("@ekey", encryptionKey);
                    cmdEnc.Parameters.AddWithValue("@origFileId", originalFileId);
                    cmdEnc.ExecuteNonQuery();
                }
            }
        }

        private void InsertDecryptedFileRecord(string decryptedFilePath)
        {
            FileInfo fi = new FileInfo(decryptedFilePath);
            double fileSizeMB = Math.Round(fi.Length / (1024.0 * 1024.0), 2);
            string fileName = fi.Name;
            string extension = fi.Extension;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO files (file_name, file_size, file_type, file_path, uploaded_on)
                    VALUES (@name, @size, @type, @path, NOW())";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", fileName);
                    cmd.Parameters.AddWithValue("@size", fileSizeMB);
                    cmd.Parameters.AddWithValue("@type", extension);
                    cmd.Parameters.AddWithValue("@path", decryptedFilePath);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
