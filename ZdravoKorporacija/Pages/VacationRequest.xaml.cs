using Controller.UserController;
using Model.Users;
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
    /// Interaction logic for VacationRequest.xaml
    /// </summary>
    /// 

    public partial class VacayRequest : Page

    {
        public const string ERROR_START_DATE = "Datumi moraju biti nakon današnjeg!";
        public const string ERROR_END_DATE = "Krajnji datum mora biti nakon početnog!";
        public const string ERROR_SELECTION = "Morate popuniti sva polja označena zvezdicom!";

        public static DateTime startDate = DateTime.Today.AddDays(-1);
        public static DateTime endDate = DateTime.Today;

        public static VacationRequestController vacationRequestController;


        public VacayRequest()
        {
            var app = Application.Current as App;
            vacationRequestController = app.vacationRequestController;
            InitializeComponent();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDate != null)
            {
                if (StartDate.SelectedDate != null)
                {
                    DateTime selected = (DateTime)StartDate.SelectedDate;

                    if (selected.CompareTo(DateTime.Today) < 0)
                    {
                        Error.Text = ERROR_START_DATE;
                        ErrorMessage.Visibility = Visibility.Visible;
                        StartDate.SelectedDate = null;
                        return;
                    }

                    startDate = selected;

                    if (EndDate.SelectedDate != null)
                    {
                        if (startDate.CompareTo((DateTime)EndDate.SelectedDate) > 0)
                        {
                            Error.Text = ERROR_END_DATE;
                            ErrorMessage.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate != null)
            {
                if (EndDate.SelectedDate != null)
                {
                    DateTime selected = (DateTime)EndDate.SelectedDate;
                    if (selected.CompareTo(DateTime.Today) < 0)
                    {
                        Error.Text = ERROR_START_DATE;
                        ErrorMessage.Visibility = Visibility.Visible;
                        EndDate.SelectedDate = null;
                        return;
                    }

                    endDate = selected;
                    if (StartDate.SelectedDate != null)
                    {
                        if (selected.CompareTo((DateTime)StartDate.SelectedDate) < 0)
                        {
                            Error.Text = ERROR_END_DATE;
                            ErrorMessage.Visibility = Visibility.Visible;
                            EndDate.SelectedDate = null;
                            return;
                        }

                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((bool)Bolovanje.IsChecked || (bool)Odmor.IsChecked) &&  EndDate.SelectedDate != null && StartDate.SelectedDate != null)
            {
                ModelHCI.VacationRequestHCI request = new ModelHCI.VacationRequestHCI();

                VacationRequest vacationRequest = new VacationRequest();
                vacationRequest.FromDate = startDate;
                vacationRequest.ToDate = endDate;
                vacationRequest.ReasonForVacation = Napomena.Text;
                vacationRequest.Employee = (Employee)MainWindow.doctor;
                vacationRequest.Approved = false;

                vacationRequestController.SendVacationRequest(vacationRequest);
                NavigationService.Navigate(new Uri("/Pages/Schedule.xaml", UriKind.Relative));
            } else
            {
                ErrorMessage.Visibility = Visibility.Visible;
                Error.Text = ERROR_SELECTION;

            }
        }
    }
}
