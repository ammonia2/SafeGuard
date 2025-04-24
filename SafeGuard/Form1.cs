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
using System.Text; // Needed for StringBuilder
using System.Windows.Forms;

namespace SafeGuard
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        private readonly DatabaseManager _dbManager;
        private const string SavedImagesFolderName = "savedimages"; // Subfolder name
        private ImageViewerForm _batchImageViewer = null;
        private const int MaxBatchSize = 10; // Maximum files to process at once
        private bool _limitMessageShown = false; // Flag to show limit message only once per check attempt
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

        private readonly HashSet<string> _supportedImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".png", ".jpg", ".jpeg", ".bmp" // Add more if needed
        };
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
            // --- ENSURE THESE LINES ARE PRESENT ---
            decryptionMethodSelection.Items.Clear(); // Clear any existing items first
            decryptionMethodSelection.Items.Add("AES");
            decryptionMethodSelection.Items.Add("Pixel Scrambling");
            // ---------------------------------------

            // Setup for the File Management dropdown
            cmbTables.Items.Clear();
            cmbTables.Items.Add("All Files");
            cmbTables.Items.Add("Encrypted Files");
            cmbTables.Items.Add("Compressed Files");
            cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged;
        }

        //Helper to update the TextBox display
        private void UpdateSelectionDisplay(CheckedListBox clb, TextBox txtDisplay)
        {
            if (clb == null || txtDisplay == null) return;

            int count = clb.CheckedItems.Count;
            if (count == 0)
            {
                txtDisplay.Text = "No selection";
            }
            else if (count == 1)
            {
                // Display the single selected item's text (might be truncated)
                txtDisplay.Text = clb.CheckedItems[0]?.ToString() ?? "1 file selected";
            }
            else
            {
                txtDisplay.Text = $"{count} files selected";
            }
        }

        // SHARED Click handler for ALL dropdown buttons
        private void btnDropdown_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            CheckedListBox clb = null;

            // Determine which CheckedListBox to toggle based on the button clicked
            if (btn == btnEncryptDropdown) clb = checkedListBoxEncrypt;
            else if (btn == btnDecryptDropdown) clb = checkedListBoxDecrypt;
            else if (btn == btnCompressDropdown) clb = checkedListBoxCompress;
            else if (btn == btnDecompressDropdown) clb = checkedListBoxDecompress;

            if (clb != null)
            {
                clb.Visible = !clb.Visible; // Toggle visibility
                if (clb.Visible)
                {
                    clb.BringToFront(); // Ensure it's drawn on top
                    // Optional: Give focus to the list when opened
                    clb.Focus();
                }
            }
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
                BackColor = Color.Gray,
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

        private void LoadCheckedListBoxData(CheckedListBox clb, Func<List<object>> dataSourceFunc)
        {
            if (_dbManager == null) return;
            clb.Items.Clear(); // Clear existing items
            try
            {
                var items = dataSourceFunc();
                foreach (var item in items)
                {
                    // Add the object itself. The ToString() override will display correctly.
                    clb.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading data for {clb.Name}: {ex.Message}");
            }
        }

        private void LoadImageFilesForEncryption() => LoadCheckedListBoxData(checkedListBoxEncrypt, () => _dbManager.GetImageFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadEncryptedFilesForDecryption() => LoadCheckedListBoxData(checkedListBoxDecrypt, () => _dbManager.GetEncryptedFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadImageFilesForCompression() => LoadCheckedListBoxData(checkedListBoxCompress, () => _dbManager.GetImageFilesForComboBox().ConvertAll(x => (object)x));
        private void LoadCompressedFilesForDecompression() => LoadCheckedListBoxData(checkedListBoxDecompress, () => _dbManager.GetCompressedFilesForComboBox().ConvertAll(x => (object)x));
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
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Encryption
        {
            ShowPanel(panelEncryptionSettings);
            LoadImageFilesForEncryption(); // Use new method
            if (encryptionMethodSelection.Items.Count > 0 && encryptionMethodSelection.SelectedIndex < 0)
                encryptionMethodSelection.SelectedIndex = 0;
            encryptionKeyBox.Clear();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Decryption
        {
            ShowPanel(decryptionPanel);
            LoadEncryptedFilesForDecryption(); // Use new method
            if (decryptionMethodSelection.Items.Count > 0 && decryptionMethodSelection.SelectedIndex < 0)
                decryptionMethodSelection.SelectedIndex = 0;
            decryptionKey.Clear();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Compression
        {
            ShowPanel(compressionPanel);
            LoadImageFilesForCompression(); // Use new method
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Decompression
        {
            ShowPanel(decompressionPanel);
            LoadCompressedFilesForDecompression(); // Use new method
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(panelFileManagement);
            if (cmbTables.Items.Count > 0 && cmbTables.SelectedIndex < 0)
                cmbTables.SelectedIndex = 0;
            else
                LoadAllFilesToGrid();
        }
        private void viewAllFilesButton_Click(object sender, EventArgs e)
        {
            linkLabel4_LinkClicked(sender, null);
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox clb = sender as CheckedListBox;
            if (clb == null) return;

            // --- Selection Limit Logic (Keep as is) ---
            if (e.NewValue == CheckState.Checked)
            {
                if (clb.CheckedItems.Count >= MaxBatchSize)
                {
                    e.NewValue = CheckState.Unchecked;
                    if (!_limitMessageShown)
                    {
                        MessageBox.Show($"You can select a maximum of {MaxBatchSize} files at a time.",
                                       "Selection Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _limitMessageShown = true;
                    }
                    return; // Important: Return here if limit reached, don't schedule update
                }
                else
                {
                    _limitMessageShown = false;
                }
            }
            else
            {
                _limitMessageShown = false;
            }
            // --- End Selection Limit Logic ---


            // --- Schedule UI Update ---
            // Use BeginInvoke because ItemCheck fires *before* the CheckedItems collection updates.
            // This ensures the count/text is correct when UpdateSelectionDisplay runs.
            this.BeginInvoke((MethodInvoker)delegate
            {
                TextBox txt = null;
                if (clb == checkedListBoxEncrypt) txt = txtEncryptSelection;
                else if (clb == checkedListBoxDecrypt) txt = txtDecryptSelection;
                else if (clb == checkedListBoxCompress) txt = txtCompressSelection;
                else if (clb == checkedListBoxDecompress) txt = txtDecompressSelection;

                if (txt != null)
                {
                    UpdateSelectionDisplay(clb, txt);
                }
            });
            // --- End Schedule UI Update ---
        }

        private void checkedListBox_Leave(object sender, EventArgs e)
        {
            CheckedListBox clb = sender as CheckedListBox;
            if (clb != null)
            {
                // Check if focus is moving to the corresponding dropdown button.
                // If so, don't hide the list immediately, let the button click handle it.
                Control focusedControl = this.ActiveControl; // Check which control has focus now
                bool focusMovedToDropdownButton =
                    (clb == checkedListBoxEncrypt && focusedControl == btnEncryptDropdown) ||
                    (clb == checkedListBoxDecrypt && focusedControl == btnDecryptDropdown) ||
                    (clb == checkedListBoxCompress && focusedControl == btnCompressDropdown) ||
                    (clb == checkedListBoxDecompress && focusedControl == btnDecompressDropdown);

                if (!focusMovedToDropdownButton)
                {
                    clb.Visible = false; // Hide the list when focus moves away

                    // Optional: Update display one last time on leave, though BeginInvoke in ItemCheck is usually sufficient
                    //TextBox txt = null;
                    //if (clb == checkedListBoxEncrypt) txt = txtEncryptSelection;
                    //else if (clb == checkedListBoxDecrypt) txt = txtDecryptSelection;
                    //else if (clb == checkedListBoxCompress) txt = txtCompressSelection;
                    //else if (clb == checkedListBoxDecompress) txt = txtDecompressSelection;
                    //if (txt != null) UpdateSelectionDisplay(clb, txt);
                }
            }
        }

        // --- Batch Encrypt Button Click ---
        private void encryptButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (checkedListBoxEncrypt.CheckedItems.Count == 0) { ShowError($"Please select at least one image file (up to {MaxBatchSize}) to encrypt."); return; }
            if (encryptionMethodSelection.SelectedItem == null) { ShowError("Please select an encryption method."); return; }

            string method = encryptionMethodSelection.SelectedItem.ToString() ?? "";
            string keyString = encryptionKeyBox.Text;
            string? outputFolder = null; // To store the chosen output directory

            // 1. Validate Key Format (Once before loop)
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

            // 2. Get Output Folder (Once before loop)
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select Folder to Save Encrypted Files";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    outputFolder = fbd.SelectedPath;
                }
                else
                {
                    MessageBox.Show("Operation cancelled: No output folder selected.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // User cancelled folder selection
                }
            }

            // 3. Process Selected Files
            int successCount = 0;
            List<string> errors = new List<string>();
            List<string> successfulOutputPaths = new List<string>(); // Collect paths for viewer
            Cursor = Cursors.WaitCursor; // Show wait cursor
            checkedListBoxEncrypt.Visible = false; // Hide dropdown

            foreach (object item in checkedListBoxEncrypt.CheckedItems)
            {
                ComboboxItem selectedFile = (ComboboxItem)item;
                int originalFileId = (int)selectedFile.Value;
                string? sourceFilePath = null;
                string originalFileName = selectedFile.Text; // Use text from item
                string destinationPath = null; // Store final path for cleanup/viewer

                try
                {
                    // Get Source Path & Validate
                    sourceFilePath = _dbManager.GetFilePath(originalFileId);
                    if (string.IsNullOrEmpty(sourceFilePath)) throw new FileNotFoundException($"Source path missing in DB for {originalFileName} (ID: {originalFileId}).");
                    if (!File.Exists(sourceFilePath)) throw new FileNotFoundException($"Source file not found on disk: {sourceFilePath}");

                    // Perform Encryption
                    byte[] processedData;
                    string suggestedExtension = (method == "AES") ? ".enc" : ".png"; // Scrambled is PNG

                    if (method == "AES")
                    {
                        byte[] originalData = File.ReadAllBytes(sourceFilePath);
                        processedData = EncryptionService.AesEncrypt(originalData, keyString);
                    }
                    else // Pixel Scrambling
                    {
                        int scrambleSeed = int.Parse(keyString); // Safe parse after validation
                        using (Bitmap originalBitmap = new Bitmap(sourceFilePath))
                        using (Bitmap scrambledBitmap = EncryptionService.PixelScramble(originalBitmap, scrambleSeed))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            scrambledBitmap.Save(ms, ImageFormat.Png);
                            processedData = ms.ToArray();
                        }
                    }

                    // Construct Output Path (with uniqueness check)
                    string outputFileNameBase = Path.GetFileNameWithoutExtension(originalFileName);
                    string outputFileName = $"{outputFileNameBase}_encrypted{suggestedExtension}";
                    destinationPath = Path.Combine(outputFolder, outputFileName);
                    int counter = 1;
                    while (File.Exists(destinationPath))
                    {
                        outputFileName = $"{outputFileNameBase}_encrypted_{counter++}{suggestedExtension}";
                        destinationPath = Path.Combine(outputFolder, outputFileName);
                    }

                    // Write Encrypted Data & Add DB Record
                    File.WriteAllBytes(destinationPath, processedData);
                    _dbManager.InsertEncryptedRecord(originalFileId, keyString, destinationPath);

                    // Collect path for viewer only if Pixel Scrambling (viewable)
                    if (method == "Pixel Scrambling")
                    {
                        successfulOutputPaths.Add(destinationPath); // ADD TO LIST
                    }
                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to encrypt '{originalFileName}': {ex.Message}");
                    // Basic cleanup attempt if file was created before DB insert failed etc.
                    if (destinationPath != null && File.Exists(destinationPath))
                    {
                        try { File.Delete(destinationPath); } catch { /* Ignore delete error */ }
                    }
                }
            } // End foreach loop

            Cursor = Cursors.Default; // Reset cursor

            // 4. Show Summary Feedback
            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"Batch Encryption Complete.");
            summary.AppendLine($"Successfully encrypted: {successCount} file(s).");
            summary.AppendLine($"Failed: {errors.Count} file(s).");
            if (errors.Count > 0)
            {
                summary.AppendLine("\nErrors:");
                errors.ForEach(err => summary.AppendLine($"- {err}"));
                MessageBox.Show(summary.ToString(), "Encryption Summary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(summary.ToString(), "Encryption Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 5. Launch Single Viewer (if any viewable files)
            if (successfulOutputPaths.Any())
            {
                // Close previous viewer instance if it exists and hasn't been closed/disposed
                if (_batchImageViewer != null && !_batchImageViewer.IsDisposed)
                {
                    _batchImageViewer.Close(); // Close will trigger disposal via OnFormClosed
                }
                // Create and show the new viewer with the collected paths
                _batchImageViewer = new ImageViewerForm(successfulOutputPaths, "Encryption Results (Scrambled Images)");
                _batchImageViewer.Show(); // Show modelessly
            }

            // 6. Refresh UI
            LoadImageFilesForEncryption(); // Refresh the source list
            encryptionKeyBox.Clear();
            UpdateSelectionDisplay(checkedListBoxEncrypt, txtEncryptSelection); // Reset display text
        }

        // --- Batch Decrypt Button Click ---
        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (checkedListBoxDecrypt.CheckedItems.Count == 0) { ShowError($"Please select at least one file (up to {MaxBatchSize}) to decrypt."); return; }
            if (decryptionMethodSelection.SelectedItem == null) { ShowError("Please select a decryption method."); return; }

            string method = decryptionMethodSelection.SelectedItem.ToString() ?? "";
            string typedKey = decryptionKey.Text;
            string? outputFolder = null;

            // 1. Get Output Folder
            using (var fbd = new FolderBrowserDialog { /* ... setup ... */ })
            {
                fbd.Description = "Select Folder to Save Decrypted Files";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Operation cancelled: No output folder selected.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                outputFolder = fbd.SelectedPath;
            }

            // 2. Process Selected Files
            int successCount = 0;
            List<string> errors = new List<string>();
            List<string> successfulOutputPaths = new List<string>(); // Collect paths for viewer
            Cursor = Cursors.WaitCursor;
            checkedListBoxDecrypt.Visible = false; // Hide dropdown

            foreach (object item in checkedListBoxDecrypt.CheckedItems)
            {
                ComboBoxEncryptedItem selectedEncryptedFile = (ComboBoxEncryptedItem)item;
                int encryptedRecordId = selectedEncryptedFile.EncryptedId;
                int encryptedFileId = selectedEncryptedFile.FileId;
                string encryptedFileName = selectedEncryptedFile.Text;
                string destinationPath = null; // Store final path
                string originalFileType = ""; // Store original type for viewer check

                try
                {
                    // Validate Key
                    string? storedKey = _dbManager.GetEncryptionKey(encryptedRecordId);
                    if (storedKey == null) throw new KeyNotFoundException($"Could not retrieve stored key for {encryptedFileName}.");
                    if (typedKey != storedKey) throw new ArgumentException($"Incorrect key provided for {encryptedFileName}.");

                    // Get Original File Details
                    int? originalFileId = _dbManager.GetOriginalFileIdFromEncrypted(encryptedRecordId);
                    if (originalFileId == null) throw new Exception($"Cannot find original file link for {encryptedFileName}.");
                    originalFileType = _dbManager.GetFileType(originalFileId.Value);
                    string? originalFileNameBase = _dbManager.GetFileName(originalFileId.Value);
                    if (string.IsNullOrEmpty(originalFileType) || string.IsNullOrEmpty(originalFileNameBase)) throw new Exception($"Cannot retrieve original details for {encryptedFileName}.");


                    // Get Encrypted File Path & Validate
                    string? encryptedFilePath = _dbManager.GetFilePath(encryptedFileId);
                    if (string.IsNullOrEmpty(encryptedFilePath)) throw new FileNotFoundException($"Encrypted path missing in DB for {encryptedFileName} (ID: {encryptedFileId}).");
                    if (!File.Exists(encryptedFilePath)) throw new FileNotFoundException($"Encrypted file not found on disk: {encryptedFilePath}");

                    // Perform Decryption
                    byte[] decryptedData;
                    if (method == "AES")
                    {
                        byte[] encryptedBytes = File.ReadAllBytes(encryptedFilePath);
                        decryptedData = EncryptionService.AesDecrypt(encryptedBytes, typedKey);
                    }
                    else // Pixel Scrambling
                    {
                        int unscrambleSeed = int.Parse(typedKey); // Key already validated
                        using (Bitmap scrambledBitmap = new Bitmap(encryptedFilePath))
                        using (Bitmap unscrambledBitmap = EncryptionService.PixelUnscramble(scrambledBitmap, unscrambleSeed))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ImageFormat originalFormat = GetImageFormatFromExtension(originalFileType) ?? ImageFormat.Png;
                            unscrambledBitmap.Save(ms, originalFormat);
                            decryptedData = ms.ToArray();
                        }
                    }

                    // Construct Output Path (with uniqueness check)
                    string outputFileNameBase = Path.GetFileNameWithoutExtension(originalFileNameBase);
                    string outputFileName = $"{outputFileNameBase}_decrypted{originalFileType}";
                    destinationPath = Path.Combine(outputFolder, outputFileName);
                    int counter = 1;
                    while (File.Exists(destinationPath))
                    {
                        outputFileName = $"{outputFileNameBase}_decrypted_{counter++}{originalFileType}";
                        destinationPath = Path.Combine(outputFolder, outputFileName);
                    }

                    // Write Decrypted Data & Add DB Record
                    File.WriteAllBytes(destinationPath, decryptedData);
                    _dbManager.InsertDerivedFileRecord(destinationPath); // Record the new decrypted file

                    // Collect path for viewer only if the original type was viewable
                    if (_supportedImageExtensions.Contains(originalFileType.ToLowerInvariant()))
                    {
                        successfulOutputPaths.Add(destinationPath); // ADD TO LIST
                    }

                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to decrypt '{encryptedFileName}': {ex.Message}");
                    if (destinationPath != null && File.Exists(destinationPath))
                    {
                        try { File.Delete(destinationPath); } catch { /* Ignore delete error */ }
                    }
                }
            } // End foreach

            Cursor = Cursors.Default;

            // 3. Show Summary Feedback
            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"Batch Decryption Complete.");
            summary.AppendLine($"Successfully decrypted: {successCount} file(s).");
            summary.AppendLine($"Failed: {errors.Count} file(s).");
            if (errors.Count > 0)
            {
                summary.AppendLine("\nErrors:");
                errors.ForEach(err => summary.AppendLine($"- {err}"));
                MessageBox.Show(summary.ToString(), "Decryption Summary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(summary.ToString(), "Decryption Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 4. Launch Single Viewer
            if (successfulOutputPaths.Any())
            {
                if (_batchImageViewer != null && !_batchImageViewer.IsDisposed)
                {
                    _batchImageViewer.Close();
                }
                _batchImageViewer = new ImageViewerForm(successfulOutputPaths, "Decryption Results");
                _batchImageViewer.Show();
            }

            // 5. Refresh UI
            LoadEncryptedFilesForDecryption();
            decryptionKey.Clear();
            UpdateSelectionDisplay(checkedListBoxDecrypt, txtDecryptSelection);
        }

        // --- Batch Compress Button Click ---
        private void compressButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (checkedListBoxCompress.CheckedItems.Count == 0) { ShowError($"Please select at least one image file (up to {MaxBatchSize}) to compress."); return; }

            string? outputFolder = null;

            // 1. Get Output Folder
            using (var fbd = new FolderBrowserDialog { /* ... setup ... */ })
            {
                fbd.Description = "Select Folder to Save Compressed Files";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Operation cancelled: No output folder selected.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                outputFolder = fbd.SelectedPath;
            }

            // 2. Process Selected Files
            int successCount = 0;
            List<string> errors = new List<string>();
            List<string> successfulOutputPaths = new List<string>(); // Collect paths for viewer
            Cursor = Cursors.WaitCursor;
            checkedListBoxCompress.Visible = false; // Hide dropdown

            foreach (object item in checkedListBoxCompress.CheckedItems)
            {
                ComboboxItem selectedFile = (ComboboxItem)item;
                int originalFileId = (int)selectedFile.Value;
                string originalFileName = selectedFile.Text;
                string destinationPath = null; // Store final path

                try
                {
                    // Get Source Path & Validate
                    string? sourceFilePath = _dbManager.GetFilePath(originalFileId);
                    if (string.IsNullOrEmpty(sourceFilePath)) throw new FileNotFoundException($"Source path missing in DB for {originalFileName} (ID: {originalFileId}).");
                    if (!File.Exists(sourceFilePath)) throw new FileNotFoundException($"Source file not found on disk: {sourceFilePath}");

                    // Construct Output Path (with uniqueness check)
                    string originalExtension = Path.GetExtension(originalFileName).ToLowerInvariant();
                    // Simple heuristic: keep PNG/BMP as PNG, others as JPG for compression
                    string targetExtension = (originalExtension == ".bmp" || originalExtension == ".png") ? ".png" : ".jpg";
                    string outputFileNameBase = Path.GetFileNameWithoutExtension(originalFileName);
                    string outputFileName = $"{outputFileNameBase}_compressed{targetExtension}";
                    destinationPath = Path.Combine(outputFolder, outputFileName);
                    int counter = 1;
                    while (File.Exists(destinationPath))
                    {
                        outputFileName = $"{outputFileNameBase}_compressed_{counter++}{targetExtension}";
                        destinationPath = Path.Combine(outputFolder, outputFileName);
                    }

                    // Perform Compression
                    ImageCompressionService.CompressImage(sourceFilePath, destinationPath);

                    // Add DB Record
                    _dbManager.InsertCompressedFileRecord(destinationPath, originalFileId);

                    // Compressed images are always viewable
                    successfulOutputPaths.Add(destinationPath); // ADD TO LIST

                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to compress '{originalFileName}': {ex.Message}");
                    if (destinationPath != null && File.Exists(destinationPath))
                    {
                        try { File.Delete(destinationPath); } catch { /* Ignore delete error */ }
                    }
                }
            } // End foreach

            Cursor = Cursors.Default;

            // 3. Show Summary Feedback
            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"Batch Compression Complete.");
            summary.AppendLine($"Successfully compressed: {successCount} file(s).");
            summary.AppendLine($"Failed: {errors.Count} file(s).");
            if (errors.Count > 0)
            {
                summary.AppendLine("\nErrors:");
                errors.ForEach(err => summary.AppendLine($"- {err}"));
                MessageBox.Show(summary.ToString(), "Compression Summary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(summary.ToString(), "Compression Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 4. Launch Single Viewer
            if (successfulOutputPaths.Any())
            {
                if (_batchImageViewer != null && !_batchImageViewer.IsDisposed)
                {
                    _batchImageViewer.Close();
                }
                _batchImageViewer = new ImageViewerForm(successfulOutputPaths, "Compression Results");
                _batchImageViewer.Show();
            }

            // 5. Refresh UI
            LoadImageFilesForCompression();
            UpdateSelectionDisplay(checkedListBoxCompress, txtCompressSelection);
        }

        // --- Batch Decompress Button Click ---
        private void decompressButton_Click(object sender, EventArgs e)
        {
            if (_dbManager == null) { ShowError("Database connection not initialized."); return; }
            if (checkedListBoxDecompress.CheckedItems.Count == 0) { ShowError($"Please select at least one file (up to {MaxBatchSize}) to decompress."); return; }

            string? outputFolder = null;

            // 1. Get Output Folder
            using (var fbd = new FolderBrowserDialog { /* ... setup ... */ })
            {
                fbd.Description = "Select Folder to Save Decompressed Files";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Operation cancelled: No output folder selected.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                outputFolder = fbd.SelectedPath;
            }

            // 2. Process Selected Files
            int successCount = 0;
            List<string> errors = new List<string>();
            List<string> successfulOutputPaths = new List<string>(); // Collect paths for viewer
            Cursor = Cursors.WaitCursor;
            checkedListBoxDecompress.Visible = false; // Hide dropdown

            foreach (object item in checkedListBoxDecompress.CheckedItems)
            {
                CompressionComboboxItem selectedCompressedFile = (CompressionComboboxItem)item;
                int compressedRecordId = selectedCompressedFile.CompressedFileId;
                int compressedFileId = selectedCompressedFile.FileId;
                string compressedFileName = selectedCompressedFile.Text;
                string destinationPath = null; // Store final path
                string originalFileType = ""; // Store original type for viewer check

                try
                {
                    // Get Original File Details
                    int? originalFileId = _dbManager.GetOriginalFileIdFromCompressed(compressedRecordId);
                    if (originalFileId == null) throw new Exception($"Cannot find original file link for {compressedFileName}.");
                    originalFileType = _dbManager.GetFileType(originalFileId.Value);
                    string? originalFileNameBase = _dbManager.GetFileName(originalFileId.Value);
                    if (string.IsNullOrEmpty(originalFileType) || string.IsNullOrEmpty(originalFileNameBase)) throw new Exception($"Cannot retrieve original details for {compressedFileName}.");

                    // Get Compressed File Path & Validate
                    string? compressedFilePath = _dbManager.GetFilePath(compressedFileId);
                    if (string.IsNullOrEmpty(compressedFilePath)) throw new FileNotFoundException($"Compressed path missing in DB for {compressedFileName} (ID: {compressedFileId}).");
                    if (!File.Exists(compressedFilePath)) throw new FileNotFoundException($"Compressed file not found on disk: {compressedFilePath}");

                    // Construct Output Path (with uniqueness check)
                    string outputFileNameBase = Path.GetFileNameWithoutExtension(originalFileNameBase);
                    string outputFileName = $"{outputFileNameBase}_decompressed{originalFileType}";
                    destinationPath = Path.Combine(outputFolder, outputFileName);
                    int counter = 1;
                    while (File.Exists(destinationPath))
                    {
                        outputFileName = $"{outputFileNameBase}_decompressed_{counter++}{originalFileType}";
                        destinationPath = Path.Combine(outputFolder, outputFileName);
                    }

                    // Perform Decompression
                    ImageCompressionService.DecompressImage(compressedFilePath, destinationPath);

                    // Add DB Record
                    _dbManager.InsertDerivedFileRecord(destinationPath); // Record the new decompressed file

                    // Collect path for viewer only if the resulting type is viewable
                    if (_supportedImageExtensions.Contains(originalFileType.ToLowerInvariant()))
                    {
                        successfulOutputPaths.Add(destinationPath); // ADD TO LIST
                    }

                    successCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to decompress '{compressedFileName}': {ex.Message}");
                    if (destinationPath != null && File.Exists(destinationPath))
                    {
                        try { File.Delete(destinationPath); } catch { /* Ignore delete error */ }
                    }
                }
            } // End foreach

            Cursor = Cursors.Default;

            // 3. Show Summary Feedback
            StringBuilder summary = new StringBuilder();
            summary.AppendLine($"Batch Decompression Complete.");
            summary.AppendLine($"Successfully decompressed: {successCount} file(s).");
            summary.AppendLine($"Failed: {errors.Count} file(s).");
            if (errors.Count > 0)
            {
                summary.AppendLine("\nErrors:");
                errors.ForEach(err => summary.AppendLine($"- {err}"));
                MessageBox.Show(summary.ToString(), "Decompression Summary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(summary.ToString(), "Decompression Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 4. Launch Single Viewer
            if (successfulOutputPaths.Any())
            {
                if (_batchImageViewer != null && !_batchImageViewer.IsDisposed)
                {
                    _batchImageViewer.Close();
                }
                _batchImageViewer = new ImageViewerForm(successfulOutputPaths, "Decompression Results");
                _batchImageViewer.Show();
            }

            // 5. Refresh UI
            LoadCompressedFilesForDecompression();
            UpdateSelectionDisplay(checkedListBoxDecompress, txtDecompressSelection);
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

        // Keep this method if you still want single-image viewing from the grid double-click
        private void ShowImageInNewWindow(string imagePath, string title)
        {
            if (string.IsNullOrEmpty(imagePath)) return; // No path provided

            try
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"ImageViewer: File not found at {imagePath}");
                    return;
                }

                string extension = Path.GetExtension(imagePath).ToLowerInvariant();

                if (!_supportedImageExtensions.Contains(extension))
                {
                    Console.WriteLine($"ImageViewer: File type '{extension}' not supported for viewing ({Path.GetFileName(imagePath)}).");
                    return; // Not a displayable image type
                }

                // --- CHANGE HERE ---
                // Create a list containing only the single path
                List<string> singleImageList = new List<string> { imagePath };

                // Close previous viewer if it exists (optional, maybe you want multiple single viewers?)
                // If you want only one viewer EVER, manage the _batchImageViewer reference here too.
                // For simplicity now, let's allow multiple single viewers from the grid.
                if (_batchImageViewer != null && !_batchImageViewer.IsDisposed)
                {
                    // Decide: close the batch viewer if a single image is opened?
                    // _batchImageViewer.Close();
                }

                // Call the constructor with the LIST
                ImageViewerForm viewer = new ImageViewerForm(singleImageList, title);
                viewer.Show();
                // --- END CHANGE ---
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open image viewer for '{Path.GetFileName(imagePath)}'.\nReason: {ex.Message}",
                                "Image Viewer Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
