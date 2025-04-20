using SafeGuard.DataAccess;
using SafeGuard.Models;
using SafeGuard.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging; // Needed for ImageFormat
using System.IO; // Needed for Path, File, Directory
using System.Linq; // Needed for Linq extension methods like Contains
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SafeGuard
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        private readonly DatabaseManager _dbManager;
        private const string SavedImagesFolderName = "savedimages"; // Subfolder name
        private readonly HashSet<string> _supportedImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".png", ".jpg", ".jpeg", ".bmp" // Add more if needed
        };

        public Form1()
        {
            InitializeComponent();

            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"]?.ConnectionString
                                    ?? throw new ConfigurationErrorsException("MySqlConnection connection string not found in configuration.");

                _dbManager = new DatabaseManager(_connectionString);
                _dbManager.EnsureDatabaseSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal Error initializing application: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _connectionString = string.Empty;
                // Use a dummy manager to avoid null refs, but operations will fail
                _dbManager = new DatabaseManager("Server=none;"); // Provide a minimal invalid string
                this.Text += " (DATABASE CONNECTION FAILED)";
                // Disable controls that need the DB? (Optional)
                btnUpload.Enabled = false;
                panelDropPasteTarget.AllowDrop = false; // Disable drop if DB fails
                lblDropPasteHint.Text = "Database connection failed.\nUpload/Paste disabled.";
            }

            SetupComboBoxes();
            LoadRecentFiles();

            this.KeyPreview = true; // Allow form to preview key events
            this.KeyDown += Form1_KeyDown; // Add KeyDown handler

            panelContent.Visible = true;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;
            decompressionPanel.Visible = false;
        }

        private void dataGridViewFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a valid row (not header or invalid index)
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewFiles.RowCount)
            {
                return;
            }

            // Ensure the DataSource is a DataTable
            if (!(dataGridViewFiles.DataSource is DataTable dt))
            {
                Console.WriteLine("DataSource is not a DataTable or is null.");
                return;
            }

            // Get the DataRow corresponding to the clicked DataGridView row
            // Use Rows[e.RowIndex].DataBoundItem which is a DataRowView, then get its Row
            if (!(dataGridViewFiles.Rows[e.RowIndex].DataBoundItem is DataRowView drv))
            {
                Console.WriteLine($"Could not get DataRowView for row index {e.RowIndex}.");
                return;
            }
            DataRow dr = drv.Row;


            string? filePath = null;
            string? fileName = null;
            string pathColumnName = "";
            string nameColumnName = "";
            string selectedTableType = cmbTables.SelectedItem?.ToString() ?? "All Files"; // Default to All if null


            // Determine the correct column names based on the selected table type
            switch (selectedTableType)
            {
                case "All Files":
                    pathColumnName = "file_path";
                    nameColumnName = "file_name";
                    break;
                case "Encrypted Files":
                    // The path/name of the *encrypted* file itself
                    pathColumnName = "encrypted_file_path";
                    nameColumnName = "encrypted_file_name";
                    break;
                case "Compressed Files":
                    // The path/name of the *compressed* file itself
                    pathColumnName = "compressed_file_path";
                    nameColumnName = "compressed_file_name";
                    break;
                default:
                    Console.WriteLine($"Unknown table type selected: {selectedTableType}");
                    return; // Don't know which columns to use
            }

            try
            {
                // Check if the required columns exist in the DataTable
                if (dt.Columns.Contains(pathColumnName) && dt.Columns.Contains(nameColumnName))
                {
                    // Get the file path and name from the DataRow, handling potential DBNull
                    filePath = dr[pathColumnName] != DBNull.Value ? dr[pathColumnName].ToString() : null;
                    fileName = dr[nameColumnName] != DBNull.Value ? dr[nameColumnName].ToString() : "Image"; // Default title

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        // Use the existing helper method to attempt showing the image
                        // It handles checking the extension and file existence internally
                        ShowImageInNewWindow(filePath, $"Viewer: {fileName}");
                    }
                    else
                    {
                        Console.WriteLine($"File path is null or empty in column '{pathColumnName}' for row {e.RowIndex}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Required columns ('{pathColumnName}' or '{nameColumnName}') not found in the DataTable for table type '{selectedTableType}'.");
                }
            }
            catch (Exception ex)
            {
                // Log or show error specific to accessing grid data
                Console.WriteLine($"Error accessing data grid row {e.RowIndex}: {ex.Message}");
                // Optionally show a message to the user:
                // ShowError($"Could not retrieve file details from the selected row.\n{ex.Message}");
            }
        }

        private void SetupComboBoxes()
        {
            decryptionMethodSelection.Items.Clear();
            decryptionMethodSelection.Items.Add("AES");
            decryptionMethodSelection.Items.Add("Pixel Scrambling");

            cmbTables.Items.Clear();
            cmbTables.Items.Add("All Files");
            cmbTables.Items.Add("Encrypted Files");
            cmbTables.Items.Add("Compressed Files");
            cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged;

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

        private string EnsureSavedImagesFolder()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string targetDir = Path.Combine(baseDir, SavedImagesFolderName);

                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                return targetDir;
            }
            catch (Exception ex)
            {
                ShowError($"Error creating or accessing the '{SavedImagesFolderName}' directory: {ex.Message}");
                return string.Empty; // Indicate failure
            }
        }

        private string GenerateUniqueImagePath(string targetFolder, string baseName, string extension)
        {
            string uniqueName = $"{Path.GetFileNameWithoutExtension(baseName)}_{DateTime.Now:yyyyMMddHHmmssfff}{extension}";
            return Path.Combine(targetFolder, uniqueName);
        }

        private bool ProcessAndSaveDroppedFile(string sourceFilePath)
        {
            if (_dbManager == null || string.IsNullOrEmpty(_connectionString) || _connectionString.StartsWith("Server=none;"))
            { ShowError("Database not available. Cannot save file."); return false; }
            if (!File.Exists(sourceFilePath))
            { ShowError($"Source file not found: {sourceFilePath}"); return false; }

            string extension = Path.GetExtension(sourceFilePath);
            if (!_supportedImageExtensions.Contains(extension))
            {
                // Silently ignore non-image files or show a collective message later
                Console.WriteLine($"Skipped non-image file: {Path.GetFileName(sourceFilePath)}");
                return false; // Indicate not processed as an image
            }

            string targetFolder = EnsureSavedImagesFolder();
            if (string.IsNullOrEmpty(targetFolder)) return false; // Error occurred creating folder

            string destinationPath = GenerateUniqueImagePath(targetFolder, Path.GetFileName(sourceFilePath), extension);

            try
            {
                File.Copy(sourceFilePath, destinationPath, true); // Overwrite if somehow exists
                long fileId = _dbManager.InsertFileRecord(destinationPath); // Add to DB
                Console.WriteLine($"Successfully added dropped file: {Path.GetFileName(destinationPath)} (ID: {fileId})");
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Error processing dropped file '{Path.GetFileName(sourceFilePath)}':\n{ex.Message}");
                // Clean up potentially partially created file
                if (File.Exists(destinationPath))
                {
                    try { File.Delete(destinationPath); } catch { /* Ignore delete error */ }
                }
                return false;
            }
        }

        private bool ProcessAndSavePastedImage(Image image)
        {
            if (_dbManager == null || string.IsNullOrEmpty(_connectionString) || _connectionString.StartsWith("Server=none;"))
            { ShowError("Database not available. Cannot save image."); return false; }
            if (image == null) { return false; } // Nothing to paste

            string targetFolder = EnsureSavedImagesFolder();
            if (string.IsNullOrEmpty(targetFolder)) return false;

            // Save pasted images as PNG by default
            string destinationPath = GenerateUniqueImagePath(targetFolder, "pasted_image", ".png");

            try
            {
                image.Save(destinationPath, ImageFormat.Png);
                long fileId = _dbManager.InsertFileRecord(destinationPath); // Add to DB
                Console.WriteLine($"Successfully added pasted image: {Path.GetFileName(destinationPath)} (ID: {fileId})");
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Error saving pasted image:\n{ex.Message}");
                // Clean up potentially partially created file
                if (File.Exists(destinationPath))
                {
                    try { File.Delete(destinationPath); } catch { /* Ignore delete error */ }
                }
                return false;
            }
            finally
            {
                // The image from clipboard should be disposed after we're done saving it.
                image?.Dispose();
            }
        }

        // --- NEW EVENT HANDLERS ---

        private void panelDropPasteTarget_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the data being dragged is file data
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Show the copy cursor
            }
            else
            {
                e.Effect = DragDropEffects.None; // Show the standard cursor
            }
        }

        private void panelDropPasteTarget_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                int successCount = 0;
                int skippedCount = 0;

                foreach (string file in files)
                {
                    if (ProcessAndSaveDroppedFile(file))
                    {
                        successCount++;
                    }
                    else
                    {
                        // Check if it was skipped because it wasn't an image
                        if (_supportedImageExtensions.Contains(Path.GetExtension(file)))
                            skippedCount++; // It was an image but failed processing
                        // else: it was skipped because it wasn't an image (already logged)
                    }
                }

                if (successCount > 0)
                {
                    MessageBox.Show($"{successCount} image(s) added successfully.", "Drag & Drop Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecentFiles(); // Refresh the recent files panel
                }
                else if (skippedCount > 0)
                {
                    MessageBox.Show($"No images were added. Failed to process {skippedCount} image(s). Check error messages or console output.", "Drag & Drop Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // If only non-image files were dropped, no message is shown (they are silently ignored)
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check for Ctrl+V only when the main content panel is visible
            if (e.Control && e.KeyCode == Keys.V && panelContent.Visible)
            {
                // Check if the clipboard contains image data
                if (Clipboard.ContainsImage())
                {
                    Image pastedImage = null;
                    try
                    {
                        pastedImage = Clipboard.GetImage(); // Get the image
                        if (ProcessAndSavePastedImage(pastedImage)) // Process and save (includes dispose)
                        {
                            MessageBox.Show("Image pasted and saved successfully.", "Paste Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRecentFiles(); // Refresh recent files
                        }
                        // If ProcessAndSavePastedImage returns false, it already showed an error
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Error getting image from clipboard: {ex.Message}");
                        pastedImage?.Dispose(); // Ensure disposal even on error getting it
                    }
                    finally
                    {
                        e.Handled = true; // Mark event as handled to prevent further processing
                        e.SuppressKeyPress = true; // Prevent the character from being entered if applicable
                    }
                }
            }
        }

        private void LoadRecentFiles()
        {
            if (_dbManager == null) return;

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
                dataGridViewFiles.DataSource = null;
                return;
            }

            string selectedTableType = cmbTables.SelectedItem.ToString() ?? "All Files";

            try
            {
                DataTable dt = _dbManager.GetFilesDataTable(selectedTableType);
                dataGridViewFiles.DataSource = dt;
                dataGridViewFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                ShowError($"Error loading files into grid: {ex.Message}");
                dataGridViewFiles.DataSource = null;
            }
        }

        private Panel CreateFilePanel(string fileName, double fileSize, string fileType)
        {
            Panel filePanel = new Panel
            {
                BackColor = Color.LightBlue,
                Margin = new Padding(5),
                Padding = new Padding(5),
                Size = new Size(130, 70),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblFileName = new Label
            {
                Text = TruncateString(fileName, 15),
                Location = new Point(5, 5),
                AutoSize = false,
                Size = new Size(filePanel.Width - 10, 30),
                Font = new Font(this.Font, FontStyle.Bold)
            };
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(lblFileName, fileName);

            Label lblFileSize = new Label
            {
                Text = $"{fileSize:F2} MB {fileType}",
                Location = new Point(5, lblFileName.Bottom + 5),
                AutoSize = true
            };

            filePanel.Controls.Add(lblFileName);
            filePanel.Controls.Add(lblFileSize);

            return filePanel;
        }

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
                var items = dataSourceFunc();
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

        private void LoadImageFilesForEncryption() => LoadComboBoxData(fileSelectionEncryption, () => _dbManager.GetImageFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadEncryptedFilesForDecryption() => LoadComboBoxData(decryptionFileSelection, () => _dbManager.GetEncryptedFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadImageFilesForCompression() => LoadComboBoxData(fileSelectionCompression, () => _dbManager.GetImageFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadCompressedFilesForDecompression() => LoadComboBoxData(fileSelectionDecompression, () => _dbManager.GetCompressedFilesForComboBox().ConvertAll(x => (object)x));

        private void ShowPanel(Panel panelToShow)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;
            decompressionPanel.Visible = false;

            if (panelToShow != null)
            {
                panelToShow.Visible = true;
                panelToShow.BringToFront();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database not initialized."); return; }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a file to upload";
                ofd.Filter = "All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _dbManager.InsertFileRecord(ofd.FileName);
                        LoadRecentFiles();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => ShowPanel(panelContent);
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(panelEncryptionSettings);
            LoadImageFilesForEncryption();
            if (encryptionMethodSelection.Items.Count > 0 && encryptionMethodSelection.SelectedIndex < 0)
                encryptionMethodSelection.SelectedIndex = 0;
            encryptionKeyBox.Clear();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(decryptionPanel);
            LoadEncryptedFilesForDecryption();
            if (decryptionMethodSelection.Items.Count > 0 && decryptionMethodSelection.SelectedIndex < 0)
                decryptionMethodSelection.SelectedIndex = 0;
            decryptionKey.Clear();
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(panelFileManagement);
            if (cmbTables.Items.Count > 0 && cmbTables.SelectedIndex < 0)
                cmbTables.SelectedIndex = 0;
            else
                LoadAllFilesToGrid();
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(compressionPanel);
            LoadImageFilesForCompression();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(decompressionPanel);
            LoadCompressedFilesForDecompression();
        }
        private void viewAllFilesButton_Click(object sender, EventArgs e)
        {
            linkLabel4_LinkClicked(sender, null);
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (fileSelectionEncryption.SelectedItem == null) { ShowError("Please select an image file to encrypt."); return; }
            if (encryptionMethodSelection.SelectedItem == null) { ShowError("Please select an encryption method."); return; }

            // 1. Get Selected File Info
            ComboboxItem selectedFile = (ComboboxItem)fileSelectionEncryption.SelectedItem;
            int originalFileId = (int)selectedFile.Value;
            string method = encryptionMethodSelection.SelectedItem.ToString() ?? "";
            string keyString = encryptionKeyBox.Text; // Key entered by user

            // 2. Validate Key Format (Early Exit)
            try
            {
                if (method == "AES" && (keyString.Length != 16 && keyString.Length != 32))
                    throw new ArgumentException("AES Key must be 16 or 32 characters long.");
                if (method == "Pixel Scrambling" && !int.TryParse(keyString, out _))
                    throw new ArgumentException("Pixel Scrambling key must be a whole number.");
            }
            catch (ArgumentException ex)
            {
                ShowError(ex.Message);
                return;
            }

            // 3. Get Source File Path and Validate Existence
            string? sourceFilePath = _dbManager.GetFilePath(originalFileId);
            if (string.IsNullOrEmpty(sourceFilePath))
            {
                ShowError($"Source file path is missing for the selected file (ID: {originalFileId}).");
                LoadImageFilesForEncryption(); // Refresh list
                return;
            }
            if (!File.Exists(sourceFilePath))
            {
                ShowError($"Source file not found on disk. Path: {sourceFilePath}");
                LoadImageFilesForEncryption(); // Refresh list
                return;
            }

            // 4. Perform Encryption
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
                    int scrambleSeed = int.Parse(keyString); // Safe parse after validation
                    using (Bitmap originalBitmap = new Bitmap(sourceFilePath))
                    using (Bitmap scrambledBitmap = EncryptionService.PixelScramble(originalBitmap, scrambleSeed))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Always save scrambled image as PNG to preserve data, regardless of original format
                        scrambledBitmap.Save(ms, ImageFormat.Png);
                        processedData = ms.ToArray();
                    }
                    suggestedExtension = ".png"; // The scrambled file itself is a PNG
                }
            }
            catch (ArgumentException ex) // Catch specific errors from services
            {
                ShowError($"Encryption setup error: {ex.Message}");
                return;
            }
            catch (CryptographicException ex)
            {
                 ShowError($"Cryptography error during encryption: {ex.Message}");
                 return;
            }
            catch (Exception ex) // Catch-all for file reading, bitmap processing etc.
            {
                ShowError($"An error occurred during processing: {ex.Message}\n{ex.InnerException?.Message}");
                return;
            }

            // 5. Prompt User to Save Encrypted File
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = $"Save {method} Encrypted File";
                string originalFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                sfd.FileName = $"{originalFileName}_encrypted{suggestedExtension}";

                if (method == "AES")
                    sfd.Filter = $"Encrypted File (*{suggestedExtension})|*{suggestedExtension}|All Files (*.*)|*.*";
                else // Pixel Scrambling (result is PNG)
                    sfd.Filter = $"PNG Image (*{suggestedExtension})|*{suggestedExtension}|All Files (*.*)|*.*";

                sfd.DefaultExt = suggestedExtension.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string savedFilePath = sfd.FileName;
                    try
                    {
                        // 6. Write Encrypted Data to File
                        File.WriteAllBytes(sfd.FileName, processedData);

                        // 7. Add Record to Database
                        _dbManager.InsertEncryptedRecord(originalFileId, keyString, sfd.FileName);

                        MessageBox.Show("File encrypted and saved successfully!", "Encryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (method == "Pixel Scrambling")
                        {
                            ShowImageInNewWindow(savedFilePath, $"Encrypted: {Path.GetFileName(savedFilePath)} (Scrambled)");
                        }

                        LoadImageFilesForEncryption(); // Refresh the source list
                        encryptionKeyBox.Clear(); // Clear key for next operation
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Error saving encrypted file or updating database: {ex.Message}");
                        // Consider trying to delete the partially saved file if the DB insert fails
                        if (File.Exists(sfd.FileName))
                        {
                             try { File.Delete(sfd.FileName); } catch { /* Ignore delete error */ }
                        }
                    }
                }
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (decryptionFileSelection.SelectedItem == null) { ShowError("Please select a file to decrypt."); return; }
            if (decryptionMethodSelection.SelectedItem == null) { ShowError("Please select a decryption method."); return; }

            // 1. Get Selected Encrypted File Info
            ComboBoxEncryptedItem selectedEncryptedFile = (ComboBoxEncryptedItem)decryptionFileSelection.SelectedItem;
            int encryptedRecordId = selectedEncryptedFile.EncryptedId; // ID from 'encrypted' table link
            int encryptedFileId = selectedEncryptedFile.FileId;       // ID from 'files' table (the encrypted file itself)
            string method = decryptionMethodSelection.SelectedItem.ToString() ?? "";
            string typedKey = decryptionKey.Text;                   // Key entered by user

            // 2. Validate Typed Key Against Stored Key
            try
            {
                 string? storedKey = _dbManager.GetEncryptionKey(encryptedRecordId);
                 if (storedKey == null)
                     throw new KeyNotFoundException("Could not retrieve the stored encryption key for this file record.");

                 if (typedKey != storedKey)
                 {
                    // More specific feedback *without* revealing the key
                    if (method == "AES" && (typedKey.Length != 16 && typedKey.Length != 32))
                        throw new ArgumentException("Invalid key length for AES decryption attempt.");
                    if (method == "Pixel Scrambling" && !int.TryParse(typedKey, out _))
                        throw new ArgumentException("Invalid key format for Pixel Scrambling decryption attempt (must be a number).");

                    // Generic incorrect key message if format seems ok but value is wrong
                    throw new ArgumentException("Incorrect decryption key provided.");
                 }
                 // Key matches stored key, proceed.
            }
             catch (Exception ex) // Catches ArgumentException, KeyNotFoundException, DB errors
            {
                 ShowError($"Key validation failed: {ex.Message}");
                 return;
            }

            // 3. Get Original File Details (for saving correctly)
            int? originalFileId = _dbManager.GetOriginalFileIdFromEncrypted(encryptedRecordId);
            if (originalFileId == null) { ShowError("Critical error: Cannot find the link to the original file record."); return; }

            string? originalFileType = _dbManager.GetFileType(originalFileId.Value);
            string? originalFileName = _dbManager.GetFileName(originalFileId.Value);
            if (string.IsNullOrEmpty(originalFileType) || string.IsNullOrEmpty(originalFileName))
            {
                ShowError("Critical error: Cannot retrieve original file details (name or type).");
                return;
            }

            // 4. Get Encrypted File Path and Validate Existence
            string? encryptedFilePath = _dbManager.GetFilePath(encryptedFileId);
             if (string.IsNullOrEmpty(encryptedFilePath))
            {
                ShowError($"Encrypted file path is missing for the selected file (ID: {encryptedFileId}).");
                LoadEncryptedFilesForDecryption(); // Refresh list
                return;
            }
            if (!File.Exists(encryptedFilePath))
            {
                ShowError($"Encrypted file not found on disk. Path: {encryptedFilePath}");
                LoadEncryptedFilesForDecryption(); // Refresh list
                return;
            }

            // 5. Perform Decryption
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
                    int unscrambleSeed = int.Parse(typedKey); // Safe parse after validation
                    using (Bitmap scrambledBitmap = new Bitmap(encryptedFilePath)) // Load the PNG file
                    using (Bitmap unscrambledBitmap = EncryptionService.PixelUnscramble(scrambledBitmap, unscrambleSeed))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Save the unscrambled bitmap in its ORIGINAL format
                        ImageFormat originalFormat = GetImageFormatFromExtension(originalFileType) ?? ImageFormat.Png; // Default to PNG if format unknown/unsupported
                        unscrambledBitmap.Save(ms, originalFormat);
                        decryptedData = ms.ToArray();
                    }
                }
            }
            catch (CryptographicException ex) // Specific to AES issues (bad key, padding, corruption)
            {
                 ShowError($"Decryption failed. This often indicates an incorrect key or corrupted data.\nDetails: {ex.Message}");
                 return;
            }
            catch (ArgumentException ex) // Catches bad image format on load, invalid key length in service etc.
            {
                 ShowError($"Decryption failed. The file might not be a valid '{method}' encrypted file, or another argument was invalid.\nDetails: {ex.Message}");
                 return;
            }
             catch (Exception ex) // Catch-all for file reading, bitmap processing etc.
            {
                 ShowError($"An unexpected error occurred during decryption: {ex.Message}\n{ex.InnerException?.Message}");
                 return;
            }

            // 6. Prompt User to Save Decrypted File
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Decrypted File";
                // Suggest original name + "_decrypted" + original extension
                sfd.FileName = Path.GetFileNameWithoutExtension(originalFileName) + "_decrypted" + originalFileType;
                string filterExt = originalFileType.TrimStart('.').ToUpperInvariant();
                sfd.Filter = $"{filterExt} Files (*{originalFileType})|*{originalFileType}|All Files (*.*)|*.*";
                sfd.DefaultExt = originalFileType.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string savedFilePath = sfd.FileName;
                    try
                    {
                        // 7. Write Decrypted Data to File
                        File.WriteAllBytes(sfd.FileName, decryptedData);

                        // 8. Add Record for the New Decrypted File
                        _dbManager.InsertDerivedFileRecord(sfd.FileName);

                        MessageBox.Show("File decrypted and saved successfully!", "Decryption Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowImageInNewWindow(savedFilePath, $"Decrypted: {Path.GetFileName(savedFilePath)}");

                        LoadEncryptedFilesForDecryption(); // Refresh the source list
                        decryptionKey.Clear(); // Clear the key
                    }
                    catch (Exception ex)
                    {
                         ShowError($"Error saving decrypted file or updating database: {ex.Message}");
                          // Consider trying to delete the partially saved file if the DB insert fails
                        if (File.Exists(sfd.FileName))
                        {
                             try { File.Delete(sfd.FileName); } catch { /* Ignore delete error */ }
                        }
                    }
                }
            }
        }

        private void compressButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (fileSelectionCompression.SelectedItem == null) { ShowError("Please select an image file to optimize."); return; }

            // 1. Get Selected File Info
            ComboboxItem selectedFile = (ComboboxItem)fileSelectionCompression.SelectedItem;
            int originalFileId = (int)selectedFile.Value;

            // 2. Get Source File Path and Validate Existence
            string? sourceFilePath = _dbManager.GetFilePath(originalFileId);
            if (string.IsNullOrEmpty(sourceFilePath))
            {
                ShowError($"Source file path is missing for the selected file (ID: {originalFileId}).");
                LoadImageFilesForCompression(); // Refresh list
                return;
            }
            if (!File.Exists(sourceFilePath))
            {
                ShowError($"Source file not found on disk. Path: {sourceFilePath}");
                LoadImageFilesForCompression(); // Refresh list
                return;
            }

            // 3. Prompt User for Save Location and Format
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Optimized Image";
                string originalFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                string originalExtension = Path.GetExtension(sourceFilePath).ToLowerInvariant();

                // Set appropriate filters based on original type
                if (originalExtension == ".bmp")
                {
                    sfd.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|BMP Image (*.bmp)|*.bmp|All Files (*.*)|*.*";
                    sfd.DefaultExt = "png";
                    sfd.FileName = $"{originalFileName}_optimized.png";
                }
                else if (originalExtension == ".jpg" || originalExtension == ".jpeg")
                {
                     sfd.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|All Files (*.*)|*.*"; // Allow saving JPG as PNG too
                     sfd.DefaultExt = "jpg";
                     sfd.FileName = $"{originalFileName}_optimized.jpg";
                }
                 else // Default to PNG options (e.g., if original was PNG or other)
                {
                     sfd.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*"; // Allow saving PNG as JPG
                     sfd.DefaultExt = "png";
                     sfd.FileName = $"{originalFileName}_optimized.png";
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = sfd.FileName; // The path chosen by the user

                    try
                    {
                        // 4. Perform Compression (Optimization)
                        ImageCompressionService.CompressImage(sourceFilePath, destinationFilePath);

                        // 5. Add Record to Database
                        _dbManager.InsertCompressedFileRecord(destinationFilePath, originalFileId);

                        MessageBox.Show("Image optimized and saved successfully!", "Compression Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowImageInNewWindow(destinationFilePath, $"Compressed: {Path.GetFileName(destinationFilePath)}");

                        LoadImageFilesForCompression(); // Refresh source list
                        // Consider also refreshing the decompression list if the UI allows immediate decompression
                        // LoadCompressedFilesForDecompression();
                    }
                    catch (NotSupportedException ex) // Specific exception from CompressImage
                    {
                        ShowError($"Optimization failed: {ex.Message}");
                    }
                    catch (Exception ex) // Catch-all for file IO, DB errors etc.
                    {
                        ShowError($"Image optimization failed: {ex.Message}");
                         // Consider trying to delete the partially saved file if the DB insert fails
                        if (File.Exists(destinationFilePath))
                        {
                             try { File.Delete(destinationFilePath); } catch { /* Ignore delete error */ }
                        }
                    }
                }
            }
        }

        private void decompressButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (fileSelectionDecompression.SelectedItem == null) { ShowError("Please select a compressed file to decompress."); return; }

            // 1. Get Selected Compressed File Info
            CompressionComboboxItem selectedCompressedFile = (CompressionComboboxItem)fileSelectionDecompression.SelectedItem;
            int compressedRecordId = selectedCompressedFile.CompressedFileId; // ID from 'compressed_files' table link
            int compressedFileId = selectedCompressedFile.FileId;       // ID from 'files' table (the compressed file itself)

            // 2. Get Original File Details (for saving correctly)
            int? originalFileId = _dbManager.GetOriginalFileIdFromCompressed(compressedRecordId);
            if (originalFileId == null) { ShowError("Critical error: Cannot find the link to the original file record for this compressed version."); return; }

            string? originalFileType = _dbManager.GetFileType(originalFileId.Value);
            string? originalFileName = _dbManager.GetFileName(originalFileId.Value);
            if (string.IsNullOrEmpty(originalFileType) || string.IsNullOrEmpty(originalFileName))
            {
                ShowError("Critical error: Cannot retrieve original file details (name or type).");
                return;
            }

            // 3. Get Compressed File Path and Validate Existence
            string? compressedFilePath = _dbManager.GetFilePath(compressedFileId);
             if (string.IsNullOrEmpty(compressedFilePath))
            {
                ShowError($"Compressed file path is missing for the selected file (ID: {compressedFileId}).");
                LoadCompressedFilesForDecompression(); // Refresh list
                return;
            }
            if (!File.Exists(compressedFilePath))
            {
                ShowError($"Compressed file not found on disk. Path: {compressedFilePath}");
                LoadCompressedFilesForDecompression(); // Refresh list
                return;
            }

            // 4. Prompt User to Save Decompressed File
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                 sfd.Title = "Save Decompressed File";
                 // Suggest original name + "_decompressed" + original extension
                 sfd.FileName = Path.GetFileNameWithoutExtension(originalFileName) + "_decompressed" + originalFileType;
                 string filterExt = originalFileType.TrimStart('.').ToUpperInvariant();
                 sfd.Filter = $"{filterExt} Files (*{originalFileType})|*{originalFileType}|All Files (*.*)|*.*";
                 sfd.DefaultExt = originalFileType.TrimStart('.');

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = sfd.FileName;

                    try
                    {
                        // 5. Perform Decompression (Re-save)
                        ImageCompressionService.DecompressImage(compressedFilePath, destinationFilePath);

                        // 6. Add Record for the New Decompressed File
                        _dbManager.InsertDerivedFileRecord(destinationFilePath);

                        MessageBox.Show("File decompressed and saved successfully!", "Decompression Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowImageInNewWindow(destinationFilePath, $"Decompressed: {Path.GetFileName(destinationFilePath)}");

                        LoadCompressedFilesForDecompression(); // Refresh the source list
                    }
                    catch (NotSupportedException ex)
                    {
                         ShowError($"Decompression failed: {ex.Message}");
                    }
                    catch (Exception ex) // Catch-all for file IO, DB errors etc.
                    {
                        ShowError($"Decompression failed: {ex.Message}");
                        // Consider trying to delete the partially saved file if the DB insert fails
                        if (File.Exists(destinationFilePath))
                        {
                             try { File.Delete(destinationFilePath); } catch { /* Ignore delete error */ }
                        }
                    }
                }
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

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
                default: return null;
            }
        }

        private void ShowImageInNewWindow(string imagePath, string title)
        {
            if (string.IsNullOrEmpty(imagePath)) return; // No path provided

            try
            {
                if (!File.Exists(imagePath))
                {
                    // Don't show error here, as the primary operation succeeded.
                    // Just log it or silently fail to open viewer.
                    Console.WriteLine($"ImageViewer: File not found at {imagePath}");
                    return;
                }

                // Check for common, viewable image extensions
                string[] supportedExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
                string extension = Path.GetExtension(imagePath).ToLowerInvariant();

                if (!supportedExtensions.Contains(extension))
                {
                    Console.WriteLine($"ImageViewer: File type '{extension}' not supported for viewing ({Path.GetFileName(imagePath)}).");
                    return; // Not a displayable image type
                }

                // Create and show the viewer form (modelessly)
                ImageViewerForm viewer = new ImageViewerForm(imagePath, title);
                viewer.Show(); // Use Show() to not block the main UI
            }
            catch (Exception ex)
            {
                // Show an error specific to the viewer failing, but don't interrupt main flow.
                MessageBox.Show($"Could not open image viewer for '{Path.GetFileName(imagePath)}'.\nReason: {ex.Message}",
                                "Image Viewer Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
