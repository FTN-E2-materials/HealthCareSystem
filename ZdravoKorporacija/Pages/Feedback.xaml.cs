using Controller.GeneralController;
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

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Feedback.xaml
    /// </summary>
    /// 

    public class FeedbackKlasica
    {
        public int opcije { get; set; }
        public int ocenasoft { get; set; }
        public int snalazenje { get; set; }
        public int raspored { get; set; }
        public string content { get; set; }
    }
    public partial class LeaveFeedback : Page
    {
        public FeedbackController feedbackController;

        public LeaveFeedback()
        {
            var app = Application.Current as App;
            feedbackController = app.feedbackController;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if ((OcenaSoftvera.Value != 0)
                ||  Raspored.Value != 0 || !Boksic.Text.Equals(""))
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            } else
            {
                NavigationService.GoBack();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Feedback feedback = new Feedback();

            Grade softGrade = Grade.veryPoor;
            int ocenaSoftvera = (int)OcenaSoftvera.Value;
            if (ocenaSoftvera == 1)
            {
                softGrade = Grade.veryPoor;
            } else if (ocenaSoftvera == 2)
            {
                softGrade = Grade.poor;
            }
            else if (ocenaSoftvera == 3)
            {
                softGrade = Grade.good;
            }
            else if (ocenaSoftvera == 4)
            {
                softGrade = Grade.good;
            }
            else if (ocenaSoftvera == 5)
            {
                softGrade = Grade.excellent;
            }

            Grade snalazenje = Grade.veryPoor;
            int ocenaSnalazenja = (int)Raspored.Value; ;
            if (ocenaSnalazenja == 1)
            {
                snalazenje = Grade.veryPoor;
            }
            else if (ocenaSnalazenja == 2)
            {
                snalazenje = Grade.poor;
            }
            else if (ocenaSnalazenja == 3)
            {
                snalazenje = Grade.good;
            }
            else if (ocenaSnalazenja == 4)
            {
                snalazenje = Grade.good;
            }
            else if (ocenaSnalazenja == 5)
            {
                snalazenje = Grade.excellent;
            }

            feedback.SoftwareGrade = softGrade;
            feedback.EverythingInGoodPlace = snalazenje;
            feedback.AdditionalNotes = Boksic.Text;
            feedback.Date = DateTime.Today;
            feedback.RegisteredUser = (RegisteredUser)MainWindow.doctor;
            feedbackController.CreateFeedback(feedback);
            forma.Visibility = Visibility.Collapsed;
            second.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
