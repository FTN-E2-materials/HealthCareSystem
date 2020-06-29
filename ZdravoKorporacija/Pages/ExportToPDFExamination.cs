using Model.ExaminationSurgery;
using Model.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZdravoKorporacija.Pages
{
    class ExportToPDFExamination
    {

        public static void ExportAsPDF(DoctorsAppointmentReport report)
        {

            ExaminationSurgery examinationSurgery = report.ExaminationSurgery;

            FlowDocument doc = new FlowDocument();

            doc.PagePadding = new System.Windows.Thickness(40);
            doc.PageWidth = 700;
            doc.ColumnWidth = 700;
            doc.PageHeight = 900;

            doc.Background = Brushes.White;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Image logo = new Image();
            logo.Source = new BitmapImage(new Uri(@"..\..\Resources\HospitalLogo.png", UriKind.RelativeOrAbsolute));
            logo.Width = 300;
            logo.Height = 300;
            TextBlock tb = new TextBlock();
            tb.Text = "Izveštaj pregleda";
            tb.FontSize = 25;

            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.Margin = new System.Windows.Thickness(0, 0, 0, 0);
            tb.FontFamily = new FontFamily("Helvetica");

            stackPanel.Children.Add(logo);
            stackPanel.Children.Add(tb);

            Paragraph top = new Paragraph();
            top.Inlines.Add(stackPanel);

            top.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
            doc.Blocks.Add(top);

            Paragraph datum = new Paragraph();
            datum.FontSize = 20;
            tb = new TextBlock();
            Paragraph pat = new Paragraph();
            pat.FontSize = 20;
            Paragraph valid = new Paragraph();
            valid.FontSize = 20;
            tb.FontFamily = new FontFamily("Helvetica");
            tb.Text = "Datum pregleda: " + report.Date.Date.ToString("dd.MM.yyyy.");

            TextBlock pacijent = new TextBlock();
            pacijent.FontFamily = new FontFamily("Helvetica");
            pacijent.Text = "Pacijent: " + examinationSurgery.MedicalRecord.Patient.Name + " " + examinationSurgery.MedicalRecord.Patient.Name;

            TextBlock lekar = new TextBlock();
            lekar.FontFamily = new FontFamily("Helvetica");
            lekar.Text = "Pregled izvršen od strane: " + MainWindow.doctor.Name + " " + MainWindow.doctor.Surname;


            datum.Inlines.Add(tb);
            pat.Inlines.Add(pacijent);
            valid.Inlines.Add(lekar);


            doc.Blocks.Add(datum);
            doc.Blocks.Add(pat);
            doc.Blocks.Add(valid);



            StackPanel safe = new StackPanel();
            safe.Orientation = Orientation.Horizontal;
            safe.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            //id
            TextBlock tbId = new TextBlock();
            tbId.Text = "Postavljena dijagnoza";
            TextBlock tbbId = new TextBlock();
            tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbId.Text = examinationSurgery.Diagnoses.Count > 0 ? "DA" : "NE";

            StackPanel sast = new StackPanel();
            sast.Orientation = Orientation.Vertical;
            TextBlock sastojcileka = new TextBlock();
            sastojcileka.Text = "Dijagnoze: ";

            StackPanel sastojci = new StackPanel();
            sastojci.Orientation = Orientation.Vertical;
            foreach (var m in examinationSurgery.Diagnoses)
            {
                TextBlock sastojak = new TextBlock();
                sastojak.Text = m.Name;
                sastojak.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                sastojak.FontFamily = new FontFamily("Helvetica");
                sastojci.Children.Add(sastojak); ;
            }


            tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");

            TextBlock treatments = new TextBlock();
            treatments.Text = "Nova lečenja: ";
            TextBlock lecenja = new TextBlock();
            lecenja.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbId.Text = examinationSurgery.Treatments.Count > 0 ? "DA" : "NE";

            StackPanel aler = new StackPanel();
            aler.Orientation = Orientation.Vertical;
            TextBlock alerg = new TextBlock();
            alerg.Text = "Lečenja: ";
            StackPanel alergije = new StackPanel();
            alergije.Orientation = Orientation.Vertical;
            foreach (var m in examinationSurgery.Treatments)
            {
                TextBlock alergija = new TextBlock();

                if ( m.Type == TreatmentType.prescription)
                {
                    var p = (Prescription)m;
                    string str = "";
                    foreach  (var med in p.Medications)
                    {
                        if (!str.Equals(""))
                            str += ", ";
                        str += med.Med;
                    }
                    alergija.Text = "Prepisani lekovi " + str;
                }


                if (m.Type == TreatmentType.hospitalTreatment)
                {
                    var hosp = (HospitalTreatment)m;
                    alergija.Text = "Prepisano lečenje od " + hosp.StartDate.Date.ToString("dd.MM.yyyy.") + " do " + hosp.EndDate.Date.ToString("dd.MM.yyyy.");
                }

                alergija.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                alergija.FontFamily = new FontFamily("Helvetica");
                alergije.Children.Add(alergija); ;
            }


            safe.Children.Add(tbId);
            safe.Children.Add(tbbId);

            sast.Children.Add(sastojcileka);
            sast.Children.Add(sastojci);

            aler.Children.Add(alerg);
            aler.Children.Add(alergije);

            Paragraph inf = new Paragraph();
            inf.FontSize = 15;
            Paragraph inf2 = new Paragraph();
            inf2.FontSize = 15;
            Paragraph inf3 = new Paragraph();
            inf3.FontSize = 15;

            inf.Inlines.Add(safe);
            inf2.Inlines.Add(sast);
            inf3.Inlines.Add(aler);
            doc.Blocks.Add(inf);
            doc.Blocks.Add(inf2);
            doc.Blocks.Add(inf3);




            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "");
            }

        }
    }
}
