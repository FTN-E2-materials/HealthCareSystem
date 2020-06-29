using Controller.MedicalRecordController;
using Controller.MedicationController;
using Model.MedicalRecord;
using Model.Medications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoKorporacija.Pages.MedicalRecord
{
    /// <summary>
    /// Interaction logic for EditAllergensAndTherapies.xaml
    /// </summary>
    public partial class EditAllergensAndTherapies : Page
    {
        public static ModelHCI.AllergensHCI selectedAllergy = null;
        public static ModelHCI.TherapyHCI selectedTherapy = null;
        public MedicalRecordController medicalRecordController;
        public MedicationController medicationController;
        public AllergensController allergensController;

        public const string ERROR_ADD = "Molim Vas, unesite nove alergiju/terapiju pre dodavanja.";
        public const string ERROR_DELETE = "Pre brisanja morate ozačiti stavku iz liste.";
        public CancellationTokenSource cts = new CancellationTokenSource();
        public EditAllergensAndTherapies()
        {
            var app = Application.Current as App;
            medicalRecordController = app.medicalRecordController;
            medicationController = app.medicationController;
            allergensController = app.allergensController;

            InitializeComponent();
            setListBoxes();

            if (Informations.demoOn)
            {
                continueDemoAsync(cts.Token);
            }
        }

        private async Task continueDemoAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(2000);
            String text = "Popunjavanjem polja za upis terapije i pritiskom na plus, dodaće novu terapiju";
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

            string lek = "Sinacilin";
            foreach (char ch in lek)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                NewTherapy.Text += ch;
                await Task.Delay(200);
            }
            if (token.IsCancellationRequested)
            {
                return;
            }
            await Task.Delay(1000);
            TherapyList.Items.Add(lek);
            NewTherapy.Text = "";
            text = "Pritiskom na dugme nazad, vraćate se osnovne informacije.";
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
            MyEvents.DemoEvents.ContinueDemoInformations.RaiseMyCustomEvent(this, new MyEvents.DemoEvents.ContinueInfoArgs() { text = "dijagnoze" });
            if (token.IsCancellationRequested)
            {
                return;
            }
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
        }

        private void setListBoxes()
        {

            AllergiesList.ItemsSource = new List<ModelHCI.AllergensHCI>();
            TherapyList.ItemsSource = new List<ModelHCI.TherapyHCI>();
            ModelHCI.MedicalRecordHCI currentRecord = new ModelHCI.MedicalRecordHCI() { MedicalRecord = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord) };
            

            List<ModelHCI.AllergensHCI> source = new List<ModelHCI.AllergensHCI>();

            foreach (Allergens allergens in currentRecord.MedicalRecord.Allergies)
            {
                source.Add(new ModelHCI.AllergensHCI() { allergen = allergens.Allergen, allergens = allergens });
            }

            AllergiesList.ItemsSource = source;

            List<ModelHCI.TherapyHCI> therapies = new List<ModelHCI.TherapyHCI>();

            foreach (Therapy therapy in currentRecord.MedicalRecord.Therapies)
            {
                therapies.Add(new ModelHCI.TherapyHCI()
                {
                    medication = new ModelHCI.MedicationHCI()
                    {
                        name = therapy.Medication.Med,
                        medication = therapy.Medication
                    },
                    therapy = therapy
                });
            }

            TherapyList.ItemsSource = therapies;

        }
        private void DeleteTherapy_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTherapy == null)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ERROR_DELETE;
                return;
            }

            ModelHCI.MedicalRecordHCI record = Informations.currentRecord;
            var recordToUpdate = record.MedicalRecord;
            Therapy therapyToDelete = null;

            foreach (Therapy therapy in recordToUpdate.Therapies)
            {
                Medication med = medicationController.GetMedication(therapy.Medication.MedId);
                if (med.Med.Equals(selectedTherapy.medication.medication.Med))
                {
                    therapyToDelete = therapy;
                }
            }
            if (therapyToDelete != null) 
                recordToUpdate.Therapies.Remove(therapyToDelete);

            var recordUpdate = medicalRecordController.GetMedicalRecord(recordToUpdate.IdRecord);

            if (therapyToDelete != null)
                recordUpdate.Therapies.Remove(therapyToDelete);
            Informations.currentRecord.MedicalRecord = medicalRecordController.UpdateRecord(recordToUpdate);

            setListBoxes();
        }

        private void AddTherapy_Click(object sender, RoutedEventArgs e)
        {
            if (NewTherapy.Text.Equals(""))
            {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ERROR_ADD;
                return;
            }
            Medication medication = null;

            foreach (Medication meds in medicationController.GetAll())
            {
                if (meds.Med.ToLower().Equals(NewTherapy.Text.ToLower()))
                {
                    medication = meds;
                }
            }

            if (medication != null)
            {
                Therapy therapy = new Therapy(0, medication);
                var record = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
                Informations.currentRecord.MedicalRecord = medicalRecordController.UpdateTherapy(therapy, record);
            }

            setListBoxes();

        }

        private void DeleteAllergies_Click(object sender, RoutedEventArgs e)
        {

            if (selectedAllergy == null)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ERROR_DELETE;
                return;
            }


            ModelHCI.MedicalRecordHCI record = Informations.currentRecord;
            var recordToUpdate = record.MedicalRecord;
            Allergens allergyToDelete = null;

            foreach (Allergens allergens in recordToUpdate.Allergies)
            {
                if (allergens.Allergen.Equals(selectedAllergy.allergens.Allergen))
                {
                    allergyToDelete = allergens;
                }
            }
            if (allergyToDelete != null)
                recordToUpdate.Allergies.Remove(allergyToDelete);

            var recordUpdate = medicalRecordController.GetMedicalRecord(recordToUpdate.IdRecord);

            if (allergyToDelete != null)
                recordUpdate.Allergies.Remove(allergyToDelete);
            medicalRecordController.UpdateRecord(recordToUpdate);
            Informations.currentRecord.MedicalRecord = medicalRecordController.GetMedicalRecord(recordToUpdate.IdRecord);

            setListBoxes();
        }

        private void TherapyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TherapyList.SelectedItem != null)
            {
                if (TherapyList.SelectedItem is ModelHCI.TherapyHCI)
                {
                    var row = (ModelHCI.TherapyHCI)TherapyList.SelectedItem;


                    if (row != null)
                    {
                        selectedTherapy = row;
                    }
                }
            }

        }

        private void AllergiesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllergiesList.SelectedItem != null)
            {
                if (AllergiesList.SelectedItem is ModelHCI.AllergensHCI)
                {
                    var row = (ModelHCI.AllergensHCI)AllergiesList.SelectedItem;


                    if (row != null)
                    {
                        selectedAllergy = row;
                    }
                }
            }

        }

        private void AddAllergy_Click(object sender, RoutedEventArgs e)
        {
            if (NewAllergy.Text.Equals(""))
            {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ERROR_ADD;
                return;
            }
            Allergens allergenToAdd = null;
            var recordToUpdate = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);
            foreach (Allergens allergens in allergensController.GetAllAllergies())
            {
                if (allergens.Allergen.ToLower().Equals(NewAllergy.Text))
                {
                    allergenToAdd = allergens;
                }
            }

            if (allergenToAdd != null)
            medicalRecordController.UpdateAllergies(allergenToAdd, recordToUpdate);
            Informations.currentRecord.MedicalRecord = medicalRecordController.GetMedicalRecord(recordToUpdate.IdRecord);

            setListBoxes();
        }

        private void CloseAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
        }

        private void therapyauto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (therapyauto.SelectedItem != null)
            {
                NewTherapy.Text = (string)therapyauto.SelectedItem;
                therapyauto.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
            }
        }

        private void allergyauto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allergyauto.SelectedItem != null)
            {
                NewAllergy.Text = (string)allergyauto.SelectedItem;
                allergyauto.Visibility = Visibility.Collapsed;
                boxic2.Visibility = Visibility.Collapsed;
            }
        }

        private void NewAllergy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewAllergy.Text.Equals(""))
            {
                allergyauto.Visibility = Visibility.Collapsed;
                boxic2.Visibility = Visibility.Collapsed;
                return;
            }

            String text = NewAllergy.Text;
            List<string> medication = new List<string>();
            allergyauto.ItemsSource = new List<string>();

            foreach (Allergens med in allergensController.GetAllAllergies())
            {
                if (med.Allergen.ToLower().StartsWith(NewAllergy.Text.Trim().ToLower()))
                {
                    medication.Add(med.Allergen);
                }
            }

            if (medication.Count > 0)
            {
                allergyauto.Visibility = Visibility.Visible;
                boxic2.Visibility = Visibility.Visible;
                allergyauto.ItemsSource = medication;
            }
            else
            {
                allergyauto.Visibility = Visibility.Collapsed;
                boxic2.Visibility = Visibility.Collapsed;
            }
        }

        private void NewTherapy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewTherapy.Text.Equals(""))
            {
                therapyauto.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                return;
            }

            String text = NewTherapy.Text;
            List<string> medication = new List<string>();
            therapyauto.ItemsSource = new List<string>();
            foreach (Medication med in medicationController.GetAll())
            {
                if (med.Med.ToLower().StartsWith(NewTherapy.Text.Trim().ToLower()))
                {
                    medication.Add(med.Med);
                }
            }

            if (medication.Count > 0)
            {
                therapyauto.Visibility = Visibility.Visible;
                boxic.Visibility = Visibility.Visible;
                therapyauto.ItemsSource = medication;
            }
            else
            {
                therapyauto.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
            }
        }

        private void NewTherapy_LostFocus(object sender, RoutedEventArgs e)
        {
            therapyauto.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
        }

        private void NewAllergy_LostFocus(object sender, RoutedEventArgs e)
        {
            allergyauto.Visibility = Visibility.Collapsed;
            boxic2.Visibility = Visibility.Collapsed;
        }
    }
}
