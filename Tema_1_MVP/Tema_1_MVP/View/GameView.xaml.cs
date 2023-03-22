using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public int m_Rows { get; set; }
        public int m_Columns { get; set; }

        public List<MyToggleButton> m_ButtonGrid { get; set; }

        public List<String> m_ImagePaths { get; set; }

        public GameView(string width, string height)
        {
            InitializeComponent();
            DataContext = this;

            m_Rows = int.Parse(width);
            m_Columns = int.Parse(height);

            Game game = new Game(m_Rows, m_Columns);


            // Create a two-dimensional array of buttons
            m_ButtonGrid = game.GetButtonGrid();
            m_ImagePaths = game.GetImages(30);
        }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = (MyToggleButton)sender;
            var index = m_ButtonGrid.IndexOf(toggleButton);

            if (toggleButton.DataContext == toggleButton.GetBack())
            {
                // set the button's content to the second image path
                toggleButton.DataContext = toggleButton.GetFront();
            }
            else
            {
                // set the button's content back to the default image
                toggleButton.DataContext = toggleButton.GetBack();
            }
        }
        private void FlipsCard_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var imagePath = (string)button.DataContext;

            if (!string.IsNullOrEmpty(imagePath))
            {
                var image = new Image { Source = new BitmapImage(new Uri(imagePath)) };
                var contentPresenter = FindVisualChild<ContentPresenter>(button);

                if (contentPresenter != null)
                {
                    contentPresenter.Content = image;
                }
            }
        }
        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T foundChild = FindVisualChild<T>(child);
                    if (foundChild != null)
                        return foundChild;
                }
            }
            return null;
        }
        private void OpenPause_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
