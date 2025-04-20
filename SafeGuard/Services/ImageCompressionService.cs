using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SafeGuard.Services
{
    public static class ImageCompressionService
    {
        /// <summary>
        /// Compresses (optimizes) an image by saving it in a specified format with quality settings.
        /// </summary>
        /// <param name="sourceFilePath">Path to the original image.</param>
        /// <param name="destinationFilePath">Path to save the optimized image.</param>
        public static void CompressImage(string sourceFilePath, string destinationFilePath)
        {
            // Load the original image
            using (Bitmap originalBitmap = new Bitmap(sourceFilePath))
            {
                string destinationExtension = Path.GetExtension(destinationFilePath).ToLowerInvariant();
                ImageFormat targetFormat;
                ImageCodecInfo? encoder = null; // Use nullable type
                EncoderParameters? encoderParams = null; // Use nullable type

                switch (destinationExtension)
                {
                    case ".jpg":
                    case ".jpeg":
                        targetFormat = ImageFormat.Jpeg;
                        encoder = GetEncoder(targetFormat);
                        if (encoder != null)
                        {
                            encoderParams = new EncoderParameters(1);
                            // Use a reasonable quality setting like 85L
                            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 85L);
                        }
                        break;

                    case ".png":
                        targetFormat = ImageFormat.Png;
                        // PNG compression options via EncoderParameters are limited/less effective
                        // than specialized PNG optimization tools. Saving normally is usually sufficient.
                        // encoder = GetEncoder(targetFormat);
                        // encoderParams = new EncoderParameters(1);
                        // encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW); // Example, may vary
                        break;

                    case ".bmp":
                        targetFormat = ImageFormat.Bmp;
                        // BMP is generally uncompressed
                        break;

                    // Add cases for other formats like GIF, TIFF if needed
                    // case ".gif": targetFormat = ImageFormat.Gif; break;

                    default:
                        throw new NotSupportedException($"Saving to '{destinationExtension}' format is not currently supported.");
                }

                // Save the image
                if (encoder != null && encoderParams != null)
                {
                    originalBitmap.Save(destinationFilePath, encoder, encoderParams);
                }
                else
                {
                    // Save using the format directly if no specific encoder/params are needed (or found)
                    originalBitmap.Save(destinationFilePath, targetFormat);
                }
            }
        }

        /// <summary>
        /// "Decompresses" an image - essentially loads a potentially compressed format
        /// and saves it, potentially to a different format or with maximum quality settings.
        /// </summary>
        /// <param name="compressedFilePath">Path to the (potentially) compressed image.</param>
        /// <param name="outputFilePath">Path to save the decompressed/re-saved image.</param>
        public static void DecompressImage(string compressedFilePath, string outputFilePath)
        {
             using (Bitmap sourceBitmap = new Bitmap(compressedFilePath))
            {
                string destExtension = Path.GetExtension(outputFilePath).ToLowerInvariant();
                ImageFormat targetFormat;
                ImageCodecInfo? encoder = null;
                EncoderParameters? encoderParams = null;

                switch (destExtension)
                {
                    case ".jpg":
                    case ".jpeg":
                        targetFormat = ImageFormat.Jpeg;
                        encoder = GetEncoder(targetFormat);
                        if (encoder != null)
                        {
                            encoderParams = new EncoderParameters(1);
                            // Save with maximum quality when "decompressing" to JPEG
                            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        }
                        break;

                    case ".png":
                        targetFormat = ImageFormat.Png;
                        // No specific quality settings needed for PNG decompression
                        break;

                    case ".bmp":
                        targetFormat = ImageFormat.Bmp;
                        break;

                     case ".gif":
                        targetFormat = ImageFormat.Gif;
                        break;

                    default:
                         // Fallback to saving as PNG if extension is unknown/unsupported for saving
                        // or throw an exception if strict format adherence is required.
                        targetFormat = ImageFormat.Png;
                        outputFilePath = Path.ChangeExtension(outputFilePath, ".png"); // Ensure extension matches format
                        // Alternatively: throw new NotSupportedException($"Cannot decompress/save to '{destExtension}'.");
                        break;
                }

                if (encoder != null && encoderParams != null)
                {
                    sourceBitmap.Save(outputFilePath, encoder, encoderParams);
                }
                else
                {
                    sourceBitmap.Save(outputFilePath, targetFormat);
                }
            }
        }


        // Helper method to get encoder info
        private static ImageCodecInfo? GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
    }
}