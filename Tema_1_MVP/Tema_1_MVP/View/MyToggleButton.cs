using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace Tema_1_MVP.View
{
    public class MyToggleButton : ToggleButton
    {
        private Image imageFront { get; set; }
        private Image imageBack { get; set; }

        public Image GetFront()
        {
            return imageFront;
        }

        public Image GetBack()
        {
            return imageBack;
        }

        public MyToggleButton(string front)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("/Assets/MemoryCards/backCard.png", UriKind.RelativeOrAbsolute));
            imageBack = new Image();
            imageBack = image;
        }

        public void SetImages(string front)
        {
            Image image2 = new Image();
            image2.Source = new BitmapImage(new Uri(front, UriKind.RelativeOrAbsolute));
            imageFront = new Image();
            imageFront = image2;
        }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the content between imageBack and imageFront
            if (Content == imageBack)
            {
                Content = imageFront;
            }
            else
            {
                Content = imageBack;
            }
        }
    }
}
