using Controller.MedicalRecordController;
using Controller.MedicationController;
using Model.MedicalRecord;
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

namespace ZdravoKorporacija.Pages.MedicalRecord
{
    /// <summary>
    /// Interaction logic for Informations.xaml
    /// </summary>
    public partial class Informations : Page
    {
        public static ModelHCI.MedicalRecordHCI currentRecord = null;
        public static bool demoOn = false;
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MedicalRecordController medicalRecordController;
        public MedicationController medicationController;
        public DiagnosisController diagnosisController;

        public Informations()
        {
            MyEvents.DemoEvents.ContinueDemoInformations.CustomEvent += continueDemo;
            MyEvents.DemoEvents.PropagateCancellation.CustomEvent += cancelDemo;
            var app = Application.Current as App;
            medicalRecordController = app.medicalRecordController;
            medicationController = app.medicationController;
            diagnosisController = app.diagnosisController;

            if (Patients.SelectedRow != null)
            {
                currentRecord = new ModelHCI.MedicalRecordHCI() { MedicalRecord = medicalRecordController.GetMedicalRecord(Patients.SelectedRow.record.MedicalRecord.IdRecord) };
            } else if (Appointments.currentExamination != null)
            {
                currentRecord = new ModelHCI.MedicalRecordHCI() { MedicalRecord = medicalRecordController.GetMedicalRecord(Appointments.currentExamination.record.MedicalRecord.IdRecord) };
            }


            InitializeComponent();

            MyEvents.ChangeInformation.CustomEvent += ChangeInfo;
            TherapyList.ItemsSource = new List<ModelHCI.TherapyHCI>();
            AllergiesList.ItemsSource = new List<ModelHCI.AllergensHCI>();
            IllnessHistory.ItemsSource = new List<ModelHCI.DiagnosisHCI>();
            FamilyIllnessHistory.ItemsSource = new List<ModelHCI.DiagnosisHCI>();

            SetValues();
        }

        private void cancelDemo(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void continueDemo(object sender, EventArgs e)
        {
            if (Menu.demoOn)
            {
                continueDemoAsync(sender, e, cts.Token);
            }
        }

        private async Task continueDemoAsync(object sender, EventArgs e, CancellationToken token)
        {
            demoOn = true;

            if (e is MyEvents.DemoEvents.ContinueInfoArgs)
            {
                var txt = (MyEvents.DemoEvents.ContinueInfoArgs)e;
                if (txt.text.Equals("terapije"))
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    await Task.Delay(2000);
                    String text = "Pritiskom na edit dugme kod Terapija i Alergija, otvarate deo za izmenu istih..";
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
                    NavigationService.Navigate(new Uri("/Pages/MedicalRecord/EditAllergensAndTherapies.xaml", UriKind.Relative));
                } else if (txt.text.Equals("dijagnoze"))
                {

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    await Task.Delay(2000);
                    String text = "Pritiskom na edit dugme kod Problema i istorije, otvarate deo za izmenu istih..";
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
                    NavigationService.Navigate(new Uri("/Pages/MedicalRecord/EditIllnessHistory.xaml", UriKind.Relative));

                } else if (txt.text.Equals("kraj"))
                {

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    Appointments.last = false;
                    MyEvents.DemoEvents.ContinueDemoEventHandler.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ContinueDemo() { textFrom = "kraj" });
                }
            }
        }

        private void ChangeInfo(object sender, EventArgs e)
        {
            SetValues();
        }

        public void SetValues()
        {
            if (currentRecord != null)
            {
                List<ModelHCI.AllergensHCI> source = new List<ModelHCI.AllergensHCI>();

                foreach (Allergens allergens in currentRecord.MedicalRecord.Allergies)
                {
                    source.Add(new ModelHCI.AllergensHCI() { allergen = allergens.Allergen, allergens = allergens });
                }

                AllergiesList.ItemsSource = source;

                List<ModelHCI.TherapyHCI> therapies = new List<ModelHCI.TherapyHCI>();

                foreach (Therapy therapy in currentRecord.MedicalRecord.Therapies)
                {
                    Medication medication = medicationController.GetMedication(therapy.Medication.MedId);
                    therapies.Add(new ModelHCI.TherapyHCI()
                    {
                        medication = new ModelHCI.MedicationHCI()
                        {
                            name = medication.Med,
                            medication = medication
                        },
                        therapy = therapy
                    });
                }

                TherapyList.ItemsSource = therapies;

                List<ModelHCI.DiagnosisHCI> ilnesses = new List<ModelHCI.DiagnosisHCI>();

                foreach (FamilyIllnessHistory therapy in currentRecord.MedicalRecord.FamilyIllnessHistory)
                {
                    foreach (Diagnosis diagnosis in therapy.Diagnosis)
                    {
                        Diagnosis diag = diagnosisController.GetDiagnosis(diagnosis.Code);
                        ilnesses.Add(new ModelHCI.DiagnosisHCI() { name = diag.Name, diagnosis = diag });
                    }
                }
            
                FamilyIllnessHistory.ItemsSource = ilnesses;

                ilnesses = new List<ModelHCI.DiagnosisHCI>();

                foreach (Diagnosis diagnosis1 in currentRecord.MedicalRecord.IllnessHistory)
                {
                    Diagnosis diag = diagnosisController.GetDiagnosis(diagnosis1.Code);
                    ilnesses.Add(new ModelHCI.DiagnosisHCI() { name = diag.Name, diagnosis = diag });
                }

                IllnessHistory.ItemsSource = ilnesses;
                

            }
        }

        private void EditAllergiesAndTherapies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/EditAllergensAndTherapies.xaml", UriKind.Relative));
        }

        private void EditHistory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/EditIllnessHistory.xaml", UriKind.Relative));
        }
    }
}
