using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;

namespace MemoryTiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User LoggedInUser { get; set; }
        public UserList UserList { get; set; }
        public Board Board { get; set; }

        public MainWindow(UserList userList, User loggedInUser)
        {
            UserList = userList;
            LoggedInUser = loggedInUser;
            Board = new Board(LoggedInUser.Username);
            InitializeComponent();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (Board.InProgress)
            {
                if (MessageBox.Show("The game in progress will be considered lost. Are you sure you want to exit the game?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();
            Close();
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            if (Board.InProgress)
            {
                if (MessageBox.Show("The game in progress will be considered lost. Are you sure you want to exit the game?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            levelLabel.Content = "";
            mainFrame.Source = new Uri("AboutPage.xaml", UriKind.Relative);
        }

        private void MenuItem_Standard_Click(object sender, RoutedEventArgs e)
        {
            if (Board.InProgress)
            {
                _ = MessageBox.Show("You can't change the board size during a game!");
                return;
            }
            Board.Height = 5;
            Board.Width = 5;
            _ = MessageBox.Show("Every new game you begin from now on will have a 5x5 board!");
        }

        private void MenuItem_Custom_Click(object sender, RoutedEventArgs e)
        {
            if (Board.InProgress)
            {
                _ = MessageBox.Show("You can't change the board size during a game!");
                return;
            }
            CustomBoardWindow window = new CustomBoardWindow(this);
            window.Show();
        }

        private void MenuItem_NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (Board.InProgress)
            {
                if (MessageBox.Show("The game in progress will be considered lost. Are you sure you want to start a new game?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            Board.Level = 0;
            mainFrame.Content = new GamePage(Board, this);
            LoggedInUser.GamesPlayed++;
            UserListSerializationActions.SerializeUserList(UserList);
        }
        private void MenuItem_SaveGame_Click(object sender, RoutedEventArgs e)
        {
            if (!Board.InProgress)
            {
                _ = MessageBox.Show("This option is available only after starting a new game!");
                return;
            }
            if (File.Exists($"board_{LoggedInUser.Username}.xml"))
            {
                if (MessageBox.Show("You already have a saved game that will be overwritten. Are you sure you want to save the game?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            BoardSerializationActions.SerializeBoard(Board);
            _ = MessageBox.Show("The game was saved!");
            mainFrame.Content = null;
            levelLabel.Content = "";
            Board.InProgress = false;
        }

        private void MenuItem_OpenGame_Click(object sender, RoutedEventArgs e)
        {
            if (Board.InProgress)
            {
                if (MessageBox.Show("The game in progress will be considered lost. Are you sure you want to open the saved game?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            string filename = $"board_{LoggedInUser.Username}.xml";
            if (!File.Exists(filename))
            {
                _ = MessageBox.Show("You haven't saved any games!");
                return;
            }
            Board = BoardSerializationActions.DeserializeBoard(filename);
            File.Delete(filename);
            Board.IsSaved = true;
            mainFrame.Content = new GamePage(Board, this);
        }
    }
}
