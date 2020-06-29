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
    /// Interaction logic for DemoUserControl.xaml
    /// </summary>
    public partial class DemoUserControl : UserControl
    {
        public DemoUserControl()
        {
            MyEvents.DemoEvents.ChangeText.CustomEvent += changeText;
            InitializeComponent();
        }

        private void changeText(object sender, EventArgs e)
        {
            if (e is MyEvents.DemoEvents.ChangingPatients)
            {
                var text = (MyEvents.DemoEvents.ChangingPatients)e;
                Textic.Text = text.text;
            }
        }
    }
}
