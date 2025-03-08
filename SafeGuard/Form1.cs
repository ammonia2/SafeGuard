using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;

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

            LoadRecentFiles();
            fileSelectionEncryption.MaxDropDownItems = 5;
            fileSelectionEncryption.DropDownHeight = 100;

            cmbTables.SelectedIndexChanged += cmbTables_SelectedIndexChanged; // event handler
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
                query = @"SELECT 
                     encrypted_id, 
                     file_id, 
                     encryption_key, 
                     encrypted_on, 
                     original_file_id 
                  FROM encrypted 
                  ORDER BY encrypted_id DESC";
            }

            // display in DataGridView
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewFiles.DataSource = dt;
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

            encryptionMethodSelection.SelectedIndex = 0;
            LoadImageFileNames();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;
            panelFileManagement.Visible = false;

            panelContent.Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            panelFileManagement.Visible = false;

            decryptionPanel.Visible = true;
            LoadEncryptedFileNames();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;

            cmbTables.SelectedIndex = 0;
            panelFileManagement.Visible = true;
            LoadAllFiles();
        }

        private void viewAllFilesButton_Click(object sender, EventArgs e)
        {
            panelContent.Visible = false;
            panelEncryptionSettings.Visible = false;
            decryptionPanel.Visible = false;

            panelFileManagement.Visible = true;
            cmbTables.SelectedIndex = 0;
            LoadAllFiles();
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
                string query = "SELECT file_id, file_name FROM files " +
                               "WHERE file_type IN ('.png','.jpg','.jpeg')";
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

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (decryptionFileSelection.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an encrypted file first.");
                return;
            }

            ComboBoxEncryptedItem selectedItem = (ComboBoxEncryptedItem)decryptionFileSelection.SelectedItem;
            int encryptedId = selectedItem.EncryptedId;
            int fileId = selectedItem.FileId;

            if (decryptionMethodSelection.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a decryption method.");
                return;
            }
            string method = decryptionMethodSelection.SelectedItem.ToString();
            if (method != "AES")
            {
                MessageBox.Show("Unsupported method: " + method);
                return;
            }

            // Validate the decryption key
            string typedKey = decryptionKey.Text.Trim();
            // example check to see if length is 16 or 32
            if (typedKey.Length != 16 && typedKey.Length != 32)
            {
                MessageBox.Show("Decryption Key must be 16 or 32 characters (128/256-bit).");
                return;
            }

            string storedKey = GetStoredEncryptionKey(encryptedId);
            if (storedKey == null)
            {
                MessageBox.Show("Could not find encryption record for the selected file.");
                return;
            }

            if (typedKey != storedKey)
            {
                MessageBox.Show("Incorrect decryption key.");
                return;
            }

            // Finding the original file ID (i.e., what was encrypted in the first place)
            int? origFileId = GetOriginalFileId(encryptedId);
            if (origFileId == null)
            {
                MessageBox.Show("No original_file_id found. Cannot determine original extension.");
                return;
            }

            // Finding that original extension
            string originalExtension = GetOriginalFileExtension(origFileId.Value);
            if (string.IsNullOrEmpty(originalExtension))
            {
                // if none
                originalExtension = ".dat";
            }

            // Load the encrypted file from disk
            string encryptedFilePath = GetFilePathFromDB(fileId);
            if (string.IsNullOrEmpty(encryptedFilePath) || !File.Exists(encryptedFilePath))
            {
                MessageBox.Show("Encrypted file not found on disk!");
                return;
            }
            byte[] encryptedBytes = File.ReadAllBytes(encryptedFilePath);

            // Decryption
            byte[] decryptedData;
            try
            {
                decryptedData = AesDecrypt(encryptedBytes, typedKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Decryption failed: " + ex.Message);
                return;
            }

            // Prompt user to choose save location
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Decrypted File";
                // Provide a filter that defaults to the original extension:
                sfd.Filter = $"{originalExtension.ToUpper().TrimStart('.')} files|*{originalExtension}|All Files|*.*";
                // Provide a default file name if you want
                sfd.FileName = "DecryptedFile" + originalExtension;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, decryptedData);

                    InsertDecryptedFileRecord(sfd.FileName);
                    MessageBox.Show("File decrypted and saved successfully!");
                }
            }
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

            if (encryptionMethodSelection.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an encryption method.");
                return;
            }
            string method = encryptionMethodSelection.SelectedItem.ToString();

            string keyString = encryptionKeyBox.Text.Trim();
            if (keyString.Length != 16 && keyString.Length != 32)
            {
                MessageBox.Show("Encryption Key must be 16 or 32 characters (128/256-bit).");
                return;
            }

            // Read image file from disk
            byte[] originalData = File.ReadAllBytes(filePath);

            // Encrypt the data
            byte[] encryptedData;
            try
            {
                encryptedData = AesEncrypt(originalData, keyString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed: " + ex.Message);
                return;
            }

            // Prompt user to choose save location
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Encrypted File";
                sfd.Filter = "All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Write the encrypted bytes to chosen file
                    File.WriteAllBytes(sfd.FileName, encryptedData);

                    InsertEncryptedRecord(originalFileId, keyString, sfd.FileName);

                    MessageBox.Show("File encrypted and saved successfully!");
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

    }
}
