using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeGuard.Models
{
    // to store both display text & ID for general ComboBoxes
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; } // Can be int or other types
        public override string ToString() => Text; // Ensures ComboBox displays Text
    }

    public class ComboBoxEncryptedItem
    {
        public int EncryptedId { get; set; }
        public int FileId { get; set; } // ID of the encrypted file record in 'files' table
        public string Text { get; set; } // File name to display
        public override string ToString() => Text;
    }

    // Helper class specific to the decompression dropdown
    public class CompressionComboboxItem
    {
        public string Text { get; set; } // Compressed file name
        public int FileId { get; set; } // ID of the compressed file record in 'files' table
        public int CompressedFileId { get; set; } // ID from the 'compressed_files' table
        public override string ToString() => Text;
    }
}
