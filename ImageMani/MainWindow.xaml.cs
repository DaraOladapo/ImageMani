using ImageMani.Library;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

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
            ofd.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg";
            ofd.Multiselect = false;
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
                ImageToBase64Label.Text = ImageConverter.ConvertImageToBase64(ofd.FileName);
            }
        }
        private void OnConvertTextToImage(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(ImageConverter.ConvertFromBase64ToByteArray(ImageToBase64Label.Text));
            bitmap.EndInit();
            ConvertedImage.Source = bitmap;
        }
    }
}
