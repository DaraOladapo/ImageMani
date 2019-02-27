using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Environment;

namespace ImageMani
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void OnOpenAndConvertToText(object sender, RoutedEventArgs e)
        {
            ofd.FileOk += Ofd_FileOk;
            ofd.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.jpg)|*.png|JPEG files (*.jpg)|*.jpeg";
            ofd.Multiselect = false;
            ofd.InitialDirectory = GetFolderPath(SpecialFolder.MyPictures);
            ofd.ShowDialog();
        }

        private void Ofd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ofd.CheckFileExists)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(ofd.FileName);
                bitmap.EndInit();
                LoadedImage.Source = bitmap;
                ImageToBase64Label.Text = ConvertImageToBase64();
            }

        }

        private string ConvertImageToBase64()
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(ofd.FileName))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
        private void OnConvertTextToImage(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(ConvertFromBase64ToByteArray(ImageToBase64Label.Text));
            bitmap.EndInit();
            ConvertedImage.Source = bitmap;
        }
        private byte[] ConvertFromBase64ToByteArray(string baseString)
        {
            byte[] imageBytes = Convert.FromBase64String(baseString);
            return imageBytes;
        }
        private System.Drawing.Image ConvertImageFromBase64(string baseString)
        {
            byte[] imageBytes = Convert.FromBase64String(baseString);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}
