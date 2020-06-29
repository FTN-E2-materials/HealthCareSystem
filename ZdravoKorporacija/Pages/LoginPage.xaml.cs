using Controller.UserController;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimsProjekat.Exceptions;
using SimsProjekat.SIMS.Exceptions;
using Model.Users;

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public UserController userController;
        public LoginPage()
        {
            InitializeComponent();
            var app = Application.Current as App;
            userController = app.userController;

        }


        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/ForgorPassword.xaml", UriKind.Relative));
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            ModelHCI.Doctor dc = null ;
            string errorMessage = "";
            bool flag = true;
            bool drExists = false;

            if (password.Equals("") || username.Equals(""))
            {
                errorMessage = "Morate popuniti sva polja!";
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = errorMessage;
            }
            else
            {
                try
                {
                    MainWindow.doctor = (Doctor) userController.Login(username, password);
                    drExists = true;
                } catch (InvalidPassword ex)
                {
                    errorMessage = "Neispravna lozinka!";
                    flag = false;
                } catch (EntityNotFound ex2)
                {
                    errorMessage = ex2.Message;
                } 
                MainWindow.loggedInDoctor = ModelHCI.DoctorData.allDoctors[0];
                if (drExists && flag)
                {
                    NavigationService.Navigate(new Uri("Pages/Menu.xaml", UriKind.Relative));
                }
                else
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                    Error.Text = errorMessage;
                }
            }
        }

    }
}
