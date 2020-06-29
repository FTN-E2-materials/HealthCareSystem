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

namespace ZdravoKorporacija.Pages.Schedules
{
    /// <summary>
    /// Interaction logic for Monthly.xaml
    /// </summary>
    /// 


    public partial class Monthly : UserControl
    {
        public static int previousMonth;
        public static int currentMonth = DateTime.Today.Month;
        public static int currentYear = 2020;
        public static DateTime selectedDay = DateTime.Today;
        public static DateTime dayForAdd = DateTime.Today.AddDays(-1);


        public class AppointmentShow
        {
            public string time { get; set; }
            public int day { get; set; }

            public string show { get; set; }
            public bool scheduled { get; set; }
            public ModelHCI.AppointmentHCI app { get; set; }

            public AppointmentShow() { }
        }
        public class Days
        {
            public Visibility visible { get; set; }
            public DateTime date { get; set; }
            public string name { get; set; }
            public FontWeight weight {get; set; }
            public SolidColorBrush color { get; set; }
        }

        
        public Monthly()
        {
            InitializeComponent();
            dayForAdd = DateTime.Today.AddDays(-1);
            if (currentMonth == 1)
            {
                previousMonth = 12;
            } else
            {
                previousMonth = currentMonth - 1;
            }
            SetDays();
            SetList();
        }

        public void SetList()
        {
            List<AppointmentShow> ListOfAppointments = new List<AppointmentShow>();
            if (selectedDay.Date.CompareTo(DateTime.Today) < 0)
            {
                Add_appointment.Visibility = Visibility.Collapsed;
            }
            foreach (ModelHCI.AppointmentHCI appointment in ModelHCI.AppointmentsData.FullList)
            {
                if (DateTime.Compare(appointment.Date, selectedDay.Date) == 0)
                {
                    ListOfAppointments.Add(new AppointmentShow() { app = appointment, day = appointment.Date.Day, scheduled = true, 
                        show = "Vreme: " + appointment.timeString +  " \t" + appointment.patient.FullName,
                        time = appointment.timeString});
                    
                }
            }
            AppointmentList.ItemsSource = new List<AppointmentShow>();
            AppointmentList.ItemsSource = ListOfAppointments;
            ChosenDate.Text = selectedDay.Date.ToString("dd.MM.yyyy.");

        }

        public void SetDays()
        { 
            DateTime firstDate = new DateTime(currentYear, currentMonth, 1);
            SetText();

            if ((int)firstDate.DayOfWeek == 1)
            {
                SetDaysFromMonday();
            }
            else if ((int)firstDate.DayOfWeek == 2)
            {
                SetDaysFromTuesday();
            }

            else if ((int)firstDate.DayOfWeek == 3)
            {
                SetDaysFromWednesday();
            }
            else if ((int)firstDate.DayOfWeek == 4)
            {
                SetDaysFromThrusday();
            }
            else if ((int)firstDate.DayOfWeek == 5)
            {
                SetDaysFromFriday();
            }
            else if ((int)firstDate.DayOfWeek == 6)
            {
                SetDaysFromSaturday();
            }
            else if ((int)firstDate.DayOfWeek == 0)
            {
                SetDaysFromSunday();
            }
        }
        private void SetDaysFromMonday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, date = firstDate.AddDays(-1), weight = FontWeights.Light });
            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                Days day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold };
                if ((int)firstDate.DayOfWeek == 0)
                {
                    day.color = new SolidColorBrush(Colors.Red);
                }
                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (41 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromTuesday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold });
            }
            int j = 0;
            for (i = 0; i < (40 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromWednesday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold });
            }
            int j = 0;
            for (i = 0; i < (39 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromThrusday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 3), name = (daysInPreviousMonth - 3).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth -1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold});
            }
            int j = 0;
            for (i = 0; i < (38 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromFriday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 4), name = (daysInPreviousMonth - 4).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 3), name = (daysInPreviousMonth - 3).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            int i = 0;
            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold});
            }
            int j = 0;
            for (i = 0; i < (37 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }
        private void SetDaysFromSaturday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 5), name = (daysInPreviousMonth - 5).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 4), name = (daysInPreviousMonth - 4).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 3), name = (daysInPreviousMonth - 3).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold });
            }
            int j = 0;
            for (i = 0; i < (36 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);

        }
        private void SetDaysFromSunday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;

            while (i < daysInMonth)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.SemiBold });
            }
            int j = 0;
            for (i = 0; i < (42 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            currentMonth++;
            
            if (currentMonth == 13)
            {
                currentMonth = 1;
                previousMonth = 12;
                currentYear++;
            } else if (currentMonth == 0)
            {
                currentMonth = 12;
                currentYear--;
            } else
            {
                previousMonth = currentMonth - 1;
            }


            
            
            Week1.ItemsSource = new List<Days>();
            Week2.ItemsSource = new List<Days>();
            Week3.ItemsSource = new List<Days>();
            Week4.ItemsSource = new List<Days>();
            Week5.ItemsSource = new List<Days>();
            Week6.ItemsSource = new List<Days>();
            SetDays();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            currentMonth--;
            if (currentMonth == 13)
            {
                currentMonth = 1;
                previousMonth = 12;
                currentYear++;
            } else if (currentMonth == 0)
            {
                currentMonth = 12;
                previousMonth = 11;
                currentYear--; 
            } else
            {
                if (currentMonth > 1)
                {
                    previousMonth--;
                } else
                {
                    previousMonth = 12;
                }
            }

            Week1.ItemsSource = new List<Days>();
            Week2.ItemsSource = new List<Days>();
            Week3.ItemsSource = new List<Days>();
            Week4.ItemsSource = new List<Days>();
            Week5.ItemsSource = new List<Days>();
            Week6.ItemsSource = new List<Days>();
            SetDays();
        }

        private void SetText()
        {
            string text = "";
            if (currentMonth == 1)
            {
                text = "Januar " + currentYear.ToString();
            }
            if (currentMonth == 2)
            {
                text = "Februar " + currentYear.ToString();
            }
            if (currentMonth == 3)
            {
                text = "Mart " + currentYear.ToString();
            }
            if (currentMonth == 4)
            {
                text = "April " + currentYear.ToString();
            }
            if (currentMonth == 5)
            {
                text = "Maj " + currentYear.ToString();
            }
            if (currentMonth == 6)
            {
                text = "Jun " + currentYear.ToString();
            }
            if (currentMonth == 7)
            {
                text = "Jul " + currentYear.ToString();
            }
            if (currentMonth == 8)
            {
                text = "Avgust " + currentYear.ToString();
            }
            if (currentMonth == 9)
            {
                text = "Septembar " + currentYear.ToString();
            }
            if (currentMonth == 10)
            {
                text = "Oktobar " + currentYear.ToString();
            }
            if (currentMonth == 11)
            {
                text = "Novembar " + currentYear.ToString();
            }
            if (currentMonth == 12)
            {
                text = "Decembar " + currentYear.ToString();
            }
            CurrentMonth.Text = text;
        }

        private void Week1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Week1.SelectedItem != null)
                {
                    if (Week1.SelectedItem is Days)
                    {
                        var row = (Days)Week1.SelectedItem;


                        if (row != null)
                        {
                            selectedDay = row.date;
                            SetList();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            
        }

        private void Week2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Week2.SelectedItem != null)
                {
                    if (Week2.SelectedItem is Days)
                    {
                        var row = (Days)Week2.SelectedItem;


                        if (row != null)
                        {
                            selectedDay = row.date;
                            SetList();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Week3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Week3.SelectedItem != null)
                {
                    if (Week3.SelectedItem is Days)
                    {
                        var row = (Days)Week3.SelectedItem;


                        if (row != null)
                        {
                            selectedDay = row.date;
                            SetList();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Week4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Week4.SelectedItem != null)
                {
                    if (Week4.SelectedItem is Days)
                    {
                        var row = (Days)Week4.SelectedItem;


                        if (row != null)
                        {
                            selectedDay = row.date;
                            SetList();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Week5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Week5.SelectedItem != null)
                {
                    if (Week5.SelectedItem is Days)
                    {
                        var row = (Days)Week5.SelectedItem;


                        if (row != null)
                        {
                            selectedDay = row.date;
                            SetList();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Week6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Week6.SelectedItem != null)
                {
                    if (Week6.SelectedItem is Days)
                    {
                        var row = (Days)Week6.SelectedItem;


                        if (row != null)
                        {
                            selectedDay = row.date;
                            SetList();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void AppointmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Add_appointment_Click(object sender, RoutedEventArgs e)
        {
            MyEvents.AddAppointmentEventHandler.RaiseMyCustomEvent(this, new MyEvents.SetDateEventHandler() { date = selectedDay });
        }
    }
}
