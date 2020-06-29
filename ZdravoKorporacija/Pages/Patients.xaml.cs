using Controller.MedicalRecordController;
using Model.MedicalRecord;
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

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page
    {
        public static List<ModelHCI.Patient> listChanged = new List<ModelHCI.Patient>();
        public static ModelHCI.Patient SelectedRow = null;

        public MedicalRecordController medicalRecordController;

        public Patients()
        {
            var app = Application.Current as App;
            medicalRecordController = app.medicalRecordController;

            MyEvents.OpenRecord.CustomEvent += ClosePanel;
            InitializeComponent();
            SelectedRow = null;

            if (ComboBox != null)
            {
                ComboBox.Items.Add("Svi");
                ComboBox.Items.Add("Bolničko lečenje");
                ComboBox.Items.Add("Hitni");
                ComboBox.SelectedIndex = 0;
            }
            if (Patients_Data != null)
            {
                SetPatientList();
            }
        }

        private void SetPatientList()
        {
            List<ModelHCI.Patient> patients = new List<ModelHCI.Patient>();
            foreach (var record in medicalRecordController.GetRecordsForDoctor(MainWindow.doctor))
            {
                string diagnosis = "";
                if (record.IllnessHistory.Count > 0)
                    diagnosis = record.IllnessHistory.ToArray()[record.IllnessHistory.Count - 1].Name;

                string opste_stanje = "";

                if (record.CurrHealthState == PatientCondition.urgent)
                    opste_stanje = "HITNO";
                else if (record.CurrHealthState == PatientCondition.stable)
                    opste_stanje = "STABILNO";
                else if (record.CurrHealthState == PatientCondition.hospitalTreatment)
                    opste_stanje = "BOLNIČKO LEČENJE";
                else if (record.CurrHealthState == PatientCondition.homeTreatment)
                    opste_stanje = "KUĆNO LEČENE";

                patients.Add(new ModelHCI.Patient()
                {
                    dateOfBirth = record.Patient.DateOfBirth,
                    ID_NUMBER = record.Patient.IdentificationNumber,
                    State = record.Patient.CurrResidence.City.State.Name,
                    City = record.Patient.CurrResidence.City.Name,
                    LastDiagnosis = diagnosis,
                    GeneralState = opste_stanje,
                    HospitalTreatment = record.CurrHealthState == PatientCondition.hospitalTreatment ? "DA" : "NE",
                    FullName = record.Patient.Name + " " + record.Patient.Surname,
                    Age = ((DateTime.Today.Year) - record.Patient.DateOfBirth.Year).ToString(),
                    record = new ModelHCI.MedicalRecordHCI() { MedicalRecord = record, medicalRecordNumber =record.IdRecord }
                });
                 
                
                
            }
        }

        private void ClosePanel(object sender, EventArgs e)
        {

            Panel.SetZIndex(Patients_Data, 0);
            Panel.SetZIndex(ComboBox, 0);
            Panel.SetZIndex(SearchBox, 0);
        }

        private void Patients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Patients_Data.SelectedItem != null)
                {
                    if (Patients_Data.SelectedItem is ModelHCI.Patient)
                    {
                        var row = (ModelHCI.Patient)Patients_Data.SelectedItem;


                        if (row != null)
                        {
                            SelectedRow = row;

                            MoreInfo.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox != null && Patients_Data != null)
            {
                String text = SearchBox.Text;
                listChanged = new List<ModelHCI.Patient>();
                foreach (var record in medicalRecordController.GetRecordsForDoctor(MainWindow.doctor))
                {
                    string diagnosis = "";
                    if (record.IllnessHistory.Count > 0)
                        diagnosis = record.IllnessHistory.ToArray()[record.IllnessHistory.Count - 1].Name;
                    string opste_stanje = "";

                    if (record.CurrHealthState == PatientCondition.urgent)
                        opste_stanje = "HITNO";
                    else if (record.CurrHealthState == PatientCondition.stable)
                        opste_stanje = "STABILNO";
                    else if (record.CurrHealthState == PatientCondition.hospitalTreatment)
                        opste_stanje = "BOLNIČKO LEČENJE";
                    else if (record.CurrHealthState == PatientCondition.homeTreatment)
                        opste_stanje = "KUĆNO LEČENE";

                    if (record.Patient.Name.ToLower().Contains(text) || record.Patient.Surname.ToLower().Contains(text) || record.IdRecord.ToString().Contains(text))
                    {

                        listChanged.Add(new ModelHCI.Patient()
                        {
                            dateOfBirth = record.Patient.DateOfBirth,
                            ID_NUMBER = record.Patient.IdentificationNumber,
                            State = record.Patient.CurrResidence.City.State.Name,
                            City = record.Patient.CurrResidence.City.Name,
                            LastDiagnosis = diagnosis,
                            GeneralState = opste_stanje,
                            HospitalTreatment = record.CurrHealthState == PatientCondition.hospitalTreatment ? "DA" : "NE",
                            FullName = record.Patient.Name + " " + record.Patient.Surname,
                            Age = ((DateTime.Today.Year) - record.Patient.DateOfBirth.Year).ToString(),
                            record = new ModelHCI.MedicalRecordHCI() { MedicalRecord = record, medicalRecordNumber = record.IdRecord }
                        })  ;
                    }


                }
                Patients_Data.ItemsSource = new List<ModelHCI.Patient>();
                Patients_Data.ItemsSource = listChanged;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ComboBox.SelectedItem != null)
                {
                    int index = ComboBox.SelectedIndex;
                    listChanged = new List<ModelHCI.Patient>();

                    if (index == 0)
                    {
                        foreach (var record in medicalRecordController.GetRecordsForDoctor(MainWindow.doctor))
                        {
                            string opste_stanje = "";

                            if (record.CurrHealthState == PatientCondition.urgent)
                                opste_stanje = "HITNO";
                            else if (record.CurrHealthState == PatientCondition.stable)
                                opste_stanje = "STABILNO";
                            else if (record.CurrHealthState == PatientCondition.hospitalTreatment)
                                opste_stanje = "BOLNIČKO LEČENJE";
                            else if (record.CurrHealthState == PatientCondition.homeTreatment)
                                opste_stanje = "KUĆNO LEČENE";

                            string diagnosis = "";
                                if (record.IllnessHistory.Count > 0)
                                    diagnosis = record.IllnessHistory.ToArray()[record.IllnessHistory.Count - 1].Name;
                                listChanged.Add(new ModelHCI.Patient()
                                {
                                    dateOfBirth = record.Patient.DateOfBirth,
                                    ID_NUMBER = record.Patient.IdentificationNumber,
                                    State = record.Patient.CurrResidence.City.State.Name,
                                    City = record.Patient.CurrResidence.City.Name,
                                    LastDiagnosis = diagnosis,
                                    GeneralState = opste_stanje,
                                    HospitalTreatment = record.CurrHealthState == PatientCondition.hospitalTreatment ? "DA" : "NE",
                                    FullName = record.Patient.Name + " " + record.Patient.Surname,
                                    Age = ((DateTime.Today.Year) - record.Patient.DateOfBirth.Year).ToString(),
                                    record = new ModelHCI.MedicalRecordHCI() { MedicalRecord = record, medicalRecordNumber = record.IdRecord }
                                }); ;
                           
                        }
                        Patients_Data.ItemsSource = new List<ModelHCI.Patient>();
                        Patients_Data.ItemsSource = listChanged;
                    } else if (index == 1)
                    {
                        foreach (var record in medicalRecordController.GetRecordsForDoctor(MainWindow.doctor))
                        {
                            if (record.CurrHealthState == PatientCondition.hospitalTreatment)
                            {
                                string diagnosis = "";
                                if (record.IllnessHistory.Count > 0)
                                    diagnosis = record.IllnessHistory.ToArray()[record.IllnessHistory.Count - 1].Name;
                                listChanged.Add(new ModelHCI.Patient()
                                {
                                    dateOfBirth = record.Patient.DateOfBirth,
                                    ID_NUMBER = record.Patient.IdentificationNumber,
                                    State = record.Patient.CurrResidence.City.State.Name,
                                    City = record.Patient.CurrResidence.City.Name,
                                    LastDiagnosis = diagnosis,
                                    GeneralState = "BOLNIČKO LEČENJE",
                                    HospitalTreatment = record.CurrHealthState == PatientCondition.hospitalTreatment ? "DA" : "NE",
                                    FullName = record.Patient.Name + " " + record.Patient.Surname,
                                    Age = ((DateTime.Today.Year) - record.Patient.DateOfBirth.Year).ToString(),
                                    record = new ModelHCI.MedicalRecordHCI() { MedicalRecord = record, medicalRecordNumber = record.IdRecord }
                                }); ;
                            }
                        }
                        Patients_Data.ItemsSource = new List<ModelHCI.Patient>();
                        Patients_Data.ItemsSource = listChanged;
                    } else if (index == 2)
                    {
                        foreach (var record in medicalRecordController.GetRecordsForDoctor(MainWindow.doctor))
                        {
                            if (record.CurrHealthState == PatientCondition.urgent)
                            {
                                string diagnosis = "";
                                if (record.IllnessHistory.Count > 0)
                                    diagnosis = record.IllnessHistory.ToArray()[record.IllnessHistory.Count - 1].Name;
                                listChanged.Add(new ModelHCI.Patient()
                                {
                                    dateOfBirth = record.Patient.DateOfBirth,
                                    ID_NUMBER = record.Patient.IdentificationNumber,
                                    State = record.Patient.CurrResidence.City.State.Name,
                                    City = record.Patient.CurrResidence.City.Name,
                                    LastDiagnosis = diagnosis,
                                    GeneralState = "HITNO",
                                    HospitalTreatment = record.CurrHealthState == PatientCondition.hospitalTreatment ? "DA" : "NE",
                                    FullName = record.Patient.Name + " " + record.Patient.Surname,
                                    Age = ((DateTime.Today.Year) - record.Patient.DateOfBirth.Year).ToString(),
                                    record = new ModelHCI.MedicalRecordHCI() { MedicalRecord = record, medicalRecordNumber = record.IdRecord }
                                });
                            }
                        }
                    }
                        Patients_Data.ItemsSource = new List<ModelHCI.Patient>();
                        Patients_Data.ItemsSource = listChanged;
                    }
            }
            catch (Exception)
            {
            }
        }

        private void MoreInfo_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/MedicalRecordFromPatient.xaml", UriKind.Relative));
        }
    }
}
