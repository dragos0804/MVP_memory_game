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

namespace MemoryTiles
{
    /// <summary>
    /// Interaction logic for CustomBoardWindow.xaml
    /// </summary>
    public partial class CustomBoardWindow : Window
    {
        public MainWindow ParentWindow { get; set; }

        public CustomBoardWindow(MainWindow parentWindow)
        {
            InitializeComponent();
            ParentWindow = parentWindow;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (heightTextBox.Text == "" || widthTextBox.Text == "")
            {
                _ = MessageBox.Show("Enter values for height and width!");
                return;
            }
            try
            {
                int height = int.Parse(heightTextBox.Text);
                int width = int.Parse(widthTextBox.Text);
                if (height * width > 30)
                {
                    _ = MessageBox.Show("The board can't be bigger than 30 tiles!");
                    return;
                }
                ParentWindow.Board = new Board(ParentWindow.LoggedInUser.Username, height, width);
                _ = MessageBox.Show($"Every new game you begin from now on will have a {height}x{width} board!");
            }
            catch (FormatException)
            {
                _ = MessageBox.Show("Height and width should be numbers!");
            }
            this.Close();
        }
    }
}
