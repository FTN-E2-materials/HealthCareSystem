using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZdravoKorporacija.Pages
{
    class ExportValidation
    {
        public static void ExportAsPDFValidation(ModelHCI.ValidationMed report)
        {

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
            tb.Text = "Validacija leka";
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

            Paragraph valid = new Paragraph();
            valid.FontSize = 20;
            tb.FontFamily = new FontFamily("Helvetica");
            tb.Text = "Datum validacije: " + report.dateOfValidation.ToString("dd.MM.yyyy.");

            TextBlock lekar = new TextBlock();
            lekar.FontFamily = new FontFamily("Helvetica");
            lekar.Text = "Validacija izvršena od strane: " + report.doctorWhoValidated.name + " " + report.doctorWhoValidated.surname;


            datum.Inlines.Add(tb);
            valid.Inlines.Add(lekar);


            doc.Blocks.Add(datum);
            doc.Blocks.Add(valid);



            StackPanel safe = new StackPanel();
            safe.Orientation = Orientation.Horizontal;
            safe.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            //id
            TextBlock tbId = new TextBlock();
            tbId.Text = "SIGURAN ZA UPOTREBU: ";
            TextBlock tbbId = new TextBlock();
            tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbId.Text = report.safe ? "DA" : "NE";

            StackPanel sast = new StackPanel();
            sast.Orientation = Orientation.Vertical;
            TextBlock sastojcileka = new TextBlock();
            sastojcileka.Text = "Sastojci: ";
            
            StackPanel sastojci = new StackPanel();
            sastojci.Orientation = Orientation.Vertical;
            foreach (String m in report.medThatIsValidated.ingredients)
            {
                TextBlock sastojak = new TextBlock();
                sastojak.Text = m;
                sastojak.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                sastojak.FontFamily = new FontFamily("Helvetica");
                sastojci.Children.Add(sastojak); ;
            }


            tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbId.Text = report.safe ? "DA" : "NE";

            tbbId.Text = report.safe ? "DA" : "NE";

            StackPanel aler = new StackPanel();
            aler.Orientation = Orientation.Vertical;
            TextBlock alerg = new TextBlock();
            alerg.Text = "Alergeni: ";
            TextBlock alergija = new TextBlock();
            StackPanel alergije = new StackPanel();
            alergije.Orientation = Orientation.Vertical;
            foreach (String m in report.medThatIsValidated.allergens)
            {
                alergija.Text = m;
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

            Paragraph razlozi = new Paragraph();
            razlozi.FontSize = 15;

            TextBlock txt = new TextBlock();
            txt.Text = "Dodatne napomene: ";
            TextBlock napomene = new TextBlock();
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            panel.Children.Add(txt);
            panel.Children.Add(napomene);

            razlozi.Inlines.Add(panel);

            doc.Blocks.Add(razlozi);
            



            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "");
            }

        }
    }
}
    

