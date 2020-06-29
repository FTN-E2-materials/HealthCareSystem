using Model.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ZdravoKorporacija.ModelHCI
{
    public class Patient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public string FullName { get; set; }
        public MedicalRecordHCI record { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime dateOfBirth { get; set; }
        private Random rnd = new Random();
        public string ID_NUMBER { get; set; }
        public ImageSource Portrait { get; set; }
        public string LastAppointment { get; set; }
        public string LastDiagnosis { get; set; }
        public string GeneralState { get; set; }
        public string HospitalTreatment { get; set; }
        public Patient() { }
        public Patient(string Name, string Surname, string Age)

        {
            this.Name = Name;
            this.Surname = Surname;
            this.Age = Age;
            this.FullName = Name + " " + Surname;
           
            City random = CityData.cities[rnd.Next(1, 3)];
            this.City = random.CityName;
            this.State = random.State;
            this.dateOfBirth = DateTime.ParseExact("01.01.1970.", "dd.MM.yyyy.", null);
            this.ID_NUMBER = "12345023532";
            this.GeneralState = "OK";
            this.HospitalTreatment = "NE";

 
        }

    }
}
