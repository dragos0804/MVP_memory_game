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

namespace Tema_1_MVP.View
{
    public partial class NewGameView : Window
    {
        public NewGameView()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StartNewGame(object sender, RoutedEventArgs e) 
        {
            GameView gameView = new GameView(ComboboxDim1.SelectedItem.ToString(), ComboboxDim2.SelectedItem.ToString());
            this.Close();
            gameView.Show();
        }
    }
}
