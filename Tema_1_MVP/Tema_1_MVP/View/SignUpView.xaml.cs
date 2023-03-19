using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static System.Net.Mime.MediaTypeNames;
using Tema_1_MVP.Model;

namespace Tema_1_MVP.View
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        private SignUpModel signUpModel;

        public SignUpView()
        {
            InitializeComponent();

            // Create a list of image paths
            List<string> imagePaths = new List<string>();
            for (int i = 0; i <= 13; i++)
                imagePaths.Add("/Assets/Avatars/" + i.ToString() + ".png");

            // Create an instance of the ImageModel class
            signUpModel = new SignUpModel(imagePaths);
            // Set the source of the Image control to the first image
            UpdateImage();
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

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // Call the NextImage method to increment the current index
            signUpModel.NextImage();

            // Update the Image control with the new image
            UpdateImage();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            // Call the PrevImage method to decrement the current index
            signUpModel.PrevImage();

            // Update the Image control with the new image
            UpdateImage();
        }
        private void UpdateImage()
        {
            // Set the source of the Image control to the current image path
            string imagePath = signUpModel.CurrentImagePath;

            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            MyImage.Source = bitmapImage;
        }

        private void lblOpenLoginView(object sender, MouseButtonEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }
    }
}
