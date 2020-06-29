using Controller.MedicalRecordController;
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
    /// Interaction logic for Vaccines.xaml
    /// </summary>
    ///
 
        public class ShowLabTest
    {
        public string name { get; set; }
        public double values { get; set; }
        public double minRefValue { get; set; }
        public double maxRefValue { get; set; }
        public Brush color { get; set; }
        public FontWeight weight { get; set; }
    }
  
    public partial class Vaccines : Page
    {
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MedicalRecordController medicalRecordController;
        public Vaccines()
        {
            var app = Application.Current as App;
            medicalRecordController = app.medicalRecordController;

            InitializeComponent();
            var record = medicalRecordController.GetMedicalRecord(Informations.currentRecord.MedicalRecord.IdRecord);


            List<ShowLabTest> list = new List<ShowLabTest>();
            LabTest.ItemsSource = new List<ShowLabTest>();
            var result = medicalRecordController.GetLastLabResult(record);
            if (result != null) {
                foreach (LabResults results in result.LabResults)
                {
                    list.Add(new ShowLabTest() { name = results.ResultType, maxRefValue = results.MaxRefValue, minRefValue = results.MinRefValue, values = results.Value });
                }

                Date.Text = result.DateOfTesting.ToString("dd.MM.yyyy.");
            }
            List<ModelHCI.VaccineHCI> listVaccine = new List<ModelHCI.VaccineHCI>();

            foreach (var vaccines in record.Vaccines)
            {
                listVaccine.Add(new ModelHCI.VaccineHCI() { vaccine = vaccines.Name, vaccines = vaccines });
            }

            VaccineList.ItemsSource = listVaccine;
            LabTest.ItemsSource = list;
      
        }
        private void NewTest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/AddNewLabTesting.xaml", UriKind.Relative));
        }

        private void CloseAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MedicalRecord/Informations.xaml", UriKind.Relative));
        }
    }
}
