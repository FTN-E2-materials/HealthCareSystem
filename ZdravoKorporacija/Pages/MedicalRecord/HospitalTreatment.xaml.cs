using Controller.ExaminationController;
using Controller.RoomController;
using Model.ExaminationSurgery;
using Model.Rooms;
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
    /// Interaction logic for HospitalTreatment.xaml
    /// </summary>
    public partial class HospitalTreatment : Page
    {
        public static DateTime startDate = DateTime.Today.AddDays(-1);

        public string ERROR_DATE_MESSAGE_BEFORE_TODAY = "Datum otvaranja mora biti posle današnjeg datuma!";
        public string ALL_INPUTS_ERROR = "Morate popuniti sva polja označena zvezdicom!";

        public string ERROR_END_DATE_MESSAGE = "Datum zatvaranja mora biti pre datuma otvaranja!";
        public CancellationTokenSource cts = new CancellationTokenSource();
        public DepartmentController departmentController;
        public ExaminationSurgeryController examinationSurgeryController;
        public TreatmentController treatmentController;

        public HospitalTreatment()
        {
            MyEvents.DemoEvents.PropagateCancellation.CustomEvent += cancelDemo;
            InitializeComponent();

            var app = Application.Current as App;
            departmentController = app.departmentController;
            examinationSurgeryController = app.examinationSurgeryController;
            treatmentController = app.treatmentController;

            foreach (Department deparment in departmentController.GetAllDepartments())
            {
                Department.Items.Add(deparment.Name);
            }

            startDate = DateTime.Today.AddDays(-1);

            if (Menu.demoOn)
            {
                continueDemoAsync(cts.Token);
            }
        }

        private void cancelDemo(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private async Task continueDemoAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);

            String text = "Najpre, popunite datume početka i kraja lečenja.";

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

            StartDate.SelectedDate = DateTime.Today;

            if (token.IsCancellationRequested)
            {
                return;
            }

            await Task.Delay(2000);
            EndDate.SelectedDate = DateTime.Today.AddDays(2);

            text = "Onda, odaberite odeljenje na kom pacijent treba da leži.";

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
            Department.SelectedIndex = 1;
            text = "Nakon toga, prikazaće Vam se sobe koje su tada slobodne.";

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

            await Task.Delay(1000);

            text = "Nakon toga, upišite dodatne napomene i pritisnite potvrdi za slanje sekretaru. Za izlazak pritisnite Otkaži ili Nazad.";

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

            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));

            if (token.IsCancellationRequested)
            {
                return;
            }
            MyEvents.DemoEvents.ContinueDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ContinueDemo() { textFrom = "lecenje" });
        }

        private void Add_Form_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.SelectedDate == null || EndDate.SelectedDate == null || Department.SelectedItem == null)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ALL_INPUTS_ERROR;
            } else
            {
                TreatmentForm form = new TreatmentForm();
                form.TreatmentType = TreatmentType.hospitalTreatment;
                form.Department = departmentController.GetDepartmentByName((string)Department.SelectedItem);
                form.StartDate =(DateTime) StartDate.SelectedDate;
                form.EndDate = (DateTime)EndDate.SelectedDate;


                Treatment treatment = treatmentController.CreateTreatment(TreatmentFactory.CreateTreatment(form));
                ExaminationSurgery examination = examinationSurgeryController.GetCurrentExamination(MedicalRecord.Informations.currentRecord.MedicalRecord.IdRecord);
                examinationSurgeryController.UpdateTreatment(examination, treatment);

                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
        }

        private void Cancel_Form_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.SelectedDate != null || EndDate.SelectedDate != null || Department.SelectedItem != null)
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            } else
            {
                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.SelectedDate != null || EndDate.SelectedDate != null || Department.SelectedItem != null)
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }

        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate != null)
            {

                DateTime Date = (DateTime)EndDate.SelectedDate;

                if (Date.CompareTo(DateTime.Today) < 0)
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                    Error.Text = ERROR_DATE_MESSAGE_BEFORE_TODAY;
                    StartDate.SelectedDate = null;
                } 
                else
                {
                    if (StartDate.SelectedDate != null)
                    {
                        if (Date.CompareTo(startDate) < 0)
                        {
                            ErrorMessage.Visibility = Visibility.Visible;
                            Error.Text = ERROR_END_DATE_MESSAGE;
                            EndDate.SelectedDate = null;
                        } else
                        {

                        }
                    }
                }
            }
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDate.SelectedDate != null)
            {
                DateTime Date = (DateTime)StartDate.SelectedDate;

                if (Date.CompareTo(DateTime.Today) < 0)
                {
                    ErrorMessage.Visibility = Visibility.Visible;
                    Error.Text = ERROR_DATE_MESSAGE_BEFORE_TODAY;
                    StartDate.SelectedDate = null;
                } else
                {
                    startDate = Date;
                }
            }
        }
    }
}
