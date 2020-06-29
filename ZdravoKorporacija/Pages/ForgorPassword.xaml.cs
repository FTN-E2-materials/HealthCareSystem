using System;
using System.Collections.Generic;
using System.Net.Mail;
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

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for ForgorPassword.xaml
    /// </summary>
    public partial class ForgorPassword : Page
    {
        public ForgorPassword()
        {
            InitializeComponent();
        }

        private void VratiMeNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(email.Text))
            {
                Email.Visibility = Visibility.Collapsed;
                Error.Visibility = Visibility.Collapsed;
                Vratime.Visibility = Visibility.Visible;
            } else
            {
                Error.Text = "Nevalidni format email adrese!";
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                if (emailaddress.Equals(""))
                {
                    return false;
                }

                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
