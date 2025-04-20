using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text; // For Encoding

namespace SafeGuard.Services
{
    public static class EncryptionService
    {
        // --- AES ---
        public static byte[] AesEncrypt(byte[] data, string keyString)
        {
            if (string.IsNullOrEmpty(keyString) || (keyString.Length != 16 && keyString.Length != 32))
            {
                 throw new ArgumentException("AES key must be 16 or 32 characters long.");
            }

            byte[] key = Encoding.UTF8.GetBytes(keyString);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV(); // Generate a new IV for each encryption
                byte[] iv = aes.IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    // Prepend the IV to the ciphertext
                    ms.Write(iv, 0, iv.Length);

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        // CryptoStream must be flushed/closed to finalize encryption
                    } // cs.Dispose() implicitly calls FlushFinalBlock

                    return ms.ToArray();
                }
            }
        }

        public static byte[] AesDecrypt(byte[] encryptedDataWithIv, string keyString)
        {
             if (string.IsNullOrEmpty(keyString) || (keyString.Length != 16 && keyString.Length != 32))
            {
                 throw new ArgumentException("AES key must be 16 or 32 characters long.");
            }
            byte[] key = Encoding.UTF8.GetBytes(keyString);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                int ivSize = aes.BlockSize / 8; // Typically 16 bytes

                if (encryptedDataWithIv == null || encryptedDataWithIv.Length <= ivSize)
                {
                    throw new ArgumentException("Encrypted data is missing or too short to contain IV.", nameof(encryptedDataWithIv));
                }

                // Extract the IV from the beginning of the data
                byte[] iv = new byte[ivSize];
                Array.Copy(encryptedDataWithIv, 0, iv, 0, iv.Length);

                using (MemoryStream ms = new MemoryStream()) // Stream to hold decrypted data
                {
                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, iv))
                    // Pass the ciphertext (data *after* the IV) to the CryptoStream for reading
                    using (MemoryStream cipherStream = new MemoryStream(encryptedDataWithIv, iv.Length, encryptedDataWithIv.Length - iv.Length))
                    using (CryptoStream cs = new CryptoStream(cipherStream, decryptor, CryptoStreamMode.Read))
                    {
                        cs.CopyTo(ms); // Read decrypted data from CryptoStream into MemoryStream
                    }
                    return ms.ToArray();
                }
            }
        }


        // --- Pixel Scrambling ---
        public static Bitmap PixelScramble(Bitmap bmp, int seed)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Bitmap scrambled = new Bitmap(width, height, bmp.PixelFormat); // Match original pixel format
            Random rng = new Random(seed);

            List<Point> points = Enumerable.Range(0, width * height)
                                           .Select(i => new Point(i % width, i / width))
                                           .OrderBy(p => rng.Next())
                                           .ToList();

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = null;
            BitmapData scrambledData = null;

            try
            {
                bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
                scrambledData = scrambled.LockBits(rect, ImageLockMode.WriteOnly, scrambled.PixelFormat);

                int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
                long byteCount = (long)bmpData.Stride * height; // Use long for potentially large images

                // Check if byteCount exceeds Int32.MaxValue if using byte arrays
                if (byteCount > Int32.MaxValue)
                    throw new NotSupportedException("Image is too large to process with byte arrays in this implementation.");

                byte[] pixels = new byte[(int)byteCount];
                byte[] scrambledPixels = new byte[(int)byteCount];

                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixels, 0, (int)byteCount);

                int currentPointIndex = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        long originalOffset = (long)y * bmpData.Stride + (long)x * bytesPerPixel;

                        Point targetPoint = points[currentPointIndex++];
                        long scrambledOffset = (long)targetPoint.Y * scrambledData.Stride + (long)targetPoint.X * bytesPerPixel;

                        // Ensure offsets are within bounds (though they should be)
                        if (originalOffset + bytesPerPixel <= pixels.Length && scrambledOffset + bytesPerPixel <= scrambledPixels.Length)
                        {
                            Buffer.BlockCopy(pixels, (int)originalOffset, scrambledPixels, (int)scrambledOffset, bytesPerPixel);
                        }
                        // Else: handle error or skip pixel if calculation is wrong (shouldn't happen with correct logic)
                    }
                }
                System.Runtime.InteropServices.Marshal.Copy(scrambledPixels, 0, scrambledData.Scan0, (int)byteCount);
            }
            finally
            {
                if (bmpData != null) bmp.UnlockBits(bmpData);
                if (scrambledData != null) scrambled.UnlockBits(scrambledData);
            }

            return scrambled;
        }

         public static Bitmap PixelUnscramble(Bitmap scrambledBmp, int seed)
        {
            int width = scrambledBmp.Width;
            int height = scrambledBmp.Height;
            Bitmap original = new Bitmap(width, height, scrambledBmp.PixelFormat); // Match scrambled pixel format
            Random rng = new Random(seed);

            // Generate the exact same shuffled sequence of target points as during scrambling
            List<Point> points = Enumerable.Range(0, width * height)
                                           .Select(i => new Point(i % width, i / width))
                                           .OrderBy(p => rng.Next())
                                           .ToList();


            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData scrambledData = null;
            BitmapData originalData = null;

            try
            {
                 scrambledData = scrambledBmp.LockBits(rect, ImageLockMode.ReadOnly, scrambledBmp.PixelFormat);
                 originalData = original.LockBits(rect, ImageLockMode.WriteOnly, original.PixelFormat);

                int bytesPerPixel = Image.GetPixelFormatSize(scrambledBmp.PixelFormat) / 8;
                long byteCount = (long)scrambledData.Stride * height;

                if (byteCount > Int32.MaxValue)
                     throw new NotSupportedException("Image is too large to process with byte arrays in this implementation.");

                byte[] scrambledPixels = new byte[(int)byteCount];
                byte[] originalPixels = new byte[(int)byteCount];

                System.Runtime.InteropServices.Marshal.Copy(scrambledData.Scan0, scrambledPixels, 0, (int)byteCount);

                int currentPointIndex = 0;
                 // Iterate through the ORIGINAL pixel locations (x, y)
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                         // Find where the pixel for (x, y) *came from* in the scrambled image
                        Point sourcePoint = points[currentPointIndex++]; // This point holds the location in the scrambled image

                        long originalOffset = (long)y * originalData.Stride + (long)x * bytesPerPixel;
                        long scrambledOffset = (long)sourcePoint.Y * scrambledData.Stride + (long)sourcePoint.X * bytesPerPixel;

                        if (originalOffset + bytesPerPixel <= originalPixels.Length && scrambledOffset + bytesPerPixel <= scrambledPixels.Length)
                        {
                           Buffer.BlockCopy(scrambledPixels, (int)scrambledOffset, originalPixels, (int)originalOffset, bytesPerPixel);
                        }
                    }
                }
                System.Runtime.InteropServices.Marshal.Copy(originalPixels, 0, originalData.Scan0, (int)byteCount);
            }
             finally
            {
                 if (scrambledData != null) scrambledBmp.UnlockBits(scrambledData);
                 if (originalData != null) original.UnlockBits(originalData);
            }
            return original;
        }
    }
}