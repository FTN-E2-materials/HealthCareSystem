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

namespace ZdravoKorporacija.Pages.MedicalRecord
{
    /// <summary>
    /// Interaction logic for ShortDescription.xaml
    /// </summary>
    public partial class ShortDescription : Page
    {
        public ShortDescription()
        {
            MyEvents.PatientsChangedEventHandler.CustomEvent += SelectionChanged;

            InitializeComponent();

            RecordNumber.Text = Patients.SelectedRow.record.medicalRecordNumber.ToString();
            HospitalTreatment.Text = Patients.SelectedRow.HospitalTreatment;
            List<string> diagnose = new List<string>();
            diagnose.Add(Patients.SelectedRow.LastDiagnosis);
            Diagnosis_List.ItemsSource = diagnose;


        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            if (e is MyEvents.PatientSelectionChanged)
            {

                RecordNumber.Text = Patients.SelectedRow.record.medicalRecordNumber.ToString();
                HospitalTreatment.Text = Patients.SelectedRow.HospitalTreatment;
                List<string> diagnose = new List<string>();
                diagnose.Add(Patients.SelectedRow.LastDiagnosis);
                Diagnosis_List.ItemsSource = diagnose;
            }
        }

        private void Diagnosis_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Therapy_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.OpenRecord.RaiseMyCustomEvent(this, new MyEvents.PatientSelectionChanged());
            NavigationService.Navigate(new Uri("/Pages/Patients.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyEvents.OpenRecord.RaiseMyCustomEvent(this, new MyEvents.PatientSelectionChanged());
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/MedicalRecordFromPatient.xaml", UriKind.Relative));
        }
    }
}
