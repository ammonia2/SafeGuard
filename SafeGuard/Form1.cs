using SafeGuard.DataAccess;         // Access DatabaseManager
using SafeGuard.Models;             // Access ComboBoxItems, FileRecordInfo
using SafeGuard.Services;           // Access EncryptionService, ImageCompressionService
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;                  // Still needed for DataTable with DataGridView
using System.Drawing;
using System.Drawing.Imaging;       // Still needed for ImageFormat in decryption logic
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
// Remove unnecessary using statements like MySql.Data.MySqlClient, System.Security.Cryptography etc.

namespace SafeGuard
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        private readonly DatabaseManager _dbManager;

        public Form1()
        {
            InitializeComponent();

            // --- Initialization ---
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"]?.ConnectionString
                                    ?? throw new ConfigurationErrorsException("MySqlConnection connection string not found in configuration.");

                _dbManager = new DatabaseManager(_connectionString);
                _dbManager.EnsureDatabaseSchema(); // Ensure all tables exist
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal Error initializing application: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Consider closing the application or disabling functionality
                // For simplicity, we'll let it continue but it might fail later.
                // Environment.Exit(1); // Or handle more gracefully
                 _connectionString = string.Empty; // prevent further null refs
                 _dbManager = new DatabaseManager(string.Empty); // prevent null refs, but it won't work
                 // Disable controls maybe?
                 this.Text += " (DATABASE CONNECTION FAILED)";

            }


            SetupComboBoxes();
            LoadRecentFiles();

            // Set default visibility
            panelContent.Visible = true;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;
            decompressionPanel.Visible = false;
        }

        private void SetupComboBoxes()
        {
            // Decryption Method ComboBox
            decryptionMethodSelection.Items.Clear();
            decryptionMethodSelection.Items.Add("AES");
            decryptionMethodSelection.Items.Add("Pixel Scrambling");
            // decryptionMethodSelection.SelectedIndex = 0; // Optional default

            // File Management Table Selection ComboBox
            cmbTables.Items.Clear();
            cmbTables.Items.Add("All Files");
            cmbTables.Items.Add("Encrypted Files");
            cmbTables.Items.Add("Compressed Files");
            // cmbTables.SelectedIndex = 0; // Optional default
            cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged; // Attach handler AFTER adding items

            // Set common ComboBox properties
            Action<ComboBox> configureDropDown = cb =>
            {
                cb.MaxDropDownItems = 5;
                cb.DropDownHeight = 100;
            };
            configureDropDown(fileSelectionEncryption);
            configureDropDown(decryptionFileSelection);
            configureDropDown(fileSelectionCompression);
            configureDropDown(fileSelectionDecompression);

        }

        // --- UI Update Methods ---

        private void LoadRecentFiles()
        {
             if (_dbManager == null) return; // Don't try if DB init failed

            flowLayoutPanelRecentFiles.Controls.Clear();
            try
            {
                List<FileRecordInfo> recentFiles = _dbManager.GetRecentFiles();
                foreach (var fileInfo in recentFiles)
                {
                    Panel filePanel = CreateFilePanel(fileInfo.FileName, fileInfo.FileSizeMB, fileInfo.FileType);
                    flowLayoutPanelRecentFiles.Controls.Add(filePanel);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading recent files: {ex.Message}");
                // Optionally clear the flow panel again or show an error message panel
                flowLayoutPanelRecentFiles.Controls.Clear();
                Label errorLabel = new Label { Text = "Error loading files.", ForeColor = Color.Red, AutoSize = true };
                flowLayoutPanelRecentFiles.Controls.Add(errorLabel);
            }
        }

        private void LoadAllFilesToGrid()
        {
            if (_dbManager == null) return;
            if (cmbTables.SelectedItem == null)
            {
                dataGridViewFiles.DataSource = null; // Clear grid if nothing selected
                return;
            }

            string selectedTableType = cmbTables.SelectedItem.ToString() ?? "All Files"; // Default to prevent null

            try
            {
                DataTable dt = _dbManager.GetFilesDataTable(selectedTableType);
                dataGridViewFiles.DataSource = dt;
                dataGridViewFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                 ShowError($"Error loading files into grid: {ex.Message}");
                 dataGridViewFiles.DataSource = null; // Clear grid on error
            }
        }

        private Panel CreateFilePanel(string fileName, double fileSize, string fileType)
        {
            // (This UI helper method remains in Form1 as it directly creates UI elements)
            Panel filePanel = new Panel
            {
                BackColor = Color.LightBlue,
                Margin = new Padding(5), // Adjusted margin
                Padding = new Padding(5), // Add padding inside the panel
                Size = new Size(130, 70), // Slightly larger size
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblFileName = new Label
            {
                Text = TruncateString(fileName, 15), // Limit file name length display
                Location = new Point(5, 5),
                AutoSize = false, // Allow manual sizing
                Size = new Size(filePanel.Width - 10, 30), // Fit width
                Font = new Font(this.Font, FontStyle.Bold) // Make filename bold
            };
             ToolTip toolTip = new ToolTip(); // Add tooltip for full name
             toolTip.SetToolTip(lblFileName, fileName);


            Label lblFileSize = new Label
            {
                Text = $"{fileSize:F2} MB {fileType}", // Combine size and type
                Location = new Point(5, lblFileName.Bottom + 5),
                AutoSize = true
            };

            filePanel.Controls.Add(lblFileName);
            filePanel.Controls.Add(lblFileSize);

            return filePanel;
        }

        // Helper to truncate long strings for display
        private string TruncateString(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        private void LoadComboBoxData(ComboBox comboBox, Func<List<object>> dataSourceFunc)
        {
            if (_dbManager == null) return;
            comboBox.Items.Clear();
             try
            {
                var items = dataSourceFunc(); // Call the function passed (e.g., _dbManager.GetImageFilesForComboBox)
                // The function should return a list of the correct ComboBoxItem type
                 foreach (var item in items)
                 {
                    comboBox.Items.Add(item);
                 }
            }
            catch (Exception ex)
            {
                 ShowError($"Error loading data for {comboBox.Name}: {ex.Message}");
            }
        }

        // Specific Load methods using the helper
        private void LoadImageFilesForEncryption() => LoadComboBoxData(fileSelectionEncryption, () => _dbManager.GetImageFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadEncryptedFilesForDecryption() => LoadComboBoxData(decryptionFileSelection, () => _dbManager.GetEncryptedFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadImageFilesForCompression() => LoadComboBoxData(fileSelectionCompression, () => _dbManager.GetImageFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadCompressedFilesForDecompression() => LoadComboBoxData(fileSelectionDecompression, () => _dbManager.GetCompressedFilesForComboBox().ConvertAll(x => (object)x));


        // --- Panel Visibility Control ---
        private void ShowPanel(Panel panelToShow)
        {
             // Hide all main content panels first
             panelContent.Visible = false;
             panelEncryptionSettings.Visible = false;
             decryptionPanel.Visible = false;
             panelFileManagement.Visible = false;
             compressionPanel.Visible = false;
             decompressionPanel.Visible = false;

             // Show the requested panel
             if (panelToShow != null)
             {
                 panelToShow.Visible = true;
                 panelToShow.BringToFront(); // Ensure it's on top if overlapping
             }
        }

        // --- Event Handlers ---

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database not initialized."); return; }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a file to upload";
                 ofd.Filter = "All Files (*.*)|*.*"; // Allow any file type
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _dbManager.InsertFileRecord(ofd.FileName);
                        LoadRecentFiles(); // Refresh recent files view
                        MessageBox.Show($"File '{Path.GetFileName(ofd.FileName)}' uploaded successfully.", "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                         ShowError($"Error uploading file: {ex.Message}");
                    }
                }
            }
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllFilesToGrid();
        }

        // --- LinkLabel Navigation ---
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => ShowPanel(panelContent); // Home
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Encryption Settings
        {
            ShowPanel(panelEncryptionSettings);
            LoadImageFilesForEncryption();
            if (encryptionMethodSelection.Items.Count > 0 && encryptionMethodSelection.SelectedIndex < 0)
                 encryptionMethodSelection.SelectedIndex = 0; // Default to AES maybe
             encryptionKeyBox.Clear(); // Clear key on panel switch
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Decryption
        {
            ShowPanel(decryptionPanel);
            LoadEncryptedFilesForDecryption();
            if (decryptionMethodSelection.Items.Count > 0 && decryptionMethodSelection.SelectedIndex < 0)
                 decryptionMethodSelection.SelectedIndex = 0; // Default to AES maybe
            decryptionKey.Clear(); // Clear key
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // File Management
        {
            ShowPanel(panelFileManagement);
            if (cmbTables.Items.Count > 0 && cmbTables.SelectedIndex < 0)
                cmbTables.SelectedIndex = 0; // Default to All Files
            else
                LoadAllFilesToGrid(); // Reload current selection if already selected
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Compression
        {
            ShowPanel(compressionPanel);
            LoadImageFilesForCompression();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Decompression
        {
            ShowPanel(decompressionPanel);
            LoadCompressedFilesForDecompression();
        }
         private void viewAllFilesButton_Click(object sender, EventArgs e) // Button on Home Panel
        {
             linkLabel4_LinkClicked(sender, null); // Simulate clicking the File Management link
        }

        // --- Encryption/Decryption Button Handlers ---

        private void encryptButton_Click(object sender, EventArgs e)
        {
             if (_dbManager == null) { ShowError("Database not initialized."); return; }
            if (fileSelectionEncryption.SelectedItem == null) { ShowError("Please select an image file to encrypt."); return; }
            if (encryptionMethodSelection.SelectedItem == null) { ShowError("Please select an encryption method."); return; }

            ComboboxItem selectedFile = (ComboboxItem)fileSelectionEncryption.SelectedItem;
            int originalFileId = (int)selectedFile.Value;
            string method = encryptionMethodSelection.SelectedItem.ToString() ?? "";
            string keyString = encryptionKeyBox.Text; // Don't trim yet, validation is inside service

            string? sourceFilePath = _dbManager.GetFilePath(originalFileId);
            if (string.IsNullOrEmpty(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                ShowError($"Source file not found for ID {originalFileId}. Path: {sourceFilePath ?? "N/A"}");
                LoadImageFilesForEncryption(); // Refresh list in case file was deleted
                return;
            }

             // Validate key based on method BEFORE processing
            try
            {
                 if (method == "AES" && (keyString.Length != 16 && keyString.Length != 32))
                     throw new ArgumentException("AES Key must be 16 or 32 characters.");
                 if (method == "Pixel Scrambling" && !int.TryParse(keyString, out _))
                     throw new ArgumentException("Pixel Scrambling key must be a whole number.");
            }
            catch (ArgumentException ex)
            {
                ShowError(ex.Message);
                return;
            }


            byte[] processedData;
            string suggestedExtension = ".bin"; // Default

            try
            {
                if (method == "AES")
                {
                    byte[] originalData = File.ReadAllBytes(sourceFilePath);
                    processedData = EncryptionService.AesEncrypt(originalData, keyString);
                    suggestedExtension = ".enc";
                }
                else // Pixel Scrambling
                {
                    int scrambleSeed = int.Parse(keyString); // Already validated it's an int
                    using (Bitmap originalBitmap = new Bitmap(sourceFilePath))
                    using (Bitmap scrambledBitmap = EncryptionService.PixelScramble(originalBitmap, scrambleSeed))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Always save scrambled as PNG to preserve data
                        scrambledBitmap.Save(ms, ImageFormat.Png);
                        processedData = ms.ToArray();
                    }
                    suggestedExtension = ".png"; // Scrambled file is a PNG
                }
            }
             catch (ArgumentException ex) // Catch specific exceptions from services if needed
            {
                ShowError($"Encryption Error: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                ShowError($"An unexpected error occurred during encryption: {ex.Message}\n{ex.InnerException?.Message}");
                return;
            }

            // Save the processed file
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = $"Save {method} Encrypted File";
                string originalFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                sfd.FileName = $"{originalFileName}_encrypted{suggestedExtension}";

                if (method == "AES")
                    sfd.Filter = $"Encrypted File (*{suggestedExtension})|*{suggestedExtension}|All Files (*.*)|*.*";
                else // Pixel Scrambling
                    sfd.Filter = $"PNG Image (*{suggestedExtension})|*{suggestedExtension}|All Files (*.*)|*.*";

                sfd.DefaultExt = suggestedExtension.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllBytes(sfd.FileName, processedData);
                        // Add record to DB linking original and new encrypted file
                        _dbManager.InsertEncryptedRecord(originalFileId, keyString, sfd.FileName);
                        MessageBox.Show("File encrypted and saved successfully!", "Encryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         LoadImageFilesForEncryption(); // Refresh list
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Error saving encrypted file or updating database: {ex.Message}");
                    }
                }
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database not initialized."); return; }
            if (decryptionFileSelection.SelectedItem == null) { ShowError("Please select a file to decrypt."); return; }
            if (decryptionMethodSelection.SelectedItem == null) { ShowError("Please select a decryption method."); return; }

            ComboBoxEncryptedItem selectedEncryptedFile = (ComboBoxEncryptedItem)decryptionFileSelection.SelectedItem;
            int encryptedRecordId = selectedEncryptedFile.EncryptedId; // ID from 'encrypted' table
            int encryptedFileId = selectedEncryptedFile.FileId; // ID from 'files' table (for the encrypted file)
            string method = decryptionMethodSelection.SelectedItem.ToString() ?? "";
            string typedKey = decryptionKey.Text; // Don't trim yet

             // --- Key Validation ---
            string? storedKey = null;
            try
            {
                 storedKey = _dbManager.GetEncryptionKey(encryptedRecordId);
                 if (storedKey == null)
                     throw new Exception("Could not retrieve the stored encryption key for this file.");

                 if (typedKey != storedKey)
                 {
                    // Optional: Provide more specific feedback without revealing the key
                    if (method == "AES" && (typedKey.Length != 16 && typedKey.Length != 32))
                        throw new ArgumentException("Invalid key length for AES.");
                    if (method == "Pixel Scrambling" && !int.TryParse(typedKey, out _))
                        throw new ArgumentException("Invalid key format for Pixel Scrambling (must be a number).");

                    // Generic incorrect key message
                    throw new ArgumentException("Incorrect decryption key provided.");
                 }
                 // Key is validated against stored key, proceed
            }
             catch (Exception ex)
            {
                 ShowError($"Key validation failed: {ex.Message}");
                 return;
            }
            // --- End Key Validation ---


            // --- Get Original File Info ---
             int? originalFileId = _dbManager.GetOriginalFileIdFromEncrypted(encryptedRecordId);
             if (originalFileId == null) { ShowError("Cannot find the link to the original file."); return; }

             string? originalFileType = _dbManager.GetFileType(originalFileId.Value);
             string? originalFileName = _dbManager.GetFileName(originalFileId.Value);
             if (string.IsNullOrEmpty(originalFileType) || string.IsNullOrEmpty(originalFileName)) { ShowError("Cannot retrieve original file details (name or type)."); return; }
             // --- End Get Original File Info ---


            string? encryptedFilePath = _dbManager.GetFilePath(encryptedFileId);
            if (string.IsNullOrEmpty(encryptedFilePath) || !File.Exists(encryptedFilePath))
            {
                ShowError($"Encrypted file not found on disk. Path: {encryptedFilePath ?? "N/A"}");
                LoadEncryptedFilesForDecryption(); // Refresh list
                return;
            }


            byte[] decryptedData;
            try
            {
                if (method == "AES")
                {
                    byte[] encryptedBytes = File.ReadAllBytes(encryptedFilePath);
                    decryptedData = EncryptionService.AesDecrypt(encryptedBytes, typedKey);
                }
                else // Pixel Scrambling
                {
                    int unscrambleSeed = int.Parse(typedKey); // Already validated
                    using (Bitmap scrambledBitmap = new Bitmap(encryptedFilePath)) // Load the PNG
                    using (Bitmap unscrambledBitmap = EncryptionService.PixelUnscramble(scrambledBitmap, unscrambleSeed))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Save the unscrambled bitmap in its ORIGINAL format
                        ImageFormat originalFormat = GetImageFormatFromExtension(originalFileType) ?? ImageFormat.Png; // Default if unknown
                        unscrambledBitmap.Save(ms, originalFormat);
                        decryptedData = ms.ToArray();
                    }
                }
            }
            catch (CryptographicException ex) // More specific for AES
            {
                 ShowError($"Decryption failed. This often indicates an incorrect key or corrupted data.\nDetails: {ex.Message}");
                 return;
            }
            catch (ArgumentException ex) // Catch issues like invalid image format during Pixel Scramble load
            {
                 ShowError($"Decryption failed. The file might not be a valid '{method}' encrypted file.\nDetails: {ex.Message}");
                 return;
            }
            catch (Exception ex)
            {
                 ShowError($"An unexpected error occurred during decryption: {ex.Message}\n{ex.InnerException?.Message}");
                 return;
            }


            // Save the decrypted file
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Decrypted File";
                sfd.FileName = Path.GetFileNameWithoutExtension(originalFileName) + "_decrypted" + originalFileType;
                string filterExt = originalFileType.TrimStart('.').ToUpperInvariant();
                sfd.Filter = $"{filterExt} Files (*{originalFileType})|*{originalFileType}|All Files (*.*)|*.*";
                sfd.DefaultExt = originalFileType.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllBytes(sfd.FileName, decryptedData);
                        // Add record for the new decrypted file to 'files' table
                        _dbManager.InsertDerivedFileRecord(sfd.FileName);
                         MessageBox.Show("File decrypted and saved successfully!", "Decryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         LoadEncryptedFilesForDecryption(); // Refresh list
                    }
                    catch (Exception ex)
                    {
                         ShowError($"Error saving decrypted file or updating database: {ex.Message}");
                    }
                }
            }
        }

        // --- Compression/Decompression Button Handlers ---

        private void compressButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database not initialized."); return; }
            if (fileSelectionCompression.SelectedItem == null) { ShowError("Please select an image file to optimize."); return; }

            ComboboxItem selectedFile = (ComboboxItem)fileSelectionCompression.SelectedItem;
            int originalFileId = (int)selectedFile.Value;

            string? sourceFilePath = _dbManager.GetFilePath(originalFileId);
             if (string.IsNullOrEmpty(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                 ShowError($"Source file not found. Path: {sourceFilePath ?? "N/A"}");
                 LoadImageFilesForCompression(); // Refresh
                 return;
            }

             // Prompt for save location and format
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Optimized Image";
                string originalFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                string originalExtension = Path.GetExtension(sourceFilePath).ToLowerInvariant();

                // Suggest appropriate filters based on original type
                if (originalExtension == ".bmp")
                {
                    sfd.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|BMP Image (*.bmp)|*.bmp|All Files (*.*)|*.*";
                    sfd.DefaultExt = "png"; // Suggest PNG for lossless compression of BMP
                    sfd.FileName = $"{originalFileName}_optimized.png";
                }
                else if (originalExtension == ".jpg" || originalExtension == ".jpeg")
                {
                     sfd.Filter = "JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*";
                     sfd.DefaultExt = "jpg";
                     sfd.FileName = $"{originalFileName}_optimized.jpg";
                }
                 else // Default to PNG (e.g., if original was PNG)
                {
                     sfd.Filter = "PNG Image (*.png)|*.png|All Files (*.*)|*.*";
                     sfd.DefaultExt = "png";
                     sfd.FileName = $"{originalFileName}_optimized.png";
                }


                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                         // Use the destination path from SaveFileDialog
                         ImageCompressionService.CompressImage(sourceFilePath, sfd.FileName);

                         // Add record to DB linking original and new compressed file
                        _dbManager.InsertCompressedFileRecord(sfd.FileName, originalFileId);

                         MessageBox.Show("Image optimized and saved successfully!", "Compression Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         LoadImageFilesForCompression(); // Refresh source list
                    }
                     catch (Exception ex)
                    {
                         ShowError($"Image optimization failed: {ex.Message}");
                    }
                }
            }
        }

        private void decompressButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database not initialized."); return; }
            if (fileSelectionDecompression.SelectedItem == null) { ShowError("Please select a compressed file to decompress."); return; }

            CompressionComboboxItem selectedCompressedFile = (CompressionComboboxItem)fileSelectionDecompression.SelectedItem;
            int compressedRecordId = selectedCompressedFile.CompressedFileId; // ID from 'compressed_files'
            int compressedFileId = selectedCompressedFile.FileId; // ID from 'files' (for the compressed file)

            // --- Get Original File Info ---
             int? originalFileId = _dbManager.GetOriginalFileIdFromCompressed(compressedRecordId);
             if (originalFileId == null) { ShowError("Cannot find the link to the original file for this compressed version."); return; }

             string? originalFileType = _dbManager.GetFileType(originalFileId.Value);
             string? originalFileName = _dbManager.GetFileName(originalFileId.Value);
             if (string.IsNullOrEmpty(originalFileType) || string.IsNullOrEmpty(originalFileName)) { ShowError("Cannot retrieve original file details (name or type)."); return; }
            // --- End Get Original File Info ---

            string? compressedFilePath = _dbManager.GetFilePath(compressedFileId);
             if (string.IsNullOrEmpty(compressedFilePath) || !File.Exists(compressedFilePath))
            {
                 ShowError($"Compressed file not found on disk. Path: {compressedFilePath ?? "N/A"}");
                 LoadCompressedFilesForDecompression(); // Refresh
                 return;
            }

             // Prompt user to save the decompressed file
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                 sfd.Title = "Save Decompressed File";
                 sfd.FileName = Path.GetFileNameWithoutExtension(originalFileName) + "_decompressed" + originalFileType;
                 string filterExt = originalFileType.TrimStart('.').ToUpperInvariant();
                 sfd.Filter = $"{filterExt} Files (*{originalFileType})|*{originalFileType}|All Files (*.*)|*.*";
                 sfd.DefaultExt = originalFileType.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                         // "Decompress" (re-save) the image using the destination path/format
                        ImageCompressionService.DecompressImage(compressedFilePath, sfd.FileName);

                        // Add record for the new decompressed file to 'files' table
                        _dbManager.InsertDerivedFileRecord(sfd.FileName);

                         MessageBox.Show("File decompressed and saved successfully!", "Decompression Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCompressedFilesForDecompression(); // Refresh list
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Decompression failed: {ex.Message}");
                    }
                }
            }
        }

        // --- Helper Methods ---
        private void ShowError(string message)
        {
             MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Helper to get ImageFormat from file extension string
        private ImageFormat? GetImageFormatFromExtension(string extension)
        {
            switch (extension?.ToLowerInvariant())
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg":
                case ".jpeg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                case ".tiff":
                case ".tif": return ImageFormat.Tiff;
                case ".ico": return ImageFormat.Icon;
                // Add more as needed
                default: return null; // Or return a default like ImageFormat.Png
            }
        }
    }
}