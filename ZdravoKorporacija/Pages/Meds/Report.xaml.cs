using Controller.MedicationController;
using Controller.ReportController;
using Model.Medications;
using Model.Reports;
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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public const string ERROR1 = "Početni datum mora biti pre današnjeg!";
        public const string ERROR2 = "Morate popuni sve podatke!";
        public static Medication medToGenerate = null;

        public MedicationController medicationController;
        public ReportController reportController;
        
        public Report()
        {
            var app = Application.Current as App;
            medicationController = app.medicationController;
            reportController = app.reportController;

            InitializeComponent();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Filterii.Visibility = Visibility.Visible;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.SelectedDate == null && EndDate.SelectedDate == null && Lek.Text.Equals(""))
            {
                
            }
            if ((StartDate.SelectedDate == null) || (EndDate.SelectedDate == null) || ((Lek.Text.Equals("")) && (!(bool)SviLekovi.IsChecked)))
            {
                Error.Text = ERROR2;
                ErrorMessage.Visibility = Visibility.Visible;
            } else
            {
                if (!(bool)SviLekovi.IsChecked)
                {
                    
                    foreach (Medication medic in medicationController.GetAllApprovedMeds())
                    {
                        if (medic.Med.ToLower().Equals(Lek.Text.ToLower()))
                        {
                            medToGenerate = medic;
                        }
                    }
                    MedicationConsumptionReport report = reportController.GenerateMedicalConsumptionReport((DateTime)StartDate.SelectedDate, (DateTime)EndDate.SelectedDate, medToGenerate);
                    ExportToPDF.ExportAsPDF(report);
                } else
                {
                }

                NavigationService.Navigate(new Uri("Pages/Medications.xaml", UriKind.Relative));


            }
           


        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDate.SelectedDate != null)
            {
                if (((DateTime) StartDate.SelectedDate).CompareTo(DateTime.Today) > 0)
                {
                    Error.Text = ERROR1;
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate.SelectedDate != null)
            {

            }
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
                NavigationService.Navigate(new Uri("Pages/Medications.xaml", UriKind.Relative));
        }

        private void autoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoList.SelectedItem != null)
            {
                string med = (string)autoList.SelectedItem;
                Lek.Text = med;

                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                autoList.ItemsSource = new List<string>();

                foreach (Medication meds in medicationController.GetAllApprovedMeds())
                {
                    if (meds.Med.ToLower().Equals(med.ToLower()))
                    {
                        medToGenerate = meds;
                    }
                }

                if (medToGenerate != null)
                {
                    SviLekovi.IsEnabled = false;
                }
            }

        }

        private void Lek_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (Lek.Text.Equals(""))
            {
                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
                SviLekovi.IsEnabled = true;
                return;
            }


            String text = Lek.Text;
            List<string> medication = new List<string>();
            autoList.ItemsSource = new List<string>();
            foreach (Medication med in medicationController.GetAllApprovedMeds())
            {
                if (med.Med.ToLower().StartsWith(text.ToLower()))
                {
                    medication.Add(med.Med);                    
                }
            }

            if (medication.Count > 0)
            {
                autoList.Visibility = Visibility.Visible;
                boxic.Visibility = Visibility.Visible;
                autoList.ItemsSource = medication;
            } else
            {
                autoList.Visibility = Visibility.Collapsed;
                boxic.Visibility = Visibility.Collapsed;
            }
        }

        private void SviLekovi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Lek.Text = "";
            autoList.Visibility = Visibility.Collapsed;
            boxic.Visibility = Visibility.Collapsed;
        }

        private void autoList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
