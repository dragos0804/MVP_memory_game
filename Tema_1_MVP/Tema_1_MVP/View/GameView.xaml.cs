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
using System.Windows.Shapes;
using Tema_1_MVP.Logic;

namespace Tema_1_MVP.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public List<int> ButtonGrid { get; set; }

        public GameView(string width, string height)
        {
            InitializeComponent();
            DataContext = this;

            Rows = int.Parse(width);
            Columns = int.Parse(height);

            Game game= new Game();
            

            // Create a two-dimensional array of buttons
            ButtonGrid = Enumerable.Range(1, Rows * Columns).ToList();
        }

        private void Flips_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Image image = button.Content as Image;
            BitmapImage defaultImage = (BitmapImage)Resources["DefaultImage"];
            BitmapImage pressedImage = (BitmapImage)Resources["PressedImage"];

            if (image.Source == defaultImage)
            {
                image.Source = pressedImage;
            }
            else
            {
                image.Source = defaultImage;
            }
        }

        private void OpenPause_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
