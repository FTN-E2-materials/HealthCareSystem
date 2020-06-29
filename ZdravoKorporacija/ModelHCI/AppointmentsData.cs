using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ZdravoKorporacija.ModelHCI
{
    public class AppointmentsData
    {
        public static List<AppointmentHCI> FullList = new List<AppointmentHCI>();

        public AppointmentsData()
        {

            String randomText = "Pacijent se žali na bolove u predelu pluća. Prethodne dijagnoze su pokazale da pacijent pati od aritmije.\nPoslednja prepisana terapija jeste Propranolol 5mg.\n";
            AppointmentHCI appointment1 = new AppointmentHCI(1, PatientsData.patientsList[0], Type.Pregled, 3, DateTime.ParseExact("01.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment1.doctor = MainWindow.loggedInDoctor;
            randomText = "Pacijent se žali na bolove u predelu bubrega. Poslednja dijagnoza: nefrolithiasis.\nPoslednja terapija: Cistyclean, Urobitrat\nPrethodni dolasci nisu pokazali napredak u vidu smanjenja veličine kamena u bubregu. Potrebno smišljanje nove terapije.\n";
            AppointmentHCI appointment2 = new AppointmentHCI(2, PatientsData.patientsList[1], Type.Operacija, 3, DateTime.ParseExact("05.05.2020. 10:00", "dd.MM.yyyy. HH:mm", null), true, randomText);
            appointment2.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment3 = new AppointmentHCI(3, PatientsData.patientsList[2], Type.Pregled, 3, DateTime.ParseExact("01.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment3.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment4 = new AppointmentHCI(4, PatientsData.patientsList[3], Type.Pregled, 3, DateTime.ParseExact("01.05.2020. 10:00", "dd.MM.yyyy. HH:mm", null), true, randomText);
            appointment4.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment5 = new AppointmentHCI(5, PatientsData.patientsList[0], Type.Operacija, 3, DateTime.ParseExact("01.05.2020. 11:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment5.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment6 = new AppointmentHCI(6, PatientsData.patientsList[1], Type.Pregled, 3, DateTime.ParseExact("02.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment6.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment7 = new AppointmentHCI(7, PatientsData.patientsList[2], Type.Pregled, 3, DateTime.ParseExact("03.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment7.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment8 = new AppointmentHCI(8, PatientsData.patientsList[3], Type.Operacija, 3, DateTime.ParseExact("04.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment8.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment9 = new AppointmentHCI(9, PatientsData.patientsList[0], Type.Pregled, 3, DateTime.ParseExact("01.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment9.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment10 = new AppointmentHCI(10, PatientsData.patientsList[1], Type.Pregled, 3, DateTime.ParseExact("01.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment10.doctor = MainWindow.loggedInDoctor;
            AppointmentHCI appointment11 = new AppointmentHCI(11, PatientsData.patientsList[2], Type.Pregled, 3, DateTime.ParseExact("01.05.2020. 09:00", "dd.MM.yyyy. HH:mm", null), false, randomText);
            appointment11.doctor = MainWindow.loggedInDoctor;
            FullList.Add(appointment1);
            FullList.Add(appointment2);
            FullList.Add(appointment3);
            FullList.Add(appointment4);
            FullList.Add(appointment5);
            FullList.Add(appointment6);
            FullList.Add(appointment7);
            FullList.Add(appointment8);
            FullList.Add(appointment9);
            FullList.Add(appointment10);
            FullList.Add(appointment11);


        }
    }
}
