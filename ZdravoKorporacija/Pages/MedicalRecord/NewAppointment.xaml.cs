using Controller.ExaminationController;
using Controller.MedicalRecordController;
using Controller.ScheduleController;
using Model.ExaminationSurgery;
using Model.Schedule;
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
    /// Interaction logic for NewAppointment.xaml
    /// </summary>
    public partial class NewAppointment : Page
    {
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MedicalRecordController medicalRecordController;
        public AppointmentController appointmentController;
        public EmergencyRequestController emergencyRequestController;
        public AvailableAppointmentController availableAppointmentController;

        public NewAppointment()
        {
            var app = Application.Current as App;

            medicalRecordController = app.medicalRecordController;
            appointmentController = app.appointmentController;
            emergencyRequestController = app.emergencyRequestController;
            availableAppointmentController = app.availableAppointmentController;

            InitializeComponent();
            MyEvents.DemoEvents.PropagateCancellation.CustomEvent += cancelDemo;

            if (MainWindow.doctor.Specializations.Count == 0)
            {
                Surgery.Visibility = Visibility.Collapsed;
                Exam.Visibility = Visibility.Collapsed;
            }


            if (Menu.demoOn)
            {
                continuDemoAsync(cts.Token);
            }

        }

        private void cancelDemo(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private async Task continuDemoAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);
            String text = "Najpre, ako ste lekar specijalista, odaberite tip pregleda.";


            if (token.IsCancellationRequested)
            {
                return;
            }
            MyEvents.DemoEvents.StartDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ChangingPatients { text = text });
            await Task.Delay(2000);
            MyEvents.DemoEvents.CloseMessageEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.OpenLabResults());

            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(1000);

            text = "Posle toga, popunjavanjem datuma generisaće Vam se vremena slobodnih termina.";


            if (token.IsCancellationRequested)
            {
                return;
            }
            MyEvents.DemoEvents.StartDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ChangingPatients { text = text });
            await Task.Delay(2000);
            MyEvents.DemoEvents.CloseMessageEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.OpenLabResults());

            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);


            if (token.IsCancellationRequested)
            {
                return;
            }
            SelectedDate.SelectedDate = DateTime.Today;
            await Task.Delay(2000);

            text = "Jednostavnim otvaranjem polja vremena, odaberite slobodan termin  za Vašeg pacijenta.";

            if (token.IsCancellationRequested)
            {
                return;
            }

            MyEvents.DemoEvents.StartDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ChangingPatients { text = text });
            await Task.Delay(2000);
            MyEvents.DemoEvents.CloseMessageEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.OpenLabResults());

            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Delay(1000);
            TimeBox.SelectedIndex = 1;

            if (token.IsCancellationRequested)
            {
                return;
            }

            text = "Nakon toga, upišite dodatne napomene i hitnost pregleda i pritisnite potvrdi. Za izlazak pritisnite Otkaži ili Nazad.";

            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);
            MyEvents.DemoEvents.StartDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ChangingPatients { text = text });

            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);
            MyEvents.DemoEvents.CloseMessageEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.OpenLabResults());

            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);


            if (token.IsCancellationRequested)
            {
                return;
            }
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            MyEvents.DemoEvents.ContinueDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ContinueDemo() { textFrom = "pregled" });
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
                if (MainWindow.doctor.Specializations.Count == 0)
                {
                    if ((bool)Yes.IsChecked)
                    {
                        EmergencyRequest emergencyRequest = new EmergencyRequest();
                        emergencyRequest.MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
                        emergencyRequest.Scheduled = false;
                        emergencyRequest.SideNotes = ShortDescr.Text;
                        emergencyRequest.Specialization = null;
                        emergencyRequest.TypeOfAppointment = TypeOfAppointment.examination;
                        emergencyRequestController.CreateEmergencyRequest(emergencyRequest);

                        MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
                        NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
                    }
                    else
                    {

                        Appointment a = new Appointment();
                        a.Doctor = MainWindow.doctor;
                        a.ShortDescription = ShortDescr.Text;
                        string[] parts = ((string)TimeBox.SelectedItem).Split(":");
                        medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
                        a.TypeOfAppointment = TypeOfAppointment.examination;
                        a.Room = MainWindow.doctor.ExaminationRoom;
                        DateTime date = (DateTime)SelectedDate.SelectedDate;
                        a.StartTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(parts[0]), int.Parse(parts[1]), 0);
                        a.Urgent = false;
                        try {
                            appointmentController.AddAppointment(a, false);

                            MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
                            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
                        } catch (PatientAlreadyHasAnAppointment)
                        {
                            Error.Text = "Pacijent već ima zakazan pregled u budućnosti";
                            ErrorMessage.Visibility = Visibility.Visible;

                        } catch (AlreadyScheduled)
                        {
                            Error.Text = "U međuvremenu, termin se zakazao!";
                            ErrorMessage.Visibility = Visibility.Visible;
                        } catch (NotValidTimeForScheduling)
                        {
                            Error.Text = "Vreme je nevalidno za zakazivanje";
                            ErrorMessage.Visibility = Visibility.Visible;
                        }
                   }
                }
                else
                {
                    if ((bool)Yes.IsChecked)
                    {
                        EmergencyRequest emergencyRequest = new EmergencyRequest();
                        emergencyRequest.MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
                        emergencyRequest.Scheduled = false;
                        emergencyRequest.SideNotes = ShortDescr.Text;
                        emergencyRequest.Specialization = null;
                        emergencyRequest.TypeOfAppointment = (bool)Surgery.IsChecked ? TypeOfAppointment.surgery : TypeOfAppointment.examination;
                        emergencyRequestController.CreateEmergencyRequest(emergencyRequest);

                        MyEvents.CloseAddFormHandler.RaiseMyCustomEvent(this, new MyEvents.CloseAddForm());
                        NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
                    }
                    else
                    {
                        Appointment a = new Appointment();
                        a.Doctor = MainWindow.doctor;
                        a.ShortDescription = ShortDescr.Text;
                        string[] parts = ((string)TimeBox.SelectedItem).Split(":");
                        a.MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);

                        a.TypeOfAppointment = (bool)Surgery.IsChecked ? TypeOfAppointment.surgery : TypeOfAppointment.examination;
                        a.Room = MainWindow.doctor.ExaminationRoom;
                        DateTime date = (DateTime)SelectedDate.SelectedDate;
                        a.StartTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(parts[0]), int.Parse(parts[1]), 0);
                        a.Urgent = false;
                        try
                        {
                            appointmentController.AddAppointment(a, false);

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
        }

        private bool checkIfAnythingTyped()
        {
            if ((bool)Surgery.IsChecked || (bool)Exam.IsChecked || (SelectedDate.SelectedDate != null)
                || (TimeBox.SelectedItem != null) || !ShortDescr.Text.Equals(""))
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
            bool flag = false;
            if ((bool)Surgery.IsChecked)
            {
                flag = true;
            }
            else if ((bool)Exam.IsChecked)
            {
                flag = true;
            }
            else
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

        private void SelectedDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SelectedDate != null)
            {
                {
                    DateTime Date = (DateTime)SelectedDate.SelectedDate;

                    List<string> timeStringComboBox = new List<String>();

                    TypeOfAppointment type = (bool)Surgery.IsChecked ? TypeOfAppointment.surgery : TypeOfAppointment.examination;
                    int cnt = availableAppointmentController.GetAvailableForDayAndDoctor(Date, MainWindow.doctor, type, false).Count;
                    foreach (Appointment appointment in availableAppointmentController.GetAvailableForDayAndDoctor(Date, MainWindow.doctor, type, false).Values)
                    {
                  
                        timeStringComboBox.Add(appointment.StartTime.ToString("HH:mm"));
                    }

                        TimeBox.ItemsSource = timeStringComboBox;

                    }
                }

            }
        }
    }

