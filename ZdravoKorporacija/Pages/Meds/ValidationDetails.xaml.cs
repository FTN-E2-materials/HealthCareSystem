using Controller.MedicationController;
using Model.Medications;
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

namespace ZdravoKorporacija.Pages.Meds
{
    /// <summary>
    /// Interaction logic for ValidationDetails.xaml
    /// </summary>
    public partial class ValidationDetails : Page
    {
        public ValidationMedicationController validationMedicationController;
        
        public ValidationDetails()
        {
            InitializeComponent();
            var app = Application.Current as App;
            validationMedicationController = app.validationMedicationController;
            MyEvents.RefreshMedEventHandelr.CustomEvent += RefreshView;
            if (Allergens != null && Ingredients != null)
            {
                Allergens.ItemsSource = new List<String>();
                Ingredients.ItemsSource = new List<String>();
                Allergens.ItemsSource = Medications.SelectedRow.allergens;
                Ingredients.ItemsSource = Medications.SelectedRow.ingredients;
                Format.Text = Medications.SelectedRow.format;
                Manufactuer.Text = Medications.SelectedRow.Manufactuer;
                Name.Text = Medications.SelectedRow.name;

                List<string> sideEffects = new List<string>();

                string koliko = "";
                foreach (var sideffect in Medications.SelectedRow.medication.SideEffects)
                {
                    if (sideffect.Frequency == SideEffectFrequency.common)
                    {
                        koliko = "često";
                    } else if (sideffect.Frequency == SideEffectFrequency.rare)
                    {
                        koliko = "retko";
                    } else if (sideffect.Frequency == SideEffectFrequency.veryCommon)
                    {
                        koliko = "veoma često";
                    }
                    sideEffects.Add(sideffect.SideEffects.Name + " " + koliko);
                }
                RiskFactors.ItemsSource = sideEffects;
            }
        }

        private void RefreshView(object sender, EventArgs e)
        {
            if (Allergens != null && Ingredients != null)
            {
                Allergens.ItemsSource = new List<String>();
                Ingredients.ItemsSource = new List<String>();
                Allergens.ItemsSource = Medications.SelectedRow.allergens;
                Ingredients.ItemsSource = Medications.SelectedRow.ingredients;
                Format.Text = Medications.SelectedRow.format;
                Manufactuer.Text = Medications.SelectedRow.Manufactuer;
                Name.Text = Medications.SelectedRow.name;
            }
        }

        private void Success_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Yes.IsChecked)
            {
                ModelHCI.MedicationData.meds.Add(Medications.SelectedRow);
                ModelHCI.MedicationData.medsToValidate.Remove(Medications.SelectedRow);

                ValidationMed validationMed = new ValidationMed();
                validationMed.Approved = true;
                validationMed.DateOfValidation = DateTime.Today;
                validationMed.Medication = Medications.SelectedRow.medication;
                validationMed.Reviewed = false;
                validationMed.Doctor = MainWindow.doctor;
                validationMed.SideNotes = Napomene.Text;
                validationMedicationController.AddValidationMedication(validationMed);

            }
            MyEvents.OpenRecord.RaiseMyCustomEvent(this, new MyEvents.CloseAddArticle());
            NavigationService.Navigate(new Uri("/Pages/Medications.xaml", UriKind.Relative));

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.OpenRecord.RaiseMyCustomEvent(this, new MyEvents.CloseAddArticle());
            NavigationService.Navigate(new Uri("/Pages/Medications.xaml", UriKind.Relative));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.OpenRecord.RaiseMyCustomEvent(this, new MyEvents.CloseAddArticle());
            NavigationService.Navigate(new Uri("/Pages/Medications.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Yes.IsChecked || (bool)No.IsChecked)
            {
                ModelHCI.MedicationData.meds.Add(Medications.SelectedRow);
                ModelHCI.MedicationData.medsToValidate.Remove(Medications.SelectedRow);

                ModelHCI.ValidationMed valid = new ModelHCI.ValidationMed();
                valid.medThatIsValidated = Medications.SelectedRow;
                valid.dateOfValidation = DateTime.Today;
                valid.doctorWhoValidated = MainWindow.loggedInDoctor;
                valid.safe = true;

                ModelHCI.ReportData.validations.Add(valid);
                ExportValidation.ExportAsPDFValidation(valid);
            }
        }
    }
}
