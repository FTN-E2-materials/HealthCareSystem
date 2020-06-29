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

namespace ZdravoKorporacija.Pages.Schedules
{
    /// <summary>
    /// Interaction logic for Weekly.xaml
    /// </summary>
    public partial class Weekly : UserControl
    {

        public static DateTime activeDate = DateTime.Today;
        public static DateTime monday = DateTime.Today;
 


        public class AppointmentShow
        {
            public string time { get; set; }
            public int day { get; set; }

            public string show { get; set; }
            public bool scheduled { get; set; }
            public ModelHCI.AppointmentHCI app { get; set; }

            public AppointmentShow() { }
        }
        public Weekly()
        {
            InitializeComponent();

            activeDate = DateTime.Today;

            if ((int)activeDate.DayOfWeek == 0)
            {
                monday = activeDate.AddDays(1);

            }
            else 
            {
                int addDays = (int) activeDate.DayOfWeek  - 1;
                monday = activeDate.AddDays(0 - (double)addDays);

            }


            InitializeLists();
        }

        private void InitializeLists()
        {
            AppointmentsMonday.ItemsSource = new List<AppointmentShow>();
            AppointmentsTuesday.ItemsSource = new List<AppointmentShow>();
            AppointmentsWednesday.ItemsSource = new List<AppointmentShow>();
            AppointmentsThursday.ItemsSource = new List<AppointmentShow>();
            AppointmentsFriday.ItemsSource = new List<AppointmentShow>();
            AppointmentsSaturday.ItemsSource = new List<AppointmentShow>();

            AppointmentsMonday.ItemsSource = getAppointmentsByDate(monday);
            AppointmentsTuesday.ItemsSource = getAppointmentsByDate(monday.AddDays(1));
            AppointmentsWednesday.ItemsSource = getAppointmentsByDate(monday.AddDays(2));
            AppointmentsThursday.ItemsSource = getAppointmentsByDate(monday.AddDays(3));
            AppointmentsFriday.ItemsSource = getAppointmentsByDate(monday.AddDays(4));
            AppointmentsSaturday.ItemsSource = getAppointmentsByDate(monday.AddDays(5));
            String headline = monday.ToString("dd.MM.yyyy.") + " - " + monday.AddDays(6).ToString("dd.MM.yyyy");
            Week.Text = headline;
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            monday = monday.AddDays(7);
           
            InitializeLists();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {

            monday = monday.AddDays(-7);
   
            InitializeLists();
        }

        public List<AppointmentShow> getAppointmentsByDate(DateTime date)
        {
            List<AppointmentShow> appointments = new List<AppointmentShow>();
            foreach (ModelHCI.AppointmentHCI appointment in ModelHCI.AppointmentsData.FullList)
            {
                if (DateTime.Compare(appointment.Date, date) == 0)
                {
                    appointments.Add(new AppointmentShow()
                    {
                        app = appointment,
                        day = appointment.Date.Day,
                        scheduled = true,
                        show = "Vreme: " + appointment.timeString + " \t" + appointment.patient.FullName,
                        time = appointment.timeString
                    });

                }
            }

            return appointments;

        }
    }
}
