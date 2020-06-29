using Controller.MedicalRecordController;
using Controller.MedicationController;
using Model.MedicalRecord;
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
    /// Interaction logic for EditIllnessHistory.xaml
    /// </summary>
    public partial class EditIllnessHistory : Page
    {
        public static ModelHCI.MedicalRecordHCI record;
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MedicalRecordController medicalRecordController;
        public DiagnosisController diagnosisController;
        public SymptomsController symptomsController;
        List<Symptoms> simptomi = new List<Symptoms>();
        public EditIllnessHistory()
        {
            MyEvents.DemoEvents.PropagateCancellation.CustomEvent += cancelDemo;

            var app = Application.Current as App;
            symptomsController = app.symptomsController;
            diagnosisController = app.diagnosisController;
            medicalRecordController = app.medicalRecordController;

            InitializeComponent();
            /*
            if (Informations.demoOn)
            {
                continueDemoAsync(cts.Token);
            }*/
            SetListBoxes();

        }

        private void cancelDemo(object sender, EventArgs e)
        {
            cts.Cancel();
        }
        /*
        private async Task continueDemoAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);
            String text = "Popunjavanjem polja za simptome i pritiskom na plus, dodaćete novi simptom.";
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

            string lek = "kašalj";
            foreach (char ch in lek)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Simptom.Text += ch;
                await Task.Delay(200);
            }
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(1000);
            Problems.Items.Add(lek);
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(1000);
            text = "Nakon dodavanja, pojaviće Vam se lista mogućih dijagnoza.";
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

            List<String> potencijalne = new List<String>();
            foreach (ModelHCI.DiagnosisHCI diagnosis in ModelHCI.PatientsData.allDiagnosis)
            {
                foreach (ModelHCI.SymptomsHCI symptom in diagnosis.symptoms)
                {
                    if (symptom.symptom.ToLower().Equals(lek))
                    {
                        potencijalne.Add(diagnosis.name);
                    }
                }

            }
            Dijagnoze.ItemsSource = new List<string>();
            Dijagnoze.ItemsSource = potencijalne;

            text = "Nakon toga, pritisnite Potvrda za dodavanje.";
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
            MyEvents.DemoEvents.ContinueDemoInformations.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ContinueInfoArgs() { text = "kraj" });
            if (token.IsCancellationRequested)
            {
                return;
            }
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
        }
        */
        private void IllnessHistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Problems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Simptom.Text.Equals(""))
            {
                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            } else
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Diagnosis diagnosis = null;
            diagnosis = diagnosisController.FindByName((string)Dijagnoze.SelectedItem);
            medicalRecordController.UpdateIllnessHistory(diagnosis, medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord));
            Informations.currentRecord.MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
        }

        private void AddSymptom_Click(object sender, RoutedEventArgs e)
        {
            if (Simptom.Text.Equals(""))
            {

            } else
            {
                Symptoms simptom = new Symptoms(Simptom.Text);

                Problems.ItemsSource = new List<ModelHCI.SymptomsHCI>();
                List<String> potencijalne = new List<String>();

                var record = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);

                foreach (Diagnosis diagnosis in record.IllnessHistory)
                {
                    foreach (Symptoms symptom in diagnosis.Symptoms)
                    {
                        simptomi.Add(symptom);
                    }

                }
                simptomi.Add(simptom);

                foreach (Diagnosis diagnosis in diagnosisController.GetAllDiagnosisBySymptoms(simptomi))
                {
                      potencijalne.Add(diagnosis.Name);
                       
                }

                Simptom.Text = "";
                Dijagnoze.ItemsSource = potencijalne;
                Problems.ItemsSource = simptomi;
            }
        }

        private void CloseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Simptom.Text.Equals(""))
            {
                NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
            }
            else
            {
                MyEvents.ShowMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
            }
        }

        private void Dijagnoze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dijagnoze.SelectedItem != null)
            {
                ModelHCI.DiagnosisHCI diagnosis1 = null;
                String dijagnoza = (String) Dijagnoze.SelectedItem;
                foreach (Diagnosis diagnosis in diagnosisController.GetAllDiagnosis())
                {
 
                        if (diagnosis.Name.ToLower().Equals(dijagnoza))
                        {
                            diagnosis1 = new ModelHCI.DiagnosisHCI() { diagnosis = diagnosis, name = diagnosis.Name, symptoms = diagnosis.Symptoms };
                        }


                }

                if (diagnosis1 != null)
                {
                    var record = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
                    medicalRecordController.UpdateIllnessHistory(diagnosis1.diagnosis, record);
                }
                SetListBoxes();
            }
        }

        private void SetListBoxes()
        {

            IllnessHistoryList.ItemsSource = new List<ModelHCI.DiagnosisHCI>();
            List<ModelHCI.DiagnosisHCI> history = new List<ModelHCI.DiagnosisHCI>();
            var record = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
            List<Symptoms> simptomi = new List<Symptoms>();
            foreach (Diagnosis diagnosis in record.IllnessHistory)
            {
                history.Add(new ModelHCI.DiagnosisHCI() { diagnosis = diagnosis, name = diagnosis.Name, symptoms = diagnosis.Symptoms });
                simptomi.AddRange(diagnosis.Symptoms);
            }
            IllnessHistoryList.ItemsSource = history;

            Problems.ItemsSource = new List<Symptoms>();

            foreach (FamilyIllnessHistory familyIllnessHistory in record.FamilyIllnessHistory)
            {
                foreach (Diagnosis diagnosis in familyIllnessHistory.Diagnosis)
                {
                    simptomi.AddRange(diagnosis.Symptoms);
                }
            }

            Problems.ItemsSource = simptomi;
        }

        private void Simptom_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (Simptom.Text.Equals(""))
            {
                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                Dugmicii.Visibility = Visibility.Visible;
                return;
            }

            Dugmicii.Visibility = Visibility.Collapsed;
            String text = Simptom.Text;
            List<string> medication = new List<string>();
            autoList.ItemsSource = new List<string>();
            foreach (Symptoms med in symptomsController.GetAllSymptoms())
            {
                if (med.Name.ToLower().StartsWith(text.ToLower()))
                {
                    medication.Add(med.Name);
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

        private void Simptom_LostFocus(object sender, RoutedEventArgs e)
        {
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
            Dugmicii.Visibility = Visibility.Visible;
        }

        private void autoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoList.SelectedItem != null)
            {
                string med = (string)autoList.SelectedItem;
                Simptom.Text = med;

                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                Dugmicii.Visibility = Visibility.Visible;
                autoList.ItemsSource = new List<string>();
                List<String> potencijalne = new List<String>();
                List<Symptoms> symptoms = new List<Symptoms>();
                symptoms.Add(new Symptoms(med));
                foreach (Diagnosis diagnosis in diagnosisController.GetAllDiagnosisBySymptoms(symptoms))
                {
                            potencijalne.Add(diagnosis.Name);
                }
                Dijagnoze.ItemsSource = new List<String>();
                Dijagnoze.ItemsSource = potencijalne;


            }
        }
    }
}
