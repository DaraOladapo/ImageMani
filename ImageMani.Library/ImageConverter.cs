using System;
using System.IO;

namespace ImageMani.Library
{
    public static class ImageConverter
    {
        public static string ConvertImageToBase64(string FileName)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(FileName))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
        public static byte[] ConvertFromBase64ToByteArray(string baseString)
        {
            byte[] imageBytes = Convert.FromBase64String(baseString);
            return imageBytes;
        }
        private static System.Drawing.Image ConvertImageFromBase64(string baseString)
        {
            byte[] imageBytes = Convert.FromBase64String(baseString);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}
