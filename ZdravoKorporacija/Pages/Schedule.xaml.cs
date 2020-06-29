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
    /// Interaction logic for Schedule.xaml
    /// </summary>
    /// 
    public partial class Schedule : Page
    {
        public static bool flag = false;
        public static DateTime date = DateTime.Today;

        public Schedule()
        {
            

            MyEvents.AddAppointmentEventHandler.CustomEvent += AddAppointment;
            MyEvents.CloseAddFormHandler.CustomEvent += CloseAddForm;
            MyEvents.ShowMessageBoxEventHandler.CustomEvent += ShowMessageBox;
            MyEvents.CloseMessageBoxEventHandler.CustomEvent += CloseMessageBox;

            InitializeComponent();
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Schedules.DailySchedule());
        }

        private void CloseMessageBox(object sender, EventArgs e)
        {
            if (Pages.MessageBox.isOkay)
            {
                MessageBox.Children.Clear();
                MainPanel.Children.Clear();
                MainPanel.Children.Add(new Schedules.DailySchedule());
            }
            else
            {
                MessageBox.Children.Clear();

            }
        }

        private void ShowMessageBox(object sender, EventArgs e)
        {
            MessageBox.Children.Clear();
            MessageBox.Children.Add(new MessageBox());
        }

        private void CloseAddForm(object sender, EventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Schedules.DailySchedule());
        }

        private void AddAppointment(object sender, EventArgs e)
        {
                if (e is MyEvents.SetDateEventHandler)
                {
                var datum = (MyEvents.SetDateEventHandler)e;
                date = datum.date;
                flag = true;
                MainPanel.Children.Clear();
                MainPanel.Children.Add(new Schedules.AddAppointment());
            } else
            {
                flag = false;
                MainPanel.Children.Clear();
                MainPanel.Children.Add(new Schedules.AddAppointment());
            }

            
        }


        private void Daily_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Schedules.DailySchedule());
        }

        private void Monthly_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Schedules.Monthly());

        }

        private void Weekly_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new Schedules.Weekly());

        }
    }
}

