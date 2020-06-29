using System;
using System.Collections.Generic;
using System.Text;
using Model.Medications;

namespace ZdravoKorporacija.ModelHCI
{

    public class MedicationHCI
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<string> ingredients { get; set; }
        public List<string> allergens { get; set; }
        public bool isForValidation;
        public string format { get; set; }
        public string Manufactuer { get; set; }
        public Medication medication {get; set;}

        public MedicationHCI() { }
        public MedicationHCI(string name, bool isForValid, List<string> allergens, List<string> ingredients, string format)
        {
            this.ingredients = ingredients;
            this.allergens = allergens;
            this.name = name;
            this.isForValidation = isForValid;
            this.format = format;
            this.Manufactuer = "Hemofarm";
        }

    }
}
