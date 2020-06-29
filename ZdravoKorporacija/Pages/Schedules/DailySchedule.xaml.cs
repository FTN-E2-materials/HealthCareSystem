using Controller.ScheduleController;
using Model.Schedule;
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
    /// Interaction logic for DailySchedule.xaml
    /// </summary>
    /// 


    public partial class DailySchedule : UserControl
    {
        public ModelHCI.AppointmentHCI SelectedRow = null;
        public AppointmentController appointmentController;
        public DailySchedule()
        {
            var app = Application.Current as App;
            appointmentController = app.appointmentController;

            InitializeComponent();
            DatePicker_Schedule.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
           
            SetDataGrid();
        }

        private void SetDataGrid()
        {
            DateTime Date = (DateTime)DatePicker_Schedule.SelectedDate;
            List<ModelHCI.AppointmentHCI> ListOfAppointments = new List<ModelHCI.AppointmentHCI>();

            foreach (Appointment appointment in appointmentController.GetScheduledByDoctorForOneDay(MainWindow.doctor, Date))
            {
                    ListOfAppointments.Add(new ModelHCI.AppointmentHCI() { type = appointment.TypeOfAppointment == TypeOfAppointment.examination ? ModelHCI.Type.Operacija : ModelHCI.Type.Pregled, Date = Date, appointment = appointment, Age = (DateTime.Today.Year - appointment.MedicalRecord.Patient.DateOfBirth.Year).ToString(),
                         FullName = appointment.MedicalRecord.Patient.Name + " " + appointment.MedicalRecord.Patient.Surname, Gender = "Muški", HealthProblem = appointment.ShortDescription,
                         ID = appointment.IdAppointment, RoomNumber = appointment.Room.RoomNumber, timeString = appointment.StartTime.ToString("HH:mm"), doctor = null, Urgent = appointment.Urgent ? "DA" : "NE" });
                    Console.WriteLine("asfasd");
                
            }

            if (Appointments_by_day != null)
            {
                Appointments_by_day.ItemsSource = new List<ModelHCI.AppointmentHCI>();
                Appointments_by_day.ItemsSource = ListOfAppointments;
            }
        }
        private void DatePicker_Schedule_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (GroupBox_Condition != null)
            {
                GroupBox_Condition.Visibility = Visibility.Collapsed;
                Close_Information.Visibility = Visibility.Collapsed;
            }
            SetDataGrid();


        }

        private void Some_information_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null)
            {
                Main_content.Margin = new Thickness(0, 60, 0, 0);
                Close_Information.Visibility = Visibility.Visible;
                GroupBox_Condition.Visibility = Visibility.Visible;
                Condition.Text = SelectedRow.HealthProblem;


            }
        }


        private void Add_appointment_Click(object sender, RoutedEventArgs e)
        {

            MyEvents.AddAppointmentEventHandler.RaiseMyCustomEvent(this, new MyEvents.AddAppointmentEvent());
        }


        private void Appointments_by_day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                try
                {
                    if (Appointments_by_day.SelectedItem != null)
                    {
                        if (Appointments_by_day.SelectedItem is ModelHCI.AppointmentHCI)
                        {
                            var row = (ModelHCI.AppointmentHCI)Appointments_by_day.SelectedItem;


                            if (row != null)
                            {
                                SelectedRow = row;
                                Some_information.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

            }
        }

        private void Close_Information_Click(object sender, RoutedEventArgs e)
        {
            Close_Information.Visibility = Visibility.Collapsed;
            GroupBox_Condition.Visibility = Visibility.Collapsed;

            Main_content.Margin = new Thickness(100, 60, 0, 0);
        }
    }
}
