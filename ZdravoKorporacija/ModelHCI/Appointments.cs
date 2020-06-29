using Model.Schedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class AppointmentHCI
    {
        public Patient patient;
        public Type type { get; set; }

        public Doctor doctor { get; set; }

        public int RoomNumber { get; set; }
        public DateTime Time { get; set; }
        public string Urgent { get; set; }
        public string timeString { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public DateTime Date { get; set; }
        public string HealthProblem { get; set; }
        public string Gender { get; set; }
        public int ID { get; set; }
        public Appointment appointment { get; set; }

        public AppointmentHCI() {}
        public AppointmentHCI(int i, Patient patient, Type type, int RoomNumber, DateTime Time, bool Urgent, string healthProblem)
        {
            this.patient = patient;
            this.type = type;
            this.RoomNumber = RoomNumber;
            this.Time = Time;
            this.Urgent = Urgent ? "DA" : "NE";
            this.timeString = this.Time.ToString("HH:mm");
            this.Date = this.Time.Date;
            this.FullName = patient.FullName;
            this.Age = patient.Age.ToString();
            this.HealthProblem = healthProblem;
            this.Gender = "Muški";
            this.ID = ++i;

        }

        public override bool Equals(Object obj)
        {
            if (obj is AppointmentHCI)
            {
                AppointmentHCI apt = (AppointmentHCI)obj;
                if (apt.ID == this.ID) 
                { 

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }

    public enum Type
    {
        Operacija,
        Pregled
    }


}
