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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MemoryTiles
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public UserList UserList { get; set; }
        private User selectedUser;

        public LogInWindow()
        {
            UserList = new UserList();
            UserListSerializationActions.DeserializeUserList(UserList);
            InitializeComponent();
        }

        private void Button_NewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(UserList);
            newUserWindow.Show();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = usersGrid.SelectedItem as User;
            deleteUserButton.IsEnabled = true;
            playButton.IsEnabled = true;
            avatarPicture.Fill = selectedUser != null
                ? new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/{selectedUser.AvatarPath}")))
                : new ImageBrush();
        }

        private void Button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("This action can't be undone. Are you sure you want to delete the user?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            string filename = $"board_{selectedUser.Username}.xml";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            UserList.DeleteUser(selectedUser);
            UserListSerializationActions.SerializeUserList(UserList);
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(UserList, selectedUser);
            mainWindow.Show();
            Close();
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
            Application.Current.Shutdown();
        }
        private void lblOpenSignUpView(object sender, MouseButtonEventArgs e)
        {
            NewUserWindow signUpView = new NewUserWindow(UserList);
            this.Close();
            signUpView.Show();
        }
    }
}
