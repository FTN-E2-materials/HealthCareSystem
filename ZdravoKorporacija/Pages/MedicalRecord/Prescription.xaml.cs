using Controller.ExaminationController;
using Controller.MedicationController;
using Model.ExaminationSurgery;
using Model.Medications;
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


namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Prescription.xaml
    /// </summary>
    /// 

    public class ShowMedications
    {
        public string name { get; set; }
        public Medication medication { get; set; }
    }
    public partial class IzdajRecept : Page
    {
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MedicationController medicationController;
        public TreatmentController treatmentController;
        public ExaminationSurgeryController examinationSurgeryController;
        public Medication medication;
        public List<Medication> medications = new List<Medication>();

        public IzdajRecept()
        {
            InitializeComponent();
            var app = Application.Current as App;

            medicationController = app.medicationController;
            treatmentController = app.treatmentController;
            examinationSurgeryController = app.examinationSurgeryController;

            List<String> usage = new List<String>();
            MyEvents.DemoEvents.PropagateCancellation.CustomEvent += cancelDemo;
            usage.Add("Svejedno");
            usage.Add("3");
            usage.Add("6");
            usage.Add("8");
            usage.Add("12");
            usage.Add("24");

            HourlyUsage.ItemsSource = usage;

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
            String text = "Popunjavanjem polja za naziv leka, pretražujete našu bazu lekova.";
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
            string lek = "Sinacilin";
            foreach(char ch in lek)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Medication.Text += ch;
                await Task.Delay(200);
            }

            text = "Kada popunite, možete odabrati vremenski period uzimanja leka.";
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
            HourlyUsage.SelectedIndex = 2;
            text = "Takođe, možete rezervisati pacijentu lek u našoj apoteci, koji će onda biti rezervisan za njega narednih 10 dana.";
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
            text = "Nakon što popunite sve informacije, pritisnućete Potvrda, ili Otkaži ili Nazad za povratak na prethodnu stranu.";
            if (token.IsCancellationRequested)
            {
                return;
            }

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

            NavigationService.Navigate(new Uri("Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            MyEvents.DemoEvents.ContinueDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ContinueDemo() { textFrom = "recept" });
        }

        private void CancelPrescription_Click(object sender, RoutedEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
            if (!Medication.Text.Equals(""))
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
            else
            {
                NavigationService.Navigate(new Uri("Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
        }

        private void AddPrescription_Click(object sender, RoutedEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
            if (Medication.Text.Equals("") && HourlyUsage.SelectedItem == null)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                TreatmentForm form = new TreatmentForm();
                form.TreatmentType = TreatmentType.prescription;
                form.AdditionalNotes = HowToUSe.Text;
                form.Medications.Add(medication);
                form.DateOfExamination = DateTime.Today;
                if (HourlyUsage.SelectedIndex != 0)
                    form.Intake = int.Parse((string)HourlyUsage.SelectedItem);
                form.Flag = (bool)Reserved.IsChecked;

                Treatment treatment = treatmentController.CreateTreatment(TreatmentFactory.CreateTreatment(form));
                ExaminationSurgery examination = examinationSurgeryController.GetCurrentExamination(MedicalRecord.Informations.currentRecord.MedicalRecord.IdRecord);
                examinationSurgeryController.UpdateTreatment(examination, treatment);

                /*
                ModelHCI.MedicationHCI medForPrescription = null;
                foreach (ModelHCI.MedicationHCI med in ModelHCI.MedicationData.meds)
                {
                    if (med.name.ToLower().Equals(Medication.Text.ToLower()))
                    {
                        medForPrescription = med;
                    }
                }
                ModelHCI.PrescriptionHCI prescription = new ModelHCI.PrescriptionHCI();
                prescription.medication = medForPrescription;

                if (HourlyUsage.SelectedIndex != 0)
                {
                    prescription.intake = int.Parse((string)HourlyUsage.SelectedItem);
                }
                prescription.reasonWhy = HowToUSe.Text;
                prescription.patient = Appointments.currentExamination.appointment.patient;
                */
               // Appointments.currentExamination.examination.treatments.Add(prescription);
               // Pages.MedicalRecord.Informations.currentRecord.Therapies.Add(new ModelHCI.TherapyHCI() { medication = prescription.medication, hourlyUsage = prescription.intake });

                NavigationService.Navigate(new Uri("Pages/MedicalRecord/Informations.xaml", UriKind.Relative));

            }
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
            if (!Medication.Text.Equals(""))
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
            else
            {
                NavigationService.Navigate(new Uri("Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
            if (Medication.Text.Equals("") && HourlyUsage.SelectedItem == null)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ModelHCI.MedicationHCI medForPrescription = null;
                foreach (ModelHCI.MedicationHCI med in ModelHCI.MedicationData.meds)
                {
                    if (med.name.ToLower().Trim().Equals(Medication.Text.Trim().ToLower()))
                    {
                        medForPrescription = med;
                    }
                }

                ModelHCI.PrescriptionHCI prescription = new ModelHCI.PrescriptionHCI();
                prescription.medication = medForPrescription;
                prescription.dateOfPrescription = DateTime.Today;
                prescription.patient = Appointments.currentExamination.appointment.patient;
                prescription.id = 15;
                if (HourlyUsage.SelectedIndex != 0)
                {
                    prescription.intake = int.Parse((string)HourlyUsage.SelectedItem);
                } else
                {
                    prescription.intake = 0;
                }
                prescription.reasonWhy = HowToUSe.Text;
                ExportPrescrption.ExportAsPdf(prescription);
            }
        }

        private void Medication_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (Medication.Text.Equals(""))
            {
                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                return;
            }


            String text = Medication.Text;
            medications = new List<Medication>();
            List<ShowMedications> medication = new List<ShowMedications>();
            autoList.ItemsSource = new List<string>();
            foreach (Medication med in medicationController.GetAllApprovedMeds())
            {
                if (med.Med.ToLower().StartsWith(text.ToLower()))
                {
                    medication.Add(new ShowMedications() { medication = med, name = med.Med});
                    medications.Add(med);
                }
            }

            if (medication.Count > 0)
            {
                autoList.Visibility = Visibility.Visible;
                boxic.Visibility = Visibility.Visible;
                autoList.ItemsSource = medication;
            }
            else
            {
                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
            }
        }

        private void HourlyUsage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
        }

        private void HowToUSe_MouseDown(object sender, MouseButtonEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
        }

        private void autoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoList.SelectedItem != null)
            {
                var med = (ShowMedications) autoList.SelectedItem;
                Medication.Text = med.medication.Med ;
                medication = med.medication;
                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                autoList.ItemsSource = new List<string>();

            }
        }

        private void Medication_LostFocus(object sender, RoutedEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;

        }
    }
}
