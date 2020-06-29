using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class City
    {
        public string CityName { get; set; }
        public string State { get; set; }

        public City(string CityName, string State)
        {
            this.CityName = CityName;
            this.State = State;
            
        }
    }
}
