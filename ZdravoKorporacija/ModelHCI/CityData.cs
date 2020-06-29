using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{

    public class CityData
    {
        public static List<City> cities = new List<City>();
        public CityData()
        {

            City NoviSad = new City("Novi Sad", "Srbija");
            City Beograd = new City("Beograd", "Srbija");
            City Zagreb = new City("Zagreb", "Hrvatska");
            City Kikinda = new City("Kikinda", "Srbija");
            cities.Add(NoviSad);
            cities.Add(Beograd);
            cities.Add(Zagreb);
            cities.Add(Kikinda);
        }
    }
}
