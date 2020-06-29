using Controller.ExaminationController;
using Controller.MedicalRecordController;
using Controller.RoomController;
using Controller.ScheduleController;
using Controller.UserController;
using Model.ExaminationSurgery;
using Model.Rooms;
using Model.Schedule;
using Model.Users;
using SimsProjekat.Controller.ScheduleController;
using SimsProjekat.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoKorporacija.Pages.MedicalRecord
{
    /// <summary>
    /// Interaction logic for SpecialistsExam.xaml
    /// </summary>
    public partial class SpecialistsExam : Page
    {
        public static ModelHCI.Doctor selectedDoctor = null;
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MedicalRecordController medicalRecordController;
        public AppointmentController appointemntController;
        public DepartmentController departmentController;
        public UserController userController;
        public EmergencyRequestController emergencyRequestController;
        public AvailableAppointmentController availableAppointmentController;
        public static Doctor doctor = null;
        public List<Doctor> doctors = new List<Doctor>();
        public SpecialistsExam()
        {
            var app = Application.Current as App;
            medicalRecordController = app.medicalRecordController;
            appointemntController = app.appointmentController;
            departmentController = app.departmentController;
            userController = app.userController;
            emergencyRequestController = app.emergencyRequestController;
            availableAppointmentController = app.availableAppointmentController;


            InitializeComponent();

            foreach (Department d in departmentController.GetAllDepartments())
            {
                Department.Items.Add(d.Name);
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

                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
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
                if ((bool)Yes.IsChecked)
                {
                    EmergencyRequest emergencyRequest = new EmergencyRequest();
                    emergencyRequest.MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
                    emergencyRequest.Scheduled = false;
                    emergencyRequest.SideNotes = ShortDescr.Text;
                    emergencyRequest.Specialization = doctor.Specializations[0];
                    emergencyRequest.TypeOfAppointment = TypeOfAppointment.examination;
                    emergencyRequestController.CreateEmergencyRequest(emergencyRequest);

                    MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
                    NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
                }
                else
                {
                    Appointment a = new Appointment();
                    a.Doctor = doctor;
                    a.ShortDescription = ShortDescr.Text;
                    string[] parts = ((string)TimeBox.SelectedItem).Split(":");
                    a.MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);

                    a.TypeOfAppointment = TypeOfAppointment.examination;
                    a.Room = doctor.ExaminationRoom;
                    DateTime date = (DateTime)DateSelected.SelectedDate;
                    a.StartTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(parts[0]), int.Parse(parts[1]), 0);
                    a.Urgent = false;
                    try
                    {
                        appointemntController.AddAppointment(a, false);

                        MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
                        NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
                    }
                    catch (PatientAlreadyHasAnAppointment)
                    {
                        Error.Text = "Pacijent već ima zakazan pregled u budućnosti";
                        ErrorMessage.Visibility = Visibility.Visible;

                    }
                    catch (AlreadyScheduled)
                    {
                        Error.Text = "U međuvremenu, termin se zakazao!";
                        ErrorMessage.Visibility = Visibility.Visible;
                    }
                    catch (NotValidTimeForScheduling)
                    {
                        Error.Text = "Vreme je nevalidno za zakazivanje";
                        ErrorMessage.Visibility = Visibility.Visible;
                    }
                }
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

                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
        }

        private void DateSelected_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DateSelected != null)
            {
                {
                    DateTime Date = (DateTime)DateSelected.SelectedDate;

                   List<string> timeStringComboBox = new List<String>();
                    if (doctor != null)
                    {
                        foreach (Appointment a in availableAppointmentController.GetAvailableForDayAndDoctor(Date, doctor, TypeOfAppointment.examination, false).Values)
                        {
                            timeStringComboBox.Add(a.StartTime.ToString("HH:mm"));
                        }

                        TimeBox.ItemsSource = timeStringComboBox;

                        if (timeStringComboBox.Count == 0)
                        {
                            Error.Text = "Lekar za odabrani datum nema radno vreme!";
                            ErrorMessage.Visibility = Visibility.Visible;
                        }
                    } else
                    {
                        Error.Text = "Molim Vas, odaberite lekara!";
                        ErrorMessage.Visibility = Visibility.Visible;
                    }
                    }
                }
            }
        

        private void Department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctors.ItemsSource = new List<String>();
            List<String> doctorsFullNames = new List<String>();
            String specialization = (string)Department.SelectedItem;
            doctors = new List<Doctor>();
            Doctors.SelectedItem = null;

            if (specialization != null) {
                Department department = departmentController.GetDepartmentByName(specialization);
                foreach (Doctor dr in userController.GetDoctorsFromDepartment(department))
                {
                    doctorsFullNames.Add(dr.Name + " " + dr.Surname);
                    doctors.Add(dr);   
                }
            }
            Doctors.ItemsSource = doctorsFullNames;
        }

            private bool checkIfAnythingTyped()
            {
                if ((Department.SelectedItem != null || Doctors.SelectedItem != null || (DateSelected.SelectedDate != null)
                    || (TimeBox.SelectedItem != null) || !ShortDescr.Text.Equals("")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            private bool checkIfSomethingLeft()
            {
            bool flag = true;
            if (TimeBox.SelectedItem != null)
            {
                if (flag && ((DateTime)DateSelected.SelectedDate != null)
                    && !((string)TimeBox.SelectedItem).Equals(""))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return false;
            }
            }

        private void Doctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String doctorSelected = (string)Department.SelectedItem;
            if (doctorSelected != null)
            {
                if (doctors.Count > 0  && Doctors.SelectedIndex != -1)
                    doctor = doctors[Doctors.SelectedIndex];
            }
        }
    }

}
