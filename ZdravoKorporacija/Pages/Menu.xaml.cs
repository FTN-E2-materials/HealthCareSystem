using Controller.GeneralController;
using Controller.ScheduleController;
using MaterialDesignThemes.Wpf;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    /// 

    
    public class NotificationHCI
    {
        public string text { get; set; }
        public string category { get; set; }
        public PackIcon icon { get; set; }
        public NotificationHCI() { }
        
    }
    public partial class Menu : Page
    {
        public NotificationController notificationController;
        public AppointmentController appointmentController;

        public static bool demoOn = false;
        public Menu()
        {
            var app = Application.Current as App;
            appointmentController = app.appointmentController;
            notificationController = app.notificationController;

            InitializeComponent();
            MyEvents.CloseLanguage.CustomEvent += closeLanguage;
            ModelHCI.AppointmentsData appointments = new ModelHCI.AppointmentsData();

            List<NotificationHCI> notifications = new List<NotificationHCI>();
            foreach (Notification n in notificationController.GetNotificationsForUser(MainWindow.doctor.Username))
            {
                NotificationHCI notification = new NotificationHCI();
                notification.text = n.ContentOfNotification;
                notification.category = n.NotificationCategory.ToString();
                if (n.NotificationCategory == NotificationCategory.BLOG)
                {
                    notification.icon = new PackIcon() { Kind = PackIconKind.Blogger };
                }
                else if (n.NotificationCategory == NotificationCategory.MEDICATION)
                {
                    notification.icon = new PackIcon() { Kind = PackIconKind.Pill };
                }
                else if (n.NotificationCategory == NotificationCategory.SCHEDULE)
                {
                    notification.icon = new PackIcon() { Kind = PackIconKind.Schedule };
                }

                notifications.Add(notification);
            }


            NotificationsList.ItemsSource = notifications;

        }

        private void closeLanguage(object sender, EventArgs e)
        {
            LanguageBox.Children.Clear();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!demoOn)
            {
                Labela_Bolnice.Visibility = Visibility.Visible;
                ButtonCloseMenu.Visibility = Visibility.Visible;
            }
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            if (!demoOn)
            {
                Side_Menu.SelectedItem = null;
                Frame_Change.Navigate(new Uri("Pages/ProfileAndNotification/Profile.xaml", UriKind.Relative));
                Section_Label.Text = "Profil";
            }
        }

        private void Logout_From_PopUp_Click(object sender, RoutedEventArgs e)
        {
            if (!demoOn)
            {
                MainWindow.loggedInDoctor = null;
                NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
            }
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            Labela_Bolnice.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Side_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   if (!demoOn)
            {
                if (Side_Menu != null && Frame_Change != null)
                {
                    if (Schedule_Item.IsSelected)
                    {
                        if (GridMenu.Width == 200)
                        {
                            ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                        Frame_Change.Navigate(new Uri("Pages/Schedule.xaml", UriKind.Relative));
                        Section_Label.Text = "Raspored";
                    }
                    else if (Appointments.IsSelected)
                    {
                        if (GridMenu.Width == 200)
                        {
                            ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                        if (appointmentController.NotFinishedByDoctorAndDay(MainWindow.doctor, DateTime.Today).ToList().Count == 0)
                            Frame_Change.Navigate(new Uri("Pages/NemaPregleda.xaml", UriKind.Relative));
                        else
                            Frame_Change.Navigate(new Uri("Pages/Appointments.xaml", UriKind.Relative));
                        Section_Label.Text = "Pregledi";
                    }
                    else if (Patients.IsSelected)
                    {
                        if (GridMenu.Width == 200)
                        {
                            ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                        Frame_Change.Navigate(new Uri("Pages/Patients.xaml", UriKind.Relative));
                        Section_Label.Text = "Pacijenti";
                    }
                    else if (Medications.IsSelected)
                    {
                        if (GridMenu.Width == 200)
                        {
                            ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                        Frame_Change.Navigate(new Uri("Pages/Medications.xaml", UriKind.Relative));
                        Section_Label.Text = "Lekovi";
                    }
                    else if (Blog.IsSelected)
                    {
                        if (GridMenu.Width == 200)
                        {
                            ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                        Frame_Change.Navigate(new Uri("Pages/Blog.xaml", UriKind.Relative));
                        Section_Label.Text = "Blog";
                    }
                }
            }
        }

        private void Logout_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!demoOn)
            {
                if (Logout_Menu != null)
                {
                    if (Logout.IsSelected)
                    {

                        MainWindow.loggedInDoctor = null;
                        NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
                    }
                }
            }
        }

        private void Notifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!demoOn)
            {
                if (NotificationsList.SelectedItem != null)
                {
                    NotificationHCI notification = (NotificationHCI)NotificationsList.SelectedItem;

                    if (notification.category.Equals(NotificationCategory.MEDICATION.ToString()))
                    {
                        Frame_Change.Navigate(new Uri("Pages/Medications.xaml", UriKind.Relative));
                    }
                    else if (notification.category.Equals(NotificationCategory.SCHEDULE.ToString()))
                    {
                        Frame_Change.Navigate(new Uri("Pages/Schedule.xaml", UriKind.Relative));
                    }
                    else if (notification.category.Equals(NotificationCategory.BLOG.ToString()))
                    {
                        Frame_Change.Navigate(new Uri("Pages/Blog.xaml", UriKind.Relative));
                    }
                }
            }
        }

        private void VacationRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!demoOn)
            {
                Side_Menu.SelectedItem = null;
                Frame_Change.Navigate(new Uri("Pages/VacationRequest.xaml", UriKind.Relative));
                Section_Label.Text = "Zahtev za odmor";
            }
        }

        private void Archive_Click(object sender, RoutedEventArgs e)
        {
            Side_Menu.SelectedItem = null;
            Frame_Change.Navigate(new Uri("Pages/Archive.xaml", UriKind.Relative));
            Section_Label.Text = "Arhiva";
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            if (!demoOn)
            {
                Side_Menu.SelectedItem = null;
                Frame_Change.Navigate(new Uri("Pages/Feedback.xaml", UriKind.Relative));
                Section_Label.Text = "Feedback";
            }
        }

        private void Language_Click(object sender, RoutedEventArgs e)
        {
            if (!demoOn)
            {
                LanguageBox.Children.Clear();
                LanguageBox.Children.Add(new Language());
                Section_Label.Text = "Jezik";
            }
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            demoOn = true;
            Side_Menu.SelectedIndex = 1;
            Frame_Change.Navigate(new Uri("Pages/Appointments.xaml", UriKind.Relative));
            MyEvents.DemoEvents.AlreadyInAppointmetns.RaiseMyCustomEvent(this, new EventArgs());
            
            
        }
    }
}
