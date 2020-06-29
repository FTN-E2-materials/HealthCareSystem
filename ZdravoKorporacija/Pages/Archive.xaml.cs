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
    /// Interaction logic for Archive.xaml
    /// </summary>
    /// 

    public class showPrescription
    {
        public string show { get; set; }
        public ModelHCI.PrescriptionHCI prescription { get; set; }
    }

    public class showReport
    {
        public string show { get; set; }
        public ModelHCI.Report report { get; set; }
    }
    public class showValidation
    {
        public string show { get; set; }
        public ModelHCI.ValidationMed valid { get; set; }
    }

    public class showExaminations
    {
        public string show { get; set; }
        public ModelHCI.ExaminationSurgeryHCI exam { get; set; }
    }
    public partial class Archive : Page
    {
      
        public Archive()
        {
            InitializeComponent();
            List<showPrescription> recepti = new List<showPrescription>();
            List<showReport> izvestaji = new List<showReport>();
            List<showValidation> validacije = new List<showValidation>();
            ModelHCI.PrescriptionsForReports pr = new ModelHCI.PrescriptionsForReports();
            List<showExaminations> exams = new List<showExaminations>();
            pr.addPrescriptions();
            foreach (ModelHCI.PrescriptionHCI p in pr.allPrescriptions)
            {
                recepti.Add(new showPrescription() { show = "Recept: " + p.id + " Dana: " + p.dateOfPrescription.Date.ToString("dd.MM.yyyy"), prescription = p });
            }
            foreach (ModelHCI.Report p in ModelHCI.PrescriptionsForReports.reports)
            {
                izvestaji.Add(new showReport() { show = "Izveštaj za period " + p.dateFrom.Date.ToString("dd.MM.yyyy") +  "-" + p.dateTo.ToString("dd.MM.yyyy") , report = p });
            }

            foreach (ModelHCI.ValidationMed p in ModelHCI.ReportData.validations)
            {
                validacije.Add(new showValidation() { show = "Validacija leka: " + p.medThatIsValidated.name + " Dana: " + p.dateOfValidation.ToString("dd.MM.yyyy"), valid = p });
            }

            foreach (ModelHCI.ExaminationSurgeryHCI e in ModelHCI.ReportData.examinationReports)
            {
                exams.Add(new showExaminations() { show = "Pregled pacijenta " + e.patient.FullName + " dana " + e.date.Date.ToString("dd.MM.yyyy.") });
            }


            Recepti.ItemsSource = new List<showPrescription>();
            Recepti.ItemsSource = recepti;
            Izvestaji.ItemsSource = new List<showReport>();
            Izvestaji.ItemsSource = izvestaji;

            ValidacijeList.ItemsSource = new List<showValidation>();
            ValidacijeList.ItemsSource = validacije;

            Pregledi.ItemsSource = new List<showExaminations>();
            Pregledi.ItemsSource = exams;


        }

        private void Recepti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Recepti.SelectedItem != null)
            {
                ModelHCI.PrescriptionHCI p = ((showPrescription)Recepti.SelectedItem).prescription;
                ExportPrescrption.ExportAsPdf(p);
            }
        }

        private void Izvestaji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Izvestaji.SelectedItem != null)
            {
                ModelHCI.Report p = ((showReport)Izvestaji.SelectedItem).report;
               // ExportToPDF.ExportAsPDF(p);
            }
        }

        private void ValidacijeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ValidacijeList.SelectedItem != null)
            {
                ModelHCI.ValidationMed p = ((showValidation)ValidacijeList.SelectedItem).valid;
                ExportValidation.ExportAsPDFValidation(p);
            }
        }

        private void Pregledi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Pregledi.SelectedItem != null)
            {
                ModelHCI.ExaminationSurgeryHCI p = ((showExaminations)Pregledi.SelectedItem).exam;
               // ExportToPDFExamination.ExportAsPDF(p);
            }
        }
    }
}
