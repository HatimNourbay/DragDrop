using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        Dictionary<int,BitmapImage> dictionaryOfImages = new Dictionary<int,BitmapImage>();

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

                var x = bit.Width;
                var y = bit.Height;
                var ratio = x / y;


                Image ima = new Image();

                if (x > this.Width)
                {
                    newHeightDone = this.Width/ratio;
                    bitmap = new TransformedBitmap(bit, new ScaleTransform(this.Width / bit.PixelWidth, newHeightDone / bit.PixelHeight));
                    ima.Source = bitmap;
                }


                ima.Source = bit;               

                CanvasDrop.Children.Add(ima);
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
