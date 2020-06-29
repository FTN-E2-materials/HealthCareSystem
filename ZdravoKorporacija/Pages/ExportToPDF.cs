using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model.Reports;

namespace ZdravoKorporacija.Pages
{
    public class ExportToPDF
    {


        public static void ExportAsPDF(MedicationConsumptionReport report)
        {
            FlowDocument doc = new FlowDocument();

            doc.PagePadding = new System.Windows.Thickness(40);
            doc.PageWidth = 1240;
            doc.ColumnWidth = 1240;
            doc.PageHeight = 1754;
            doc.IsColumnWidthFlexible = false;
            doc.Background = Brushes.White;
            
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Image logo = new Image();
            logo.Source = new BitmapImage(new Uri(@"..\..\Resources\default.png", UriKind.RelativeOrAbsolute));
            logo.Height = 320;
            logo.Width = 400;
            TextBlock tb = new TextBlock();
            tb.Text = "Izveštaj o potrošnji lekova";
            tb.FontSize = 50;
            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.Margin = new System.Windows.Thickness(0, 70, 0, 0);
            tb.FontFamily = new FontFamily("Helvetica"); 

            stackPanel.Children.Add(logo);
            stackPanel.Children.Add(tb);

            Paragraph top = new Paragraph();
            top.Inlines.Add(stackPanel);

            doc.Blocks.Add(top);

            Paragraph datum = new Paragraph();
            datum.FontSize = 35;
            tb = new TextBlock();
            tb.FontFamily = new FontFamily("Helvetica");
            tb.Text = "Izveštaj za period od " + report.StartTime.ToString("dd.MM.yyyy.") + " do " + report.EndTime.ToString("dd.MM.yyyy.");
            TextBlock textBlock = new TextBlock();

            datum.Inlines.Add(tb);
            datum.Inlines.Add(textBlock);

            doc.Blocks.Add(datum);

            Table table = new Table();
            table.FontSize = 20;
            table.FontFamily = new FontFamily("Helvetica");

            TableColumn kolona1 = new TableColumn();
            TableColumn kolona2 = new TableColumn();
            TableColumn kolona3 = new TableColumn();
            kolona1.Width = new System.Windows.GridLength(100);
            kolona2.Width = new System.Windows.GridLength(500);
            kolona3.Width = new System.Windows.GridLength(640);
            table.Columns.Add(kolona1);
            table.Columns.Add(kolona2);
            table.Columns.Add(kolona3);
            TableRowGroup tableRowGroup = new TableRowGroup();


            foreach (var p in report.Prescriptions)
            {
                foreach (var v in p.Medications)
                {

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
                    tbId.Text = "ID leka: ";
                    TextBlock tbbId = new TextBlock();
                    tbbId.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                    tbbId.Text = v.MedId.ToString();

                    idPanel.Children.Add(tbId);
                    idPanel.Children.Add(tbbId);

                    //ime
                    StackPanel imePanel = new StackPanel();
                    imePanel.Orientation = Orientation.Horizontal;

                    TextBlock tbIme = new TextBlock();
                    tbIme.Text = "Ime: ";
                    TextBlock tbbIme = new TextBlock();
                    tbbIme.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                    tbbIme.Text = v.Med;

                    imePanel.Children.Add(tbIme);
                    imePanel.Children.Add(tbbIme);

                    //prezime
                    StackPanel prezimePanel = new StackPanel();
                    prezimePanel.Orientation = Orientation.Horizontal;

                    TextBlock tbPrezime = new TextBlock();
                    tbPrezime.Text = "Proizvođač: ";
                    TextBlock tbbPrezime = new TextBlock();
                    tbbPrezime.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                    tbbPrezime.Text = v.Company;

                    prezimePanel.Children.Add(tbPrezime);
                    prezimePanel.Children.Add(tbbPrezime);

                    //radno mesto
                    StackPanel radnoMestoPanel = new StackPanel();
                    radnoMestoPanel.Orientation = Orientation.Horizontal;

                    TextBlock tbRadnoMesto = new TextBlock();
                    tbRadnoMesto.Text = "Kategorija: ";
                    TextBlock tbbRadnoMesto = new TextBlock();
                    tbbRadnoMesto.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                    string temp = "";
                    foreach (var cat in v.MedicationCategory)
                    {
                        if (!temp.Equals(""))
                            temp += ", ";
                        temp += cat.CategoryName;
                    }
                    tbbRadnoMesto.Text = temp;

                    radnoMestoPanel.Children.Add(tbRadnoMesto);
                    radnoMestoPanel.Children.Add(tbbRadnoMesto);

                    StackPanel brojPrepisivanja = new StackPanel();
                    brojPrepisivanja.Orientation = Orientation.Horizontal;


                    TextBlock brPrepisivanja = new TextBlock();
                    brPrepisivanja.Text = "Broj prepisivanja: ";
                    TextBlock broj = new TextBlock();
                    broj.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                    broj.Text = report.Prescriptions.Count.ToString();

                    brojPrepisivanja.Children.Add(brPrepisivanja);
                    brojPrepisivanja.Children.Add(broj);


                    StackPanel mergeInfo = new StackPanel();
                    mergeInfo.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    mergeInfo.Margin = new System.Windows.Thickness(50, 0, 0, 0);

                    mergeInfo.Children.Add(idPanel);
                    mergeInfo.Children.Add(imePanel);
                    mergeInfo.Children.Add(prezimePanel);
                    mergeInfo.Children.Add(radnoMestoPanel);
                    mergeInfo.Children.Add(brojPrepisivanja);

                    Paragraph medInfo = new Paragraph();
                    medInfo.TextAlignment = System.Windows.TextAlignment.Left;
                    medInfo.Inlines.Add(mergeInfo);

                    TableCell celijaInfo = new TableCell(medInfo);
                    celijaInfo.ColumnSpan = 1;

                    celijaInfo.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
                    celijaInfo.BorderBrush = Brushes.LightGray;


                    r.Cells.Add(celijaInfo);

                    //Ukupno radno vreme
                    StackPanel urvPanel = new StackPanel();
                    urvPanel.Orientation = Orientation.Vertical;


                    TextBlock tbUrv = new TextBlock();
                    tbUrv.Text = "Sastojci ";

                    StackPanel tbbUrv = new StackPanel();
                    tbbUrv.Width = 200;
                    tbbUrv.Orientation = Orientation.Vertical;

                    foreach (var ing in v.MedicationContent)
                    {
                        TextBlock ingr = new TextBlock();
                        ingr.Text = ing.MedicationIngredient.Name + " " + ing.Amount + "mg";
                        ingr.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                        tbbUrv.Children.Add(ingr);
                    }


                    urvPanel.Children.Add(tbUrv);
                    urvPanel.Children.Add(tbbUrv);

                    //Zauzeto radno vreme
                    StackPanel zrvPanel = new StackPanel();
                    zrvPanel.Orientation = Orientation.Vertical;

                    TextBlock tbZrv = new TextBlock();
                    tbZrv.Text = "Alergeni: ";

                    StackPanel tbbZrv = new StackPanel();
                    tbbZrv.Width = 200;
                    tbbZrv.Orientation = Orientation.Vertical;

                    foreach (var ing in v.Allergens)
                    {
                        TextBlock ingr = new TextBlock();
                        ingr.Text = ing.Allergen;
                        ingr.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                        tbbZrv.Children.Add(ingr);
                    }


                    TextBlock risk = new TextBlock();
                    risk.Text = "Faktori rizika: ";

                    StackPanel riskSt = new StackPanel();
                    riskSt.Width = 200;
                    riskSt.Orientation = Orientation.Vertical;

                    foreach (var ing in v.SideEffects)
                    {
                        TextBlock ingr = new TextBlock();
                        ingr.Text = ing.SideEffects.Name;
                        ingr.Foreground = (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
                        riskSt.Children.Add(ingr);
                    }


                    StackPanel aller = new StackPanel();
                    aller.Orientation = Orientation.Vertical;
                    aller.Children.Add(tbZrv);
                    aller.Children.Add(tbbZrv);

                    StackPanel riskFac = new StackPanel();
                    riskFac.Orientation = Orientation.Vertical;
                    riskFac.Children.Add(risk);
                    riskFac.Children.Add(riskSt);


                    StackPanel mergeStats = new StackPanel();
                    mergeStats.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    mergeStats.Margin = new System.Windows.Thickness(100, 0, 0, 0);

                    mergeStats.Children.Add(urvPanel);
                    mergeStats.Children.Add(aller);
                    mergeStats.Children.Add(riskFac);


                    Paragraph statsInfo = new Paragraph();
                    statsInfo.Inlines.Add(mergeStats);

                    TableCell celijaStats = new TableCell(statsInfo);
                    celijaStats.ColumnSpan = 1;


                    celijaStats.BorderThickness = new System.Windows.Thickness(0, 0, 0, 1);
                    celijaStats.BorderBrush = Brushes.LightGray;


                    r.Cells.Add(celijaStats);

                    tableRowGroup.Rows.Add(r);
                }
            }
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
