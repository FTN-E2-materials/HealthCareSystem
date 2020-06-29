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
    /// Interaction logic for MedicalRecordFromPatient.xaml
    /// </summary>
    public partial class MedicalRecordFromPatient : Page
    {
        public MedicalRecordFromPatient()
        {
            InitializeComponent();
            if (Patients.SelectedRow.record.MedicalRecord.Patient.ProfileImage != null)
                Profile.Source = new BitmapImage(new Uri(Patients.SelectedRow.record.MedicalRecord.Patient.ProfileImage, UriKind.Relative));

            FullName.Text = Patients.SelectedRow.FullName;
            DateAppointment.Text = Patients.SelectedRow.LastAppointment;
            
            Record_Number.Text = Patients.SelectedRow.record.medicalRecordNumber.ToString();
            Gender.Text = "Muški";
            ID_Number.Text = Patients.SelectedRow.ID_NUMBER;
            DateOfBirth.Text = Patients.SelectedRow.dateOfBirth.ToString("dd.MM.yyyy.");
            Curr_Residence.Text = Patients.SelectedRow.City;
            State.Text = Patients.SelectedRow.State;
        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/Prescription.xaml", UriKind.Relative));
        }

        private void SpecialistsExam_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/SpecialistsExam.xaml", UriKind.Relative));
        }

        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {

            Frejm.Navigate(new Uri("/Pages/MedicalRecord/NewAppointment.xaml", UriKind.Relative));
        }

        private void HospitalTreatment_Click(object sender, RoutedEventArgs e)
        {
            Frejm.Navigate(new Uri("/Pages/MedicalRecord/HospitalTreatment.xaml", UriKind.Relative));
        }

        private void Vaccines_Click(object sender, RoutedEventArgs e)
        {

            Frejm.Navigate(new Uri("/Pages/MedicalRecord/Vaccines.xaml", UriKind.Relative));
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Patients.xaml", UriKind.Relative));
        }
    }
}
