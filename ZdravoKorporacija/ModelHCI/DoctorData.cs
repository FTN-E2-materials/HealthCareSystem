using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class DoctorData
    {
        public static List<ModelHCI.Doctor> allDoctors = new List<ModelHCI.Doctor>();

        public DoctorData()
        {
            allDoctors.Add(new Doctor("Gregory", "House", "drhouse", "drhouse123", "1234567", new Uri("/Resources/drhouse.jpg", UriKind.Relative), "Opšta hirurgija"));
            allDoctors.Add(new Doctor("Derek", "Shepard", "mcdreamy", "mcdreamy123", "9876543", new Uri("/Resources/drhouse.jpg", UriKind.Relative), "Neurologija"));
            allDoctors.Add(new Doctor("Meredith", "Gray", "merder", "merder123", "456123", new Uri("/Resources/drhouse.jpg", UriKind.Relative), "Opšta hirurgija"));
            allDoctors.Add(new Doctor("Sheldon", "Cooper", "drcooper", "drcooper123", "654987", new Uri("/Resources/drhouse.jpg", UriKind.Relative), "Kardiologija"));

        }
    }
}
