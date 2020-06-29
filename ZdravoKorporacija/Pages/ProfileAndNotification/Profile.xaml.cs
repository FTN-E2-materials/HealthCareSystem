using Controller.GeneralController;
using Controller.ScheduleController;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    /// 
    
    public class showArticle
    {
        public string show { get; set; }
        public ModelHCI.ArticleHCI article;
    }
    public partial class Profile : Page
    {
        public AppointmentController appointmentController;
        public ArticleController articleController;

        public static ModelHCI.ArticleHCI article;
        public Profile()
        {
            var app = Application.Current as App;
            appointmentController = app.appointmentController;
            articleController = app.articleController;

            InitializeComponent();
            License.Text = MainWindow.doctor.LicenseNumber;
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


            List<showArticle> articles = new List<showArticle>();
            foreach (Article a in articleController.GetArticlesWroteByDoctor(dr)) 
            {
                articles.Add(new showArticle() { show = "Naslov: " + a.PostContent.ContentTitle, 
                    article = new ModelHCI.ArticleHCI() { article = a, content = a.PostContent.Content, 
                        title = a.PostContent.ContentTitle, articlePic = new BitmapImage(new Uri(a.Image, UriKind.Relative)) }
                });
            }

            Objave.ItemsSource = new List<showArticle>();
            Objave.ItemsSource = articles;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/ProfileAndNotification/EditProfile.xaml", UriKind.Relative));
        }

        private void Objave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Objave.SelectedItem != null) {
                
                NavigationService.Navigate(new Uri("Pages/OneArticle.xaml", UriKind.Relative));
                article = ((showArticle)Objave.SelectedItem).article;
                MyEvents.ShowOneArticleEventHandler.RaiseMyCustomEvent(this, new MyEvents.ShowOneEventArgs() { article = ((showArticle)Objave.SelectedItem).article });
            }
        }
    }
}
