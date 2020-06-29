using Controller.ScheduleController;
using Controller.UserController;
using Model.Schedule;
using Model.Users;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public static string ERROR1 = "Pogrešna stara lozinka!";
        public static string ERROR2 = "Nova lozinka ne može biti stara lozinka!";
        public static string ERROR3 = "Ponovni unos lozinke mora biti isti kao i nova lozinka!";
        public UserController userController;
        public AppointmentController appointmentController;
        public ChangePassword()
        {
            var app = Application.Current as App;
            userController = app.userController;
            appointmentController = app.appointmentController;

            InitializeComponent(); License.Text = MainWindow.doctor.LicenseNumber;
            //  ProfilePic.Source = MainWindow.
            var dr = MainWindow.doctor;
            Name.Text = MainWindow.doctor.Name + " " + MainWindow.doctor.Surname;

            if (dr.ProfileImage != null)
                ProfilePic.Source = new BitmapImage(new Uri(dr.ProfileImage, UriKind.Relative));
            DateOfBirth.Text = dr.DateOfBirth.ToString("dd.MM.yyyy.");
            PlceOfBirth.Text = dr.PlaceOfBirth.Name;
            CurrentCity.Text = dr.CurrResidence.City.Name + ", " + dr.CurrResidence.City.State.Name;
            StateOfBirth.Text = dr.PlaceOfBirth.State.Name;
            IDNumber.Text = dr.IdentificationNumber;
            Address.Text = dr.CurrResidence.Street + ", " + dr.CurrResidence.Number.ToString();
            Email.Text = dr.Email;
            PhoneNumber.Text = dr.Phone;

            Specialization.Text = dr.Specializations[0].SpecializationName;
            DoctorReview.Text = dr.PatientReview.ToString();
            NumberOfExams.Text = appointmentController.GetNumberOfAppointmentsForDoctor(dr, TypeOfAppointment.examination).ToString();
            NumberOfOperations.Text = appointmentController.GetNumberOfAppointmentsForDoctor(dr, TypeOfAppointment.surgery).ToString();


        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (OldPass.Password.Equals(MainWindow.doctor.Password))
            {
                if (NewPass.Password.Equals(NewPassAgain.Password))
                {
                    if (NewPass.Password.Equals(OldPass.Password))
                    {

                        ErrorMessage.Visibility = Visibility.Visible;
                        Error.Text = ERROR3;

                    } else
                    {
                        MainWindow.doctor.Password = NewPass.Password;
                        userController.UpdateUserProfile(MainWindow.doctor);
                        MainWindow.doctor = (Doctor)userController.GetUser(MainWindow.doctor.Username);
                        NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));
                    }
                } else {
                    ErrorMessage.Visibility = Visibility.Visible;
                    Error.Text = ERROR3;
                }
            } else {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ERROR1;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));
        }

    }
}
