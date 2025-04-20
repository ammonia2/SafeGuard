using SafeGuard.DataAccess;
using SafeGuard.Models;
using SafeGuard.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SafeGuard
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        private readonly DatabaseManager _dbManager;

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
                _dbManager = new DatabaseManager(string.Empty);
                this.Text += " (DATABASE CONNECTION FAILED)";
            }

            SetupComboBoxes();
            LoadRecentFiles();

            panelContent.Visible = true;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;
            compressionPanel.Visible = false;
            decompressionPanel.Visible = false;
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
    }
}
