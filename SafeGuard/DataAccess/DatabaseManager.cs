using MySql.Data.MySqlClient;
using SafeGuard.Models; // Use our models
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SafeGuard.DataAccess
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // --- Schema Management ---

        public void EnsureDatabaseSchema()
        {
            EnsureFileTableExists();
            EnsureEncryptedTableExists();
            EnsureCompressedFilesTableExists();
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
            ExecuteNonQuery(createTableSql);
        }

        private void EnsureEncryptedTableExists()
        {
            string createTableSql = @"
            CREATE TABLE IF NOT EXISTS `encrypted` (
              `encrypted_id` INT AUTO_INCREMENT PRIMARY KEY,
              `file_id` INT NOT NULL,                 -- FK to the 'files' table entry for the ENCRYPTED file
              `encryption_key` VARCHAR(255) NOT NULL,
              `encrypted_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
              `original_file_id` INT NOT NULL,        -- FK to the 'files' table entry for the ORIGINAL file
              FOREIGN KEY (`file_id`) REFERENCES `files` (`file_id`) ON DELETE CASCADE,
              FOREIGN KEY (`original_file_id`) REFERENCES `files` (`file_id`) ON DELETE CASCADE
            );";
            ExecuteNonQuery(createTableSql);
        }

        private void EnsureCompressedFilesTableExists()
        {
            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS `compressed_files` (
                  `compressed_file_id` INT AUTO_INCREMENT PRIMARY KEY,
                  `file_id` INT NOT NULL,               -- FK to the 'files' table entry for the COMPRESSED file
                  `original_file_id` INT NOT NULL,      -- FK to the 'files' table entry for the ORIGINAL file
                  `compressed_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                  FOREIGN KEY (`file_id`) REFERENCES `files` (`file_id`) ON DELETE CASCADE,
                  FOREIGN KEY (`original_file_id`) REFERENCES `files` (`file_id`) ON DELETE CASCADE
                );";
            ExecuteNonQuery(createTableSql);
        }

        // --- Data Insertion ---

        public long InsertFileRecord(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            double fileSizeMB = Math.Round(fi.Length / (1024.0 * 1024.0), 2);
            string fileName = fi.Name;
            string extension = fi.Extension;

            string query = @"INSERT INTO files (file_name, file_size, file_type, file_path, uploaded_on)
                             VALUES (@name, @size, @type, @path, NOW());
                             SELECT LAST_INSERT_ID();"; // Get the ID back

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", fileName);
                cmd.Parameters.AddWithValue("@size", fileSizeMB);
                cmd.Parameters.AddWithValue("@type", extension);
                cmd.Parameters.AddWithValue("@path", filePath);
                conn.Open();
                // ExecuteScalar returns the first column of the first row (the ID)
                object result = cmd.ExecuteScalar();
                return Convert.ToInt64(result);
            }
        }

         public void InsertEncryptedRecord(int originalFileId, string encryptionKey, string encryptedFilePath)
        {
            // 1. Insert the encrypted file itself into the 'files' table
            long newEncryptedFileRecordId = InsertFileRecord(encryptedFilePath); // Re-use InsertFileRecord

            if (newEncryptedFileRecordId <= 0)
            {
                throw new Exception("Failed to insert the encrypted file record into the 'files' table.");
            }

            // 2. Insert the relationship into the 'encrypted' table
            string insertEncryptedQuery = @"
                INSERT INTO encrypted (file_id, encryption_key, original_file_id, encrypted_on)
                VALUES (@fid, @ekey, @origFileId, NOW())";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmdEnc = new MySqlCommand(insertEncryptedQuery, conn))
            {
                cmdEnc.Parameters.AddWithValue("@fid", newEncryptedFileRecordId); // ID of the file record for the encrypted file
                cmdEnc.Parameters.AddWithValue("@ekey", encryptionKey);
                cmdEnc.Parameters.AddWithValue("@origFileId", originalFileId);   // ID of the original file
                conn.Open();
                cmdEnc.ExecuteNonQuery();
            }
        }

        public void InsertCompressedFileRecord(string compressedFilePath, int originalFileId)
        {
            // 1. Insert record for the compressed file into the 'files' table
            long newCompressedFileRecordId = InsertFileRecord(compressedFilePath); // Re-use InsertFileRecord

             if (newCompressedFileRecordId <= 0)
            {
                throw new Exception("Failed to insert the compressed file record into the 'files' table.");
            }

            // 2. Insert record into the 'compressed_files' table to link original and compressed
            string insertCompressedLinkQuery = @"
            INSERT INTO compressed_files (file_id, original_file_id, compressed_on)
            VALUES (@compressed_file_id, @original_file_id, NOW())";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmdLink = new MySqlCommand(insertCompressedLinkQuery, conn))
            {
                cmdLink.Parameters.AddWithValue("@compressed_file_id", newCompressedFileRecordId); // The ID of the file record we just created
                cmdLink.Parameters.AddWithValue("@original_file_id", originalFileId);      // The ID of the file it was compressed FROM
                conn.Open();
                cmdLink.ExecuteNonQuery();
            }
        }

         // Insert record for a file that resulted from DECRYPTION or DECOMPRESSION
        // It doesn't need a link table, just goes into 'files'
        public long InsertDerivedFileRecord(string filePath)
        {
             return InsertFileRecord(filePath); // Same logic as inserting any new file
        }


        // --- Data Retrieval ---

        public List<FileRecordInfo> GetRecentFiles(int limit = 3)
        {
            var files = new List<FileRecordInfo>();
            string query = $"SELECT file_name, file_size, file_type FROM files ORDER BY uploaded_on DESC LIMIT {limit}";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        files.Add(new FileRecordInfo(
                            reader.GetString("file_name"),
                            reader.GetDouble("file_size"),
                            reader.GetString("file_type")
                        ));
                    }
                }
            }
            return files;
        }

        public DataTable GetFilesDataTable(string tableType)
        {
            string query = "";
            switch (tableType)
            {
                case "All Files":
                    query = @"SELECT
                         file_id, file_name, file_size, file_type, file_path, uploaded_on
                         FROM files ORDER BY uploaded_on DESC";
                    break;
                case "Encrypted Files":
                    query = @"SELECT
                         e.encrypted_id, e.file_id AS encrypted_entry_file_id,
                         f.file_name AS encrypted_file_name, f.file_path AS encrypted_file_path,
                         e.encryption_key, e.encrypted_on, e.original_file_id
                         FROM encrypted e JOIN files f ON e.file_id = f.file_id
                         ORDER BY e.encrypted_on DESC";
                    break;
                case "Compressed Files":
                     query = @"SELECT
                         cf.compressed_file_id, cf.file_id AS compressed_entry_file_id,
                         f_comp.file_name AS compressed_file_name, f_comp.file_size AS compressed_file_size,
                         f_comp.file_path AS compressed_file_path, cf.original_file_id,
                         f_orig.file_name AS original_file_name, cf.compressed_on
                         FROM compressed_files cf
                         JOIN files f_comp ON cf.file_id = f_comp.file_id
                         JOIN files f_orig ON cf.original_file_id = f_orig.file_id
                         ORDER BY cf.compressed_on DESC";
                    break;
                default:
                    return new DataTable(); // Return empty table if type is unknown
            }

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public List<ComboboxItem> GetImageFilesForComboBox()
        {
             var items = new List<ComboboxItem>();
             // Added '.bmp'
             string query = "SELECT file_id, file_name FROM files WHERE file_type IN ('.png','.jpg','.jpeg', '.bmp')";
             using (MySqlConnection conn = new MySqlConnection(_connectionString))
             using (MySqlCommand cmd = new MySqlCommand(query, conn))
             {
                 conn.Open();
                 using (MySqlDataReader reader = cmd.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         items.Add(new ComboboxItem
                         {
                             Text = reader.GetString("file_name"),
                             Value = reader.GetInt32("file_id")
                         });
                     }
                 }
             }
             return items;
        }

        public List<ComboBoxEncryptedItem> GetEncryptedFilesForComboBox()
        {
            var items = new List<ComboBoxEncryptedItem>();
            string query = @"SELECT e.encrypted_id, f.file_id, f.file_name
                             FROM encrypted e JOIN files f ON e.file_id = f.file_id
                             ORDER BY e.encrypted_on DESC";
             using (MySqlConnection conn = new MySqlConnection(_connectionString))
             using (MySqlCommand cmd = new MySqlCommand(query, conn))
             {
                 conn.Open();
                 using (MySqlDataReader reader = cmd.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         items.Add(new ComboBoxEncryptedItem
                         {
                             EncryptedId = reader.GetInt32("encrypted_id"),
                             FileId = reader.GetInt32("file_id"), // ID of the file record representing the encrypted file
                             Text = reader.GetString("file_name") // Name of the encrypted file
                         });
                     }
                 }
             }
             return items;
        }

        public List<CompressionComboboxItem> GetCompressedFilesForComboBox()
        {
            var items = new List<CompressionComboboxItem>();
            string query = @"SELECT cf.compressed_file_id, cf.file_id, f.file_name
                             FROM compressed_files cf JOIN files f ON cf.file_id = f.file_id
                             ORDER BY cf.compressed_on DESC";
             using (MySqlConnection conn = new MySqlConnection(_connectionString))
             using (MySqlCommand cmd = new MySqlCommand(query, conn))
             {
                 conn.Open();
                 using (MySqlDataReader reader = cmd.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         items.Add(new CompressionComboboxItem
                         {
                             CompressedFileId = reader.GetInt32("compressed_file_id"),
                             FileId = reader.GetInt32("file_id"), // ID of the file record representing the compressed file
                             Text = reader.GetString("file_name") // Name of the compressed file
                         });
                     }
                 }
             }
             return items;
        }

        public string? GetFilePath(int fileId)
        {
            return GetSingleStringValue("SELECT file_path FROM files WHERE file_id = @id LIMIT 1", "@id", fileId);
        }

        public string? GetFileName(int fileId)
        {
            return GetSingleStringValue("SELECT file_name FROM files WHERE file_id = @id LIMIT 1", "@id", fileId);
        }

        public string? GetFileType(int fileId)
        {
             return GetSingleStringValue("SELECT file_type FROM files WHERE file_id = @id LIMIT 1", "@id", fileId);
        }

        public string? GetEncryptionKey(int encryptedId)
        {
             return GetSingleStringValue("SELECT encryption_key FROM encrypted WHERE encrypted_id = @encId LIMIT 1", "@encId", encryptedId);
        }

         public int? GetOriginalFileIdFromEncrypted(int encryptedId)
        {
            return GetSingleIntValue("SELECT original_file_id FROM encrypted WHERE encrypted_id = @encId LIMIT 1", "@encId", encryptedId);
        }

        public int? GetOriginalFileIdFromCompressed(int compressedFileId)
        {
            return GetSingleIntValue("SELECT original_file_id FROM compressed_files WHERE compressed_file_id = @cfId LIMIT 1", "@cfId", compressedFileId);
        }


        // --- Helper Methods ---
        private void ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string? GetSingleStringValue(string query, string paramName, object paramValue)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(paramName, paramValue);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        private int? GetSingleIntValue(string query, string paramName, object paramValue)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(paramName, paramValue);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return null;
            }
        }
    }
}