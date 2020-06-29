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
using Controller.MedicationController;
using Model.Medications;

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Medications.xaml
    /// </summary>
    /// 

   
    public partial class Medications : Page
    {
        public static ModelHCI.MedicationHCI SelectedRow = null;
        public static ModelHCI.MedicationHCI SelectedRowStorage = null;
        public MedicationController medicationController;
        public ValidationMedicationController validationMedicationController;
        
        public Medications()
        {   
            InitializeComponent();
            var app = Application.Current as App;
            medicationController = app.medicationController;
            validationMedicationController = app.validationMedicationController;

            MyEvents.OpenRecord.CustomEvent += ClosePanel;
            if (Validation != null)
            {
                Validation.ItemsSource = new List<ModelHCI.MedicationHCI>();
                List<ModelHCI.MedicationHCI> validations = new List<ModelHCI.MedicationHCI>();
                List<string> allergens = new List<string>();
                List<string> ingredients = new List<string>();

                foreach (Medication medication in medicationController.GetAllOnValidationForDoctor(MainWindow.doctor))
                {
                    allergens = new List<string>();
                    ingredients = new List<string>();

                    foreach (Allergens allergen1 in medication.Allergens)
                    {
                        allergens.Add(allergen1.Allergen);
                    }
                    foreach (DosageOfIngredient dosage in medication.MedicationContent)
                    {
                        ingredients.Add(dosage.MedicationIngredient.Name + " " + dosage.Amount + " mg");
                    }

                    validations.Add(new ModelHCI.MedicationHCI(medication.Med, true, allergens, ingredients, "Tableta") { medication = medication });
                }
                Validation.ItemsSource = validations;
                Storage.ItemsSource = new List<ModelHCI.MedicationHCI>();

                List < ModelHCI.MedicationHCI > storage = new List<ModelHCI.MedicationHCI>();

                foreach (Medication medication in medicationController.GetAllApprovedMeds())
                {
                    allergens = new List<string>();
                    ingredients = new List<string>();

                    foreach (Allergens allergen1 in medication.Allergens)
                    {
                        allergens.Add(allergen1.Allergen);
                    }
                    foreach (DosageOfIngredient dosage in medication.MedicationContent)
                    {
                        ingredients.Add(dosage.MedicationIngredient.Name + " " + dosage.Amount + " mg");
                    }


                    storage.Add(new ModelHCI.MedicationHCI(medication.Med, false, allergens, ingredients, "Tableta") { medication = medication });
                }
                Storage.ItemsSource = storage;
            }
        }

        private void ClosePanel(object sender, EventArgs e)
        {
            Panel.SetZIndex(Storage_Grid, 0);
            Panel.SetZIndex(Valid_Grid, 0);
        }

        private void Validation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (Validation != null)
                {

                    if (Validation.SelectedItem is ModelHCI.MedicationHCI)
                    {
                        SelectedRow = (ModelHCI.MedicationHCI) Validation.SelectedItem;
                        Valid_Grid.Margin = new Thickness(0, 0, 0, 0);
                        Valid_Label.Margin = new Thickness(0, 0, 0, 0);
                        Storage_Label.Margin = new Thickness(0, 0, 0, 0);
                        Storage_Grid.Margin = new Thickness(0, 0, 0, 0);
                        Panel.SetZIndex(Storage_Grid, 1);
                        Panel.SetZIndex(Valid_Grid, 1);
                        MyEvents.RefreshMedEventHandelr.RaiseMyCustomEvent(this, new MyEvents.RefreshMeds());
                        Details.Navigate(new Uri("/Pages/Meds/ValidationDetails.xaml", UriKind.Relative));

                    }

                }
            } catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Storage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Storage != null)
                {

                    if (Storage.SelectedItem is ModelHCI.MedicationHCI)
                    {
                        SelectedRowStorage = (ModelHCI.MedicationHCI)Storage.SelectedItem;
                        Valid_Grid.Margin = new Thickness(0, 0, 0, 0);
                        Valid_Label.Margin = new Thickness(0, 0, 0, 0);
                        Storage_Label.Margin = new Thickness(0, 0, 0, 0);
                        Storage_Grid.Margin = new Thickness(0, 0, 0, 0);
                        Panel.SetZIndex(Storage_Grid, 1);
                        Panel.SetZIndex(Valid_Grid, 1);
                        MyEvents.RefreshMedEventHandelr.RaiseMyCustomEvent(this, new MyEvents.RefreshMeds());
                        Details.Navigate(new Uri("/Pages/Meds/StorageMeds.xaml", UriKind.Relative));
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Meds/Report.xaml", UriKind.Relative));
        }
    }
}
