using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeGuard.Models
{
    // Represents basic file information, useful for passing data around
    public class FileRecordInfo
    {
        public int FileId { get; set; }
        public string FileName { get; set; } = string.Empty; // Initialize to avoid nulls
        public double FileSizeMB { get; set; }
        public string FileType { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; }

        // Parameterless constructor for easier initialization
        public FileRecordInfo() { }

        // Constructor for quick creation
        public FileRecordInfo(int id, string name, double size, string type, string path, DateTime uploaded)
        {
            FileId = id;
            FileName = name;
            FileSizeMB = size;
            FileType = type;
            FilePath = path;
            UploadedOn = uploaded;
        }
        public FileRecordInfo(string name, double size, string type)
        {
            FileName = name;
            FileSizeMB = size;
            FileType = type;
        }
    }
}