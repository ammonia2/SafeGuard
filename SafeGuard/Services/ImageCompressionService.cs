using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SafeGuard.Services
{
    public static class ImageCompressionService
    {
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
                            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 85L);
                        }
                        break;

                    case ".png":
                        targetFormat = ImageFormat.Png;
                        // PNG compression options via EncoderParameters are limited/less effective
                        // than specialized PNG optimization tools. Saving normally is sufficient.
                        // encoder = GetEncoder(targetFormat);
                        // encoderParams = new EncoderParameters(1);
                        // encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW); // Example, may vary
                        break;

                    case ".bmp":
                        targetFormat = ImageFormat.Bmp;
                        break;

                    default:
                        throw new NotSupportedException($"Saving to '{destinationExtension}' format is not currently supported.");
                }

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