using Controller.GeneralController;
using Controller.ScheduleController;
using Controller.UserController;
using Model.Schedule;
using Model.Users;
using SimsProjekat.Exceptions;
using SimsProjekat.View;
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

namespace ZdravoKorporacija.Pages.ProfileAndNotification
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Page
    {
        public AppointmentController appointmentController;
        public ArticleController articleController;
        public CityController cityController;
        public AddressController addressController;
        public UserController userController;
        public Validations validate;
        public EditProfile()
        {
            var app = Application.Current as App;
            appointmentController = app.appointmentController;
            articleController = app.articleController;
            cityController = app.cityController;
            addressController = app.addressController;
            userController = app.userController;
            validate = app.validations;

            InitializeComponent();
            License.Text = MainWindow.doctor.LicenseNumber;
            //  ProfilePic.Source = MainWindow.
            var dr = MainWindow.doctor;

            if (dr.ProfileImage != null)
                ProfilePic.Source = new BitmapImage(new Uri(dr.ProfileImage, UriKind.Relative));

            Name.Text = MainWindow.doctor.Name + " " + MainWindow.doctor.Surname;
            DateOfBirth.Text = dr.DateOfBirth.ToString("dd.MM.yyyy.");
            
            AgainDate.Text = dr.DateOfBirth.ToString("dd.MM.yyyy.");
            AgainIDNumber.Text = dr.IdentificationNumber;
            AgainPlace.Text = dr.PlaceOfBirth.Name;
            AgainState.Text = dr.PlaceOfBirth.State.Name;

            
            PlceOfBirth.Text = dr.PlaceOfBirth.Name;
            CurrentCity.Text = dr.CurrResidence.City.Name;
            StateOfBirth.Text = dr.PlaceOfBirth.State.Name;
            IDNumber.Text = dr.IdentificationNumber;
            Address.Text = dr.CurrResidence.Street + ", " + dr.CurrResidence.Number.ToString() + ", " + dr.CurrResidence.Floor.ToString() ;
            Email.Text = dr.Email;
            PhoneNumber.Text = dr.Phone;

            Specialization.Text = dr.Specializations[0].SpecializationName;
            DoctorReview.Text = dr.PatientReview.ToString();
            NumberOfExams.Text = appointmentController.GetNumberOfAppointmentsForDoctor(dr, TypeOfAppointment.examination).ToString();
            NumberOfOperations.Text = appointmentController.GetNumberOfAppointmentsForDoctor(dr, TypeOfAppointment.surgery).ToString();

        }


        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/ChangePassword.xaml", UriKind.Relative));
        }

        private void SubmitChange_Click(object sender, RoutedEventArgs e)
        {
            string city = ChangeCurrentCity.Text;
            string address = ChangeAddress.Text;

            string[] parts = city.Split(", ");
            string[] parts2 = address.Split(", ");
            City c = new City(parts[0], new State(parts[1]));

            try
            {

                if (!cityController.CheckIfExists(c))
                {
                    cityController.CreateCity(c);
                }
                else
                {
                    c = cityController.GetCityByName(c);
                }

                Address a = new Address(parts2[0], int.Parse(parts2[1]), 0, 0, c);
                if (addressController.CheckIfExists(a))
                {
                    a = addressController.GetExistentAddress(a);
                }
                else
                {
                    a = addressController.CreateAddress(a);
                }


                var dr = MainWindow.doctor;
                validate.IsEmailValid(ChangeEmail.Text);
                dr.Email = ChangeEmail.Text;
                dr.Phone = ChangePhoneNumber.Text;
                dr.CurrResidence = a;
                userController.UpdateUserProfile(dr);
                MainWindow.doctor = (Doctor)userController.GetUser(MainWindow.doctor.Username);

                NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));

                NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));
            } catch (IncorrectEmailAddress)
            {
                Napomena.Visibility = Visibility.Collapsed;
                Error.Text = "Nevalidan format email adrese!";
                ErrorMessage.Visibility = Visibility.Visible;
            } catch (Exception)
            {
                Napomena.Visibility = Visibility.Collapsed;
                Error.Text = "Došlo je do neke greške!";
                ErrorMessage.Visibility = Visibility.Visible;
            }
        
        }

        private void CancelChange_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));

        }

        private void Gradovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Gradovi.SelectedItem != null)
            {
                ChangeCurrentCity.Text = (string)Gradovi.SelectedItem;
            }
        }

        private void Adrese_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Adrese.SelectedItem != null)
            {
                ChangeAddress.Text = (string)Adrese.SelectedItem;
            }
        }

        private void ChangeAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (ChangeAddress.Text.Equals(""))
            {
                boxic2.Visibility = Visibility.Collapsed;
                Adrese.Visibility = Visibility.Collapsed;
            }

            City city = null;
            List<string> gradovi = new List<string>();
            Adrese.ItemsSource = new List<string>();
            if (!ChangeCurrentCity.Text.Equals(""))
            {
                try
                {
                    string[] parts = ChangeCurrentCity.Text.Split(", ");
                    string citystr = parts[0];
                    City ci = new City() { Name = citystr };
                    city = cityController.GetCityByName(ci);
                } catch (Exception)
                {

                }
            }
            if (city != null)
            {
                foreach (Address address in addressController.GetAdressesByCity(city))
                {
                    if (address.Street.ToLower().Contains(ChangeAddress.Text))
                    {
                        gradovi.Add(address.Street + ", " + address.Number.ToString());
                    }
                }
            }
            else
            {
                foreach (Address address in addressController.GetAll())
                {
                    if (address.Street.ToLower().Contains(ChangeAddress.Text))
                    {
                        gradovi.Add(address.Street + ", " + address.Number);
                    }
                }
            }
            if (gradovi.Count > 0)
            {
                boxic2.Visibility = Visibility.Visible;
                Adrese.Visibility = Visibility.Visible;
            }
            Adrese.ItemsSource = gradovi;
        }

        private void ChangeCurrentCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChangeCurrentCity.Text.Equals(""))
            {
                boxic.Visibility = Visibility.Collapsed;
                Gradovi.Visibility = Visibility.Collapsed;
            }

            List<string> gradovi = new List<string>();
            Gradovi.ItemsSource = new List<string>();
            foreach (City city in cityController.GetAll())
            {
                if (city.Name.ToLower().Contains(ChangeCurrentCity.Text))
                {
                    gradovi.Add(city.Name + ", " + city.State.Name);
                }
            }
            
            if (gradovi.Count > 0)
            {
                boxic.Visibility = Visibility.Visible;
                Gradovi.Visibility = Visibility.Visible;
            }
            Gradovi.ItemsSource = gradovi;
        }

        private void ChangeCurrentCity_LostFocus(object sender, RoutedEventArgs e)
        {
            boxic.Visibility = Visibility.Collapsed;
            Gradovi.Visibility = Visibility.Collapsed;
        }

        private void ChangeAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            boxic2.Visibility = Visibility.Collapsed;
            Adrese.Visibility = Visibility.Collapsed;
        }
    }
}
