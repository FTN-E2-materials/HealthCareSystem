using Controller.ExaminationController;
using Controller.MedicalRecordController;
using Controller.ReportController;
using Controller.ScheduleController;
using Model.ExaminationSurgery;
using Model.Schedule;
using Model.Users;
using Service.ReportService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using ZdravoKorporacija.MyEvents.DemoEvents;

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Appointments.xaml
    /// </summary>



    /// 


    public class ButtonAppointment
    {
        public Visibility visible {get; set;}
        public ModelHCI.AppointmentHCI appointment { get; set; }
        public ModelHCI.MedicalRecordHCI record { get; set; }
        public ModelHCI.ExaminationSurgeryHCI examination { get; set; }
        public string name { get; set; }
        
    }
    public partial class Appointments : Page
    {
        public AppointmentController appointemntController;
        public MedicalRecordController medicalRecordController;
        public ExaminationSurgeryController examinationSurgeryController;
        public ReportController reportController;


        public List<ModelHCI.AppointmentHCI> todayAppointments = new List<ModelHCI.AppointmentHCI>();
        public List<ModelHCI.AppointmentHCI> forShowing = new List<ModelHCI.AppointmentHCI>();
        public static ButtonAppointment currentExamination = null;
        public ModelHCI.AppointmentHCI forShow = null; 
        public List<ButtonAppointment> buttoni;
        public int lastIndex = 4;
        public int firstIndex = 0;
        public bool demoOn = false;
        public static ModelHCI.MedicalRecordHCI recordDemo = null;
        public static bool last = false;
        public CancellationTokenSource cts = new CancellationTokenSource();



        public Appointments()
        {
            currentExamination = null;
            MyEvents.ShowMessageBoxEventHandler.CustomEvent += ShowMessageBox;
            MyEvents.CloseMessageBoxEventHandler.CustomEvent += CloseMessageBox;
            MyEvents.ShowLanguage.CustomEvent += showLanguage;
            MyEvents.CloseLanguage.CustomEvent += closeLanguage;

            var app = Application.Current as App;
            appointemntController = app.appointmentController;
            medicalRecordController = app.medicalRecordController;
            examinationSurgeryController = app.examinationSurgeryController;
            reportController = app.reportController;

            DateTime todaysDate = DateTime.ParseExact("01.05.2020.", "dd.MM.yyyy.", null);



            foreach (Appointment appointment in appointemntController.NotFinishedByDoctorAndDay(MainWindow.doctor, DateTime.Today))
            {
                Patient patient = medicalRecordController.GetMedicalRecord(appointment.MedicalRecord.IdRecord).Patient;
                todayAppointments.Add(new ModelHCI.AppointmentHCI() { Gender = patient.Gender == Model.Users.Gender.FEMALE ? "ŽENSKI" : "MUŠKI",
                     Age = ((DateTime.Today.Year - (medicalRecordController.GetMedicalRecord(appointment.MedicalRecord.IdRecord)).Patient.DateOfBirth.Year).ToString()), Date = appointment.StartTime.Date,
                      FullName = patient.Name + " " + patient.Surname, HealthProblem = appointment.ShortDescription,
                      appointment = appointment,  RoomNumber = appointment.Room.RoomNumber, timeString = appointment.StartTime.ToString("HH:mm"), 
                      patient = new ModelHCI.Patient() { record = new ModelHCI.MedicalRecordHCI() { MedicalRecord = appointment.MedicalRecord} }
                      
                    });
            }


            InitializeComponent();

            SetPatients();

        }

        private void SetPatients()

        {

            int max = todayAppointments.Count == 4 ? 4 : todayAppointments.Count;
            lastIndex = todayAppointments.Count == 4 ? 4 : todayAppointments.Count;
            buttoni = new List<ButtonAppointment>();
                for (int i = 0; i < max; i++)
                {

                ExaminationSurgery examinationSurgery = new ExaminationSurgery();
                examinationSurgery.MedicalRecord = todayAppointments[i].patient.record.MedicalRecord;
                examinationSurgery.StartTime = todayAppointments[i].appointment.StartTime;
                examinationSurgery.Doctor = MainWindow.doctor;
                examinationSurgery = examinationSurgeryController.CreateExaminationSurgery(examinationSurgery);
                buttoni.Add(new ButtonAppointment()
                {
                    appointment = todayAppointments[i],
                    name = todayAppointments[i].FullName,
                    visible = Visibility.Visible,
                    record = todayAppointments[i].patient.record,
                    examination = new ModelHCI.ExaminationSurgeryHCI()
                    {
                        examinationSurgery = examinationSurgery
                    }
                });
                }


            if (buttoni[0].appointment.patient.record.MedicalRecord.Patient.ProfileImage != null)
                Profile.Source = new BitmapImage(new Uri(buttoni[0].appointment.patient.record.MedicalRecord.Patient.ProfileImage, UriKind.Relative));

                ButtonList.ItemsSource = buttoni;
                FullName.Text = buttoni[0].appointment.FullName;
                DateAppointment.Text = buttoni[0].appointment.Date.Date.ToString("dd.MM.yyyy.");
                StringTime.Text = buttoni[0].appointment.timeString;
                Record_Number.Text = buttoni[0].appointment.patient.record.MedicalRecord.IdRecord.ToString();
                Gender.Text = buttoni[0].appointment.Gender;
                ID_Number.Text = buttoni[0].appointment.patient.record.MedicalRecord.Patient.IdentificationNumber;
                DateOfBirth.Text = buttoni[0].appointment.patient.dateOfBirth.ToString("dd.MM.yyyy.");
                Curr_Residence.Text = buttoni[0].appointment.patient.record.MedicalRecord.Patient.CurrResidence.City.Name;
                State.Text = buttoni[0].appointment.patient.record.MedicalRecord.Patient.CurrResidence.City.State.Name;


                MedicalRecord.Informations.currentRecord = buttoni[0].appointment.patient.record;



                Circle1.Fill = Brushes.LightGray;
            
        }

        private void closeLanguage(object sender, EventArgs e)
        {
            Language.Children.Clear();
        }

        private void showLanguage(object sender, EventArgs e)
        {
            Language.Children.Clear();
            Language.Children.Add(new Language());
        }
        private void CloseMessageBox(object sender, EventArgs e)
        {
            if(Pages.MessageBox.isOkay)
            {
                MessageBox.Children.Clear();
                Frejm.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            } else
            {
                MessageBox.Children.Clear();
            }
        }

        private void ShowMessageBox(object sender, EventArgs e)
        {
            MessageBox.Children.Clear();
            MessageBox.Children.Add(new MessageBox());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/Prescription.xaml", UriKind.Relative));
        }

        private void SpecialistsExam_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/SpecialistsExam.xaml", UriKind.Relative));
        }

        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {

            Frejm.Navigate(new Uri("/Pages/MedicalRecord/NewAppointment.xaml", UriKind.Relative));
        }

        private void HospitalTreatment_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/HospitalTreatment.xaml", UriKind.Relative));
        }

        private void Vaccines_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/Vaccines.xaml", UriKind.Relative));
        }

        private void ButtonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index;
            if (demoOn)
            {
                index = 1;
  
            } else
            {
                index = ButtonList.SelectedIndex;
            }

            if (index != -1)
            {

                if (buttoni[index].appointment.patient.record.MedicalRecord.Patient.ProfileImage != null)
                    Profile.Source = new BitmapImage(new Uri(buttoni[0].appointment.patient.record.MedicalRecord.Patient.ProfileImage, UriKind.Relative));
                FullName.Text = buttoni[index].appointment.FullName;
                DateAppointment.Text = buttoni[index].appointment.Date.ToString("dd.MM.yyyy.");
                StringTime.Text = buttoni[index].appointment.timeString;
                Record_Number.Text = buttoni[index].appointment.patient.record.MedicalRecord.IdRecord.ToString();
                Gender.Text = buttoni[index].appointment.Gender;
                ID_Number.Text = buttoni[index].appointment.patient.record.MedicalRecord.Patient.IdentificationNumber;
                DateOfBirth.Text = buttoni[index].appointment.patient.record.MedicalRecord.Patient.DateOfBirth.ToString("dd.MM.yyyy.");
                Curr_Residence.Text = buttoni[index].appointment.patient.record.MedicalRecord.Patient.CurrResidence.City.Name;
                State.Text = buttoni[index].appointment.patient.record.MedicalRecord.Patient.CurrResidence.City.State.Name;

                MedicalRecord.Informations.currentRecord = buttoni[index].appointment.patient.record;
                MyEvents.ChangeInformation.RaiseMyCustomEvent(this, new MyEvents.PatientSelectionChanged());
                currentExamination = buttoni[index];
                currentExamination.record = buttoni[index].appointment.patient.record;
                recordDemo = buttoni[index].appointment.patient.record;
                Frejm.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
        }

        private void ChangePageLeft_Click(object sender, RoutedEventArgs e)
        {
            if (firstIndex > 0)
            {
                firstIndex--;
                lastIndex--;
                List<ButtonAppointment> newButtoni = new List<ButtonAppointment>();

                if (lastIndex == todayAppointments.Count)
                {
                    ChangePageRight.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ChangePageRight.Visibility = Visibility.Visible;
                }

                if (lastIndex > 0)
                {
                    ChangePageLeft.Visibility = Visibility.Visible;
                }
                else
                {
                    ChangePageLeft.Visibility = Visibility.Collapsed;
                }

                for (int i = firstIndex; i < lastIndex; i++)
                {
                    newButtoni.Add(new ButtonAppointment { appointment = todayAppointments[i], name = todayAppointments[i].FullName });
                }

                ButtonList.ItemsSource = new List<ButtonAppointment>();
                buttoni.Clear();
                foreach (ButtonAppointment app in newButtoni)
                {
                    buttoni.Add(app);
                }
                ButtonList.ItemsSource = newButtoni;

                if (firstIndex == 0)
                {
                    ChangePageLeft.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ChangePageLeft.Visibility = Visibility.Collapsed;
            }

        }

        private void ChangePageRight_Click(object sender, RoutedEventArgs e)
        {
            if (lastIndex < todayAppointments.Count)
            {
                firstIndex++;
                List<ButtonAppointment> newButtoni = new List<ButtonAppointment>();

                if (lastIndex == todayAppointments.Count)
                {
                    ChangePageRight.Visibility = Visibility.Collapsed;
                } else
                {
                    ChangePageRight.Visibility = Visibility.Visible;
                }

                if (lastIndex > 0)
                {
                    ChangePageLeft.Visibility = Visibility.Visible;
                } else
                {
                    ChangePageLeft.Visibility = Visibility.Collapsed;
                }

                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    newButtoni.Add(new ButtonAppointment { appointment = todayAppointments[i], name = todayAppointments[i].FullName });
                }
                lastIndex++;
                ButtonList.ItemsSource = new List<ButtonAppointment>();
                buttoni.Clear();
                foreach(ButtonAppointment app in newButtoni)
                {
                    buttoni.Add(app);
                }
                ButtonList.ItemsSource = newButtoni;

                if (lastIndex == todayAppointments.Count)
                {
                    ChangePageRight.Visibility = Visibility.Collapsed;
                }
            } else
            {
                ChangePageRight.Visibility = Visibility.Collapsed;
            }

        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = appointemntController.GetCurrentAppointment(MainWindow.doctor, MedicalRecord.Informations.currentRecord.MedicalRecord);
            appointemntController.FinishAppointment(appointment);
            var report = reportController.GenerateExaminationReport(examinationSurgeryController.GetCurrentExamination(MedicalRecord.Informations.currentRecord.MedicalRecord.IdRecord));
            ExportToPDFExamination.ExportAsPDF(report);
        }
    }
    
}
