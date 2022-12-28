using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Common
{
    public static class BitmapManager
    {
        public static BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                source.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static Bitmap BitmapSourceToBitmap(BitmapSource source)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(source));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                // return bitmap; <-- leads to problems, stream is closed/closing ...
                return new Bitmap(bitmap);
            }
        }

        public static void BitmapSourceToFile(BitmapSource image, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        private static ImageFormat encodingFormat = ImageFormat.Png;
    
        public static byte[] BitmapToBytes(Bitmap bmp, string encoding = "")
        {
            encoding = encoding.ToLower();
            switch (encoding)
            {
                case "gif":
                    encodingFormat = ImageFormat.Gif;
                    break;
                case "png":
                    encodingFormat = ImageFormat.Png;
                    break;
                case "jpeg":
                    encodingFormat = ImageFormat.Jpeg;
                    break;
                case "tiff":
                    encodingFormat = ImageFormat.Tiff;
                    break;
                default:
                    throw new System.Exception($"Unrecognized image encoding: {encoding}");
            }

            byte[] bytes = null;
            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, encodingFormat);
                bytes = stream.ToArray();
            }

            return bytes;
        }
    }
}
