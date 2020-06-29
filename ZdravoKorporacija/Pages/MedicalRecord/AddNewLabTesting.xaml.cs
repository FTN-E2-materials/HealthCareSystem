using Controller.ExaminationController;
using Model.ExaminationSurgery;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for AddNewLabTesting.xaml
    /// </summary>
    public partial class AddNewLabTesting : Page
    {
        public static bool finishedDemo = false;
        public CancellationTokenSource cts = new CancellationTokenSource();
        public List<LabTestType> types = new List<LabTestType>();
        public TreatmentController treatmentController;
        public class Show
        {
            public string Name { get; set; }
            public string Tag { get; set; }
        }

        public LabTestTypeController labTestTypeController;
        public ExaminationSurgeryController ExaminationSurgeryController;
        public AddNewLabTesting()
        {
            var app = Application.Current as App;
            labTestTypeController = app.labTestTypeController;
            ExaminationSurgeryController = app.examinationSurgeryController;
            treatmentController = app.treatmentController;

            InitializeComponent();
            List<String> list1 = new List<String>();
            List<String> list2 = new List<String>();
            List<String> list3 = new List<String>();

            double count = Math.Ceiling((double)(labTestTypeController.GetAllTestTypes().ToList().Count / 3));
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            

            foreach (LabTestType type in labTestTypeController.GetAllTestTypes())
            {
                byte[] utfBytes = iso.GetBytes(type.TestName);
                byte[] isoBytes = Encoding.Convert(iso, utf8, utfBytes);
                string msg = utf8.GetString(isoBytes);
                if (list1.Count <= count && list2.Count == 0 && list3.Count == 0)
                {
                    list1.Add(msg);
                }
                else if (list2.Count <= count && list1.Count >= 0 && list3.Count == 0)
                {
                    list2.Add(msg);
                }
                else if (list2.Count >= count && list1.Count >= 0)
                {
                    list3.Add(msg);
                }
            }
            foreach (string str in list1)
            {
                listTopics.Items.Add(new Show() { Name = str, Tag = str });
                listTopics.SelectionChanged += selection1;
            }

            foreach (string str in list2)
            {
                listTopics2.Items.Add(new Show() { Name = str, Tag = str });

                listTopics.SelectionChanged += selection2;
            }
            foreach (string str in list3)
            {
                listTopics3.Items.Add(new Show() { Name = str, Tag = str });

                listTopics.SelectionChanged += selection3;
            }


        }

        private void selection2(object sender, SelectionChangedEventArgs e)
        {
            if (listTopics3.SelectedItem != null)
            {
                var name = ((Show)listTopics3.SelectedItem).Name;
                LabTestType type = new LabTestType(name);
                types.Add(type);
            }
        }

        private void selection3(object sender, SelectionChangedEventArgs e)
        {
            if (listTopics3.SelectedItem != null)
            {
                var name = ((Show)listTopics3.SelectedItem).Name;
                LabTestType type = new LabTestType(name);
                types.Add(type);
            }
        }

        private void selection1(object sender, SelectionChangedEventArgs e)
        {
            if (listTopics.SelectedItem != null)
            {
                var name = ((Show)listTopics.SelectedItem).Name;
                LabTestType type = new LabTestType(name);
                types.Add(type);
            }
        }

        private void AddPrescription_Click(object sender, RoutedEventArgs e)
        {


            TreatmentForm treatmentForm = new TreatmentForm();
            treatmentForm.LabTestTypes = types;
            treatmentForm.TreatmentType = TreatmentType.labTestType;
            Treatment t = treatmentController.CreateTreatment(TreatmentFactory.CreateTreatment(treatmentForm));
            ExaminationSurgery exam = ExaminationSurgeryController.GetCurrentExamination(Informations.currentRecord.MedicalRecord.IdRecord);
            ExaminationSurgeryController.UpdateTreatment(exam, t);


            NavigationService.Navigate(new Uri("Pages/MedicalRecord/Vaccines.xaml", UriKind.Relative));
        }

        private void CancelPrescription_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/MedicalRecord/Vaccines.xaml", UriKind.Relative));
        }

        private void Close_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/MedicalRecord/Vaccines.xaml", UriKind.Relative));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string str = (string) checkBox.Tag;
            LabTestType labTestType = new LabTestType(str);
            types.Remove(labTestType);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string str = (string)checkBox.Tag;
            LabTestType labTestType = new LabTestType(str);
            types.Add(labTestType);
        }
    }
}

