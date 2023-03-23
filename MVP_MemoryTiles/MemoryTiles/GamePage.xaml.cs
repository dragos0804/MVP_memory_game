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

using System.Threading;

namespace MemoryTiles
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Tuple<int, int> selectedPosition;
        private List<Image> images;
        private int clickCount;

        public MainWindow ParentWindow { get; set; }
        public Board Board { get; set; }
        public List<List<Button>> Buttons { get; set; }

        public GamePage(Board board, MainWindow parentWindow)
        {
            InitializeComponent();

            ClearSelectedPosition();
            images = new List<Image>();
            clickCount = 0;

            ParentWindow = parentWindow;

            // board settings
            Board = board;
            Board.InProgress = true;
            if (!Board.IsSaved)
            {
                Board.Initialize();
                Board.IncreaseLevel();
            }
            Board.IsSaved = false;

            ParentWindow.levelLabel.Content = $"Level {Board.Level}";

            Loaded += delegate
            {
                double boardSizeOnScreen = Math.Min(ActualHeight, ActualWidth) - 20.0;
                double tileSize = boardSizeOnScreen / Math.Max(Board.Height, Board.Width);
                tilesGrid.Height = tileSize * Board.Height;
                tilesGrid.Width = tileSize * Board.Width;
            };

            // grid settings
            for (int i = 0; i < board.Height; i++)
            {
                tilesGrid.RowDefinitions.Add(new RowDefinition());
                tilesGrid.RowDefinitions[i].Height = new GridLength(1.0, GridUnitType.Star);
            }
            for (int i = 0; i < board.Width; i++)
            {
                tilesGrid.ColumnDefinitions.Add(new ColumnDefinition());
                tilesGrid.ColumnDefinitions[i].Width = new GridLength(1.0, GridUnitType.Star);
            }
            Buttons = new List<List<Button>>();
            for (int rowIndex = 0; rowIndex < board.Height; rowIndex++)
            {
                List<Button> buttonRow = new List<Button>();
                for (int columnIndex = 0; columnIndex < board.Width; columnIndex++)
                {
                    Button button = new Button
                    {
                        Background = new SolidColorBrush(Color.FromArgb(0xff, 0xbe, 0xe7, 0xfd)),
                        BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x74, 0xa7, 0xcd))
                    };
                    button.Click += Button_Click;
                    Grid.SetRow(button, rowIndex);
                    Grid.SetColumn(button, columnIndex);
                    if (Board.Tiles[rowIndex][columnIndex].IsGuessed)
                    {
                        button.Visibility = Visibility.Hidden;
                    }
                    _ = tilesGrid.Children.Add(button);
                    buttonRow.Add(button);
                }
                Buttons.Add(buttonRow);
            }
        }

        private void ClearSelectedPosition()
        {
            selectedPosition = new Tuple<int, int>(-1, -1);
        }

        private bool PositionIsSelected()
        {
            return !(selectedPosition.Item1 == selectedPosition.Item2 && selectedPosition.Item2 == -1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (clickCount >= 2)
            {
                foreach (Image img in images)
                {
                    tilesGrid.Children.Remove(img);
                }
                images.Clear();
                clickCount = 0;
            }
            clickCount++;

            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);
            Image image = new Image
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/{Board.Tiles[row][column].ImagePath}"))
            };
            Grid.SetRow(image, row);
            Grid.SetColumn(image, column);
            _ = tilesGrid.Children.Add(image);
            images.Add(image);


            if (!PositionIsSelected())
            {
                selectedPosition = new Tuple<int, int>(row, column);
                return;
            }
            if (row == selectedPosition.Item1 && column == selectedPosition.Item2)
            {
                return;
            }
            if (Board.Tiles[row][column].PairId == Board.Tiles[selectedPosition.Item1][selectedPosition.Item2].PairId)
            {
                Board.Tiles[row][column].IsGuessed = true;
                Board.Tiles[selectedPosition.Item1][selectedPosition.Item2].IsGuessed = true;
                Buttons[row][column].Visibility = Visibility.Hidden;
                Buttons[selectedPosition.Item1][selectedPosition.Item2].Visibility = Visibility.Hidden;

                Board.TilePairsGuessed++;

                // check if level is done or game is over
                if (Board.TilePairsGuessed == Board.Height * Board.Width / 2)
                {
                    if (Board.Level == 3)
                    {
                        _ = MessageBox.Show("Congratulations, you won the game!");
                        ParentWindow.levelLabel.Content = "";
                        ParentWindow.mainFrame.Content = null;
                        ParentWindow.LoggedInUser.GamesWon++;
                        UserListSerializationActions.SerializeUserList(ParentWindow.UserList);
                        Board.InProgress = false;
                        Board.Level = 0;
                        return;
                    }
                    ParentWindow.mainFrame.Content = new GamePage(Board, ParentWindow);
                }
            }
            ClearSelectedPosition();
        }
    }
}
