using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Model.Users;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ModelHCI.Doctor loggedInDoctor = null;
        public static Doctor doctor = null;
        public MainWindow()
        {
            if (loggedInDoctor == null)
            {
                ModelHCI.MedicationData meds = new ModelHCI.MedicationData();
                ModelHCI.CityData cities = new ModelHCI.CityData();
                ModelHCI.PatientsData patients = new ModelHCI.PatientsData();


                ModelHCI.DoctorData doctors = new ModelHCI.DoctorData();
                ModelHCI.QuestionData questions = new ModelHCI.QuestionData();
                ModelHCI.ArticleData articles = new ModelHCI.ArticleData();
            }


            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            
        }

    }
}
