using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZdravoKorporacija.ModelHCI
{
    public class Doctor
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string licenseNumber { get; set; }
        public ImageSource Portrait { get; set; }
        public string specialization { get; set; }

        public Doctor(string name, string surname, string username, string password, string licenseNumber, Uri uri, string specialization)
        {
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.specialization = specialization;
            this.password = password;
            this.licenseNumber = licenseNumber;
            this.Portrait = new BitmapImage(uri);
        }
    }
}
