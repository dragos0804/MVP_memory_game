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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MemoryTiles
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window, INotifyPropertyChanged
    {
        private static int currentAvatarIndex = 0;
        private static readonly int noOfAvatarImages = 8;
        private string currentAvatarPath;

        public UserList UserList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public NewUserWindow(UserList userList)
        {
            UserList = userList;
            currentAvatarPath = $"assets/avatar{currentAvatarIndex}.png";
            InitializeComponent();
        }

        public string CurrentAvatarPath
        {
            get => currentAvatarPath;
            set
            {
                currentAvatarPath = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            currentAvatarIndex--;
            if (currentAvatarIndex < 0)
            {
                currentAvatarIndex += noOfAvatarImages;
            }
            CurrentAvatarPath = $"assets/avatar{currentAvatarIndex}.png";
        }

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            currentAvatarIndex++;
            currentAvatarIndex %= noOfAvatarImages;
            CurrentAvatarPath = $"assets/avatar{currentAvatarIndex}.png";
        }

        private void Button_AddUser_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            if (username == "")
            {
                _ = MessageBox.Show("Please enter a username!");
                return;
            }
            if (UserList.FindUsername(username))
            {
                _ = MessageBox.Show("This username is taken. Please enter another one!");
                return;
            }
            UserList.AddUser(new User(UserList.Users.Count, username, $"assets/avatar{currentAvatarIndex}.png"));
            UserListSerializationActions.SerializeUserList(UserList);
            _ = MessageBox.Show("The user was created successfully!");
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void lblOpenLoginView(object sender, MouseButtonEventArgs e)
        {
            LogInWindow loginView = new LogInWindow();
            this.Close();
            loginView.Show();
        }
    }
}
