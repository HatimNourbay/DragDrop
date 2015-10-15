using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragDrop2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double newHeightDone = 0;

        TransformedBitmap bitmap;

        Dictionary<double,System.Drawing.Image> dictionaryOfImages = new Dictionary<double,System.Drawing.Image>();

        Dictionary<int, int> RemainingSize = new Dictionary<int, int>();

        private List<BitmapImage> myListImages = null;

        public List<BitmapImage> MyListImages
        {
            get { return myListImages; }
            set
            {
                myListImages = value;
            }
        }

        int numberImages = 0;

        public int NumberImages
        {
            get { return numberImages; }
            set
            {
                numberImages = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FrameDrop_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];


            
            //MyListImages.Add(bit);
            if (IsImageExtension(filename))
            {
                NumberImages++;
                
                var uriN = new Uri(filename);
                Console.WriteLine(uriN);
                BitmapImage bit = new BitmapImage(uriN);
                Bitmap bit2;

                System.Windows.Controls.Image Finalima = new System.Windows.Controls.Image();

                System.Drawing.Image ima = System.Drawing.Image.FromFile(filename);

                var x = bit.Width;
                var y = bit.Height;
                var ratio = x / y;

                if (dictionaryOfImages != null)
                {
                    foreach (var images in dictionaryOfImages)
                    {
                        ImageResizeClass.ResizeImage(images.Value,800, 600);
                    }
                }


                newHeightDone = this.Width / ratio;
                //bitmap = new TransformedBitmap(bit, new ScaleTransform(this.Width / bit.PixelWidth, newHeightDone / bit.PixelHeight));
                var bonjour = ImageResizeClass.ResizeImage(ima, Convert.ToInt32(this.Width), Convert.ToInt32(newHeightDone));
                //var bit = new BitmapImage(bonjour);
                bit2 = new Bitmap(bonjour);

                using (MemoryStream memory = new MemoryStream())
                {
                    bit2.Save(memory, ImageFormat.Png);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();


                    Finalima.Source = bitmapImage;
                    dictionaryOfImages.Add(ratio, bonjour);
                    CanvasDrop.Children.Add(Finalima);

                }


            }
        }



        


        private static string[] _validExtensions = { ".jpg", ".bmp", ".gif", ".png" }; //  etc

        private bool IsImageExtension(string ext)
        {
            foreach (var valm in _validExtensions)
            {
                if (ext.EndsWith(valm))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
