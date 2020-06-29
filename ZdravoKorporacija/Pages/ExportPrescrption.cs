using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ZdravoKorporacija.Pages
{
    class ExportPrescrption
    {
        public static void ExportAsPdf(ModelHCI.PrescriptionHCI report)
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
            tb.Text = "Recept";
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
            tb.FontFamily = new FontFamily("Helvetica");
            tb.Text = "Datum izdavanja recepta: " + report.dateOfPrescription.ToString("dd.MM.yyyy.");

            TextBlock pacijent = new TextBlock();
            pacijent.FontFamily = new FontFamily("Helvetica");
            pacijent.Text = "Pacijent: " + report.patient.FullName;


            datum.Inlines.Add(tb);
            datum.Inlines.Add(pacijent);

          
            doc.Blocks.Add(datum);

            Table table = new Table();
            table.FontSize = 15;
            table.FontFamily = new FontFamily("Helvetica");

            TableColumn kolona1 = new TableColumn();
            TableColumn kolona2 = new TableColumn();
            TableColumn kolona3 = new TableColumn();
            kolona1.Width = new System.Windows.GridLength(50);
            kolona2.Width = new System.Windows.GridLength(450);
            kolona3.Width = new System.Windows.GridLength(320);
            table.Columns.Add(kolona1);
            table.Columns.Add(kolona2);
            table.Columns.Add(kolona3);
            TableRowGroup tableRowGroup = new TableRowGroup();

            //
            TableRow r = new TableRow();

            Paragraph slikaParagraf = new Paragraph();
            TableCell slikaCelija = new TableCell(slikaParagraf);
            slikaCelija.ColumnSpan = 1;
            slikaCelija.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
            r.Cells.Add(slikaCelija);

            StackPanel idPanel = new StackPanel();
            idPanel.Orientation = Orientation.Horizontal;

            //id
            TextBlock tbId = new TextBlock();
            tbId.Text = "ID recepta: ";
            TextBlock tbbId = new TextBlock();
            tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbId.Text = report.id.ToString();

            idPanel.Children.Add(tbId);
            idPanel.Children.Add(tbbId);

            //ime
            StackPanel imePanel = new StackPanel();
            imePanel.Orientation = Orientation.Horizontal;

            TextBlock tbIme = new TextBlock();
            tbIme.Text = "Naziv leka: ";
            TextBlock tbbIme = new TextBlock();
            tbbIme.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbIme.Text = report.medication.name;

            imePanel.Children.Add(tbIme);
            imePanel.Children.Add(tbbIme);

            //prezime
            StackPanel prezimePanel = new StackPanel();
            prezimePanel.Orientation = Orientation.Horizontal;

            TextBlock tbPrezime = new TextBlock();
            tbPrezime.Text = "Proizvođač: ";
            TextBlock tbbPrezime = new TextBlock();
            tbbPrezime.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbPrezime.Text = report.medication.Manufactuer;

            prezimePanel.Children.Add(tbPrezime);
            prezimePanel.Children.Add(tbbPrezime);

            //radno mesto
            StackPanel radnoMestoPanel = new StackPanel();
            radnoMestoPanel.Orientation = Orientation.Horizontal;

            TextBlock tbRadnoMesto = new TextBlock();
            tbRadnoMesto.Text = "Format: ";
            TextBlock tbbRadnoMesto = new TextBlock();
            tbbRadnoMesto.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            tbbRadnoMesto.Text = report.medication.format;

            radnoMestoPanel.Children.Add(tbRadnoMesto);
            radnoMestoPanel.Children.Add(tbbRadnoMesto);

            StackPanel brojPrepisivanja = new StackPanel();
            brojPrepisivanja.Orientation = Orientation.Horizontal;


            TextBlock brPrepisivanja = new TextBlock();
            brPrepisivanja.Text = "Rezervisan: ";
            TextBlock broj = new TextBlock();
            broj.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            broj.Text = report.reserved ? "DA" : "NE";


            brojPrepisivanja.Children.Add(brPrepisivanja);
            brojPrepisivanja.Children.Add(broj);



            StackPanel brUz = new StackPanel();
            brUz.Orientation = Orientation.Horizontal;

            if (report.reserved)
            {
                TextBlock brojUzetih = new TextBlock();
                brojUzetih.Text = "Rezervisan u periodu: ";
                TextBlock br = new TextBlock();
                br.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                br.Text = report.dateOfPrescription.ToString("dd.MM.yyyy") + "-" + report.dateOfPrescription.AddDays(10).ToString("dd.MM.yyyy");
                brUz.Children.Add(brojUzetih);
                brUz.Children.Add(br);
            }



            //Ukupno radno vreme
            StackPanel urvPanel = new StackPanel();
            urvPanel.Orientation = Orientation.Vertical;



            TextBlock tbUrv = new TextBlock();
            tbUrv.Text = "Napomene: ";


            TextBlock ingr = new TextBlock();
            ingr.Text = report.reasonWhy;
            ingr.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");

            urvPanel.Children.Add(tbUrv);
            urvPanel.Children.Add(ingr);


            StackPanel mergeInfo = new StackPanel();
            mergeInfo.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            mergeInfo.Margin = new System.Windows.Thickness(50, 0, 0, 0);


            StackPanel zrvPanel = new StackPanel();
            zrvPanel.Orientation = Orientation.Horizontal;

            TextBlock tbZrv = new TextBlock();
            tbZrv.Text = "Način korišćenja: ";

            TextBlock intake = new TextBlock();
            intake.Text = (report.intake == 0) ? "po potrebi" : "na " + report.intake.ToString() + " h";
            intake.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");

            zrvPanel.Children.Add(tbZrv);
            zrvPanel.Children.Add(intake);



            mergeInfo.Children.Add(idPanel);
            mergeInfo.Children.Add(imePanel);
            mergeInfo.Children.Add(prezimePanel);
            mergeInfo.Children.Add(radnoMestoPanel);
            mergeInfo.Children.Add(brojPrepisivanja);

            if (report.reserved)
            {
                mergeInfo.Children.Add(brUz);
            }
            mergeInfo.Children.Add(zrvPanel);
            mergeInfo.Children.Add(urvPanel);

            Paragraph medInfo = new Paragraph();
            medInfo.TextAlignment = System.Windows.TextAlignment.Left;
            medInfo.Inlines.Add(mergeInfo);




            TableCell celijaInfo = new TableCell(medInfo);
            celijaInfo.ColumnSpan = 1;

            celijaInfo.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
            celijaInfo.BorderBrush = Brushes.LightGray;

           
            r.Cells.Add(celijaInfo);

            tableRowGroup.Rows.Add(r);
            


            table.RowGroups.Add(tableRowGroup);

            doc.Blocks.Add(table);



            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "");
            }

        }
      }
    }
   