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
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : UserControl
    {
        public static bool isOkay = false;
        public MessageBox()
        {
            InitializeComponent();
            
            isOkay = false;
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            isOkay = false;
            MyEvents.CloseMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {   
            isOkay = true;
            MyEvents.CloseMessageBoxEventHandler.RaiseMyCustomEvent(this, new MyEvents.CloseMessageBox());
        }
    }
}
