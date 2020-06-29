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
using System.Windows.Shapes;

namespace ZdravoKorporacija.Pages
{
    /// <summary>
    /// Interaction logic for Language.xaml
    /// </summary>
    public partial class Language : UserControl
    {
        public Language()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.CloseLanguage.RaiseMyCustomEvent(this, new EventArgs());
        }

        private void Serbian_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.CloseLanguage.RaiseMyCustomEvent(this, new EventArgs());
        }
    }
}
