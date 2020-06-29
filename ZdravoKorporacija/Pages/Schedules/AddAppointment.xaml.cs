using Controller.MedicalRecordController;
using Controller.ScheduleController;
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
using Model.Users;
using Model.Schedule;
using SimsProjekat.Controller.ScheduleController;

namespace ZdravoKorporacija.Pages.Schedules
{
    /// <summary>
    /// Interaction logic for AddAppointment.xaml
    /// </summary>
    /// 


    public class PatientShow
    {
        public Patient patient { get; set; }
        public string FullNameChartNumber { get; set; }

        public PatientShow(string FullNameChart, Patient patient)
        {
            this.patient = patient;
            this.FullNameChartNumber = FullNameChart;
        }
    }
    public partial class AddAppointment : UserControl
    {
        public static List<PatientShow> listChanged = new List<PatientShow>();
        public UserController userController;
        public MedicalRecordController medicalRecordController;
        public AppointmentController appointmentController;
        public AvailableAppointmentController availableAppointemntController;
        public Patient selectedPatient = null;
        public AddAppointment()
        {
            var app = Application.Current as App;
            userController = app.userController;
            medicalRecordController = app.medicalRecordController;
            appointmentController = app.appointmentController;
            availableAppointemntController = app.availableAppointmentController;

            InitializeComponent();

            SelectedDate.SelectedDate = null;
            if (Schedule.flag)
            {

                SelectedDate.SelectedDate = Schedule.date;
            }
            TimeBox.ItemsSource = new List<String>();

        }

        private void PatientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (PatientList.SelectedItem != null)
                {
                    if (PatientList.SelectedItem is PatientShow)
                    {
                        var row = (PatientShow)PatientList.SelectedItem;

                        if (row != null)
                        {
                            selectedPatient = row.patient;
                            ChoosePatient.Visibility = Visibility.Collapsed;
                            Form_Add.Visibility = Visibility.Visible;
                            PatientChoosen.Text = selectedPatient.Name + " " + selectedPatient.Surname;
                            Rooms.Items.Add(MainWindow.doctor.ExaminationRoom.RoomNumber);

                            if (DateTime.Today.CompareTo(Monthly.dayForAdd) > 0)
                            {
                                SelectedDate.SelectedDate = Monthly.dayForAdd;
                            } 
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            if (checkIfAnythingTyped())
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
            else
            {
                MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
            }
        }

        private void Cancel_Form_Click(object sender, RoutedEventArgs e)
        {
            if (checkIfAnythingTyped())
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
            else
            {
                MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
            }
        }

        private void Add_Form_Click(object sender, RoutedEventArgs e)
        {
            if (!checkIfSomethingLeft())
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                Appointment appointment = new Appointment();
                if ((bool)Surgery.IsChecked)
                {
                    appointment.TypeOfAppointment = TypeOfAppointment.examination;
                }
                else if ((bool)Exam.IsChecked)
                {
                    appointment.TypeOfAppointment = TypeOfAppointment.surgery;
                }

                appointment.MedicalRecord = medicalRecordController.GetRecordByPatient(selectedPatient);
                string[] timeparts = ((string)TimeBox.SelectedItem).Split(":");
                appointment.StartTime = new DateTime(((DateTime)SelectedDate.SelectedDate).Year, ((DateTime)SelectedDate.SelectedDate).Month, ((DateTime)SelectedDate.SelectedDate).Day, int.Parse(timeparts[0]), int.Parse(timeparts[1]), 0);
                appointment.Room = MainWindow.doctor.ExaminationRoom;
                appointment.ShortDescription = ShortDescr.Text;

                if ((bool)Urgent.IsChecked)
                {
                    appointment.Urgent = true;
                }
                else if ((bool)NotUrgent.IsChecked)
                {
                    appointment.Urgent = false;
                } else
                {
                    appointment.Urgent = true;
                }

                appointment.Doctor = MainWindow.doctor;
                appointment.Deleted = false;

                try
                {
                    appointmentController.AddAppointment(appointment, false);
                    MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
                } catch (PatientAlreadyHasAnAppointment)
                {
                    Error.Text = "Pacijent već ima zakazan pregled u budućnosti!";
                    ErrorMessage.Visibility = Visibility.Visible;
                } catch (SimsProjekat.Exceptions.NotValidTimeForScheduling)
                {
                    Error.Text = "Vreme termina nije validno!";
                    ErrorMessage.Visibility = Visibility.Visible;
                } catch (SimsProjekat.Exceptions.AlreadyScheduled)
                {
                    Error.Text = "Termin se već zakazao!";
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox != null && PatientList != null)
            {
                String text = SearchBox.Text;
                listChanged = new List<PatientShow>();

                foreach (Patient patient in userController.GetAllPatients())
                {
                    if (patient.Name.ToLower().Contains(text.ToLower()) ||
                        patient.Surname.ToLower().Contains(text.ToLower()))
                       
                    {
                        listChanged.Add(new PatientShow(medicalRecordController.GetRecordByPatient(patient).IdRecord.ToString() + " " + patient.Name + " " + patient.Surname, patient));

                    }
                }
                PatientList.ItemsSource = new List<ModelHCI.Patient>();
                PatientList.ItemsSource = listChanged;
            }
        }

        private void SelectedDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedDate != null)
            {
                {
                    DateTime Date = (DateTime)SelectedDate.SelectedDate;

                    List<string> timeStringComboBox = new List<String>();

                    if (Date.CompareTo(DateTime.Today) < 0)
                    {

                        TimeBox.ItemsSource = new List<String>();
                        timeStringComboBox.Add("Odabrali ste datum pre današnjeg!");
                    }
                    else
                    {


                        TimeBox.ItemsSource = new List<String>();
                        TypeOfAppointment typeOfAppointment = (bool)Surgery.IsChecked ? TypeOfAppointment.surgery : TypeOfAppointment.examination;

                        foreach (Appointment appointment in availableAppointemntController.GetAvailableForDayAndDoctor(Date, MainWindow.doctor, typeOfAppointment, false).Values)
                        {
                            timeStringComboBox.Add(appointment.StartTime.ToString("HH:mm"));
                        }
   
                        TimeBox.ItemsSource = timeStringComboBox;

                    }
                }

            }
        }

        private void CloseChoose_Click(object sender, RoutedEventArgs e)
        {
            if (checkIfAnythingTyped())
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
            else
            {
                MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
            }
        }
        private bool checkIfAnythingTyped()
        {
            if ((bool) Surgery.IsChecked || (bool) Exam.IsChecked || (SelectedDate.SelectedDate != null)
                || (TimeBox.SelectedItem != null) || !ShortDescr.Text.Equals(""))
            {
                return true;
            } else
            {
                return false;
            }
        }


        private bool checkIfSomethingLeft()
        { 
            bool flag = false;
            if ((bool)Surgery.IsChecked)
            {
                flag = true;
            } else if ((bool)Exam.IsChecked)
            {
                flag = true;
            } else
            {
                flag = false;
            }
            if (flag && ((DateTime)SelectedDate.SelectedDate != null)
                && !((string)TimeBox.SelectedItem).Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
