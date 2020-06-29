using Model.Medications;
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

namespace ZdravoKorporacija.Pages.Meds
{
    /// <summary>
    /// Interaction logic for StorageMeds.xaml
    /// </summary>
    public partial class StorageMeds : Page
    {
        public StorageMeds()
        {
            MyEvents.RefreshMedEventHandelr.CustomEvent += RefreshView;
            InitializeComponent();
            Allergens.ItemsSource = new List<String>();
            Ingredients.ItemsSource = new List<String>();
            Allergens.ItemsSource = Medications.SelectedRowStorage.allergens;
            Ingredients.ItemsSource = Medications.SelectedRowStorage.ingredients;
            Format.Text = Medications.SelectedRowStorage.format;
            Manufactuer.Text = Medications.SelectedRowStorage.Manufactuer;
            Name.Text = Medications.SelectedRowStorage.name;

            List<string> sideEffects = new List<string>();
            string koliko = "";
            foreach (var sideffect in Medications.SelectedRowStorage.medication.SideEffects)
            {
                if (sideffect.Frequency == SideEffectFrequency.common)
                {
                    koliko = "često";
                }
                else if (sideffect.Frequency == SideEffectFrequency.rare)
                {
                    koliko = "retko";
                }
                else if (sideffect.Frequency == SideEffectFrequency.veryCommon)
                {
                    koliko = "veoma često";
                }
                sideEffects.Add(sideffect.SideEffects.Name + " " + koliko);
            }
            RiskFactors.ItemsSource = sideEffects;
        }

        private void RefreshView(object sender, EventArgs e)
        {
            if (this != null)
            {
                Allergens.ItemsSource = new List<String>();
                Ingredients.ItemsSource = new List<String>();
                Allergens.ItemsSource = Medications.SelectedRowStorage.allergens;
                Ingredients.ItemsSource = Medications.SelectedRowStorage.ingredients;
                Format.Text = Medications.SelectedRowStorage.format;
                Manufactuer.Text = Medications.SelectedRowStorage.Manufactuer;
                Name.Text = Medications.SelectedRowStorage.name;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.OpenRecord.RaiseMyCustomEvent(this, new MyEvents.CloseAddArticle());
            NavigationService.Navigate(new Uri("/Pages/Medications.xaml", UriKind.Relative));

        }
    }
}
