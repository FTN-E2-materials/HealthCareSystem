using System;
using System.Collections.Generic;
using System.Text;
using Model.Medications;
namespace ZdravoKorporacija.ModelHCI
{
    public class ValidationMed
    {

        public DateTime dateOfValidation { get; set; }
        public Doctor doctorWhoValidated { get; set; }
        public MedicationHCI medThatIsValidated { get; set; }
        public ValidationMed validation { get; set; }
        
        public bool safe { get; set; }
    }


    public static class allValidation
    {
        public static List<ValidationMed> validations = new List<ValidationMed>();
        public static void addValiation(ValidationMed vmed)
        {
            validations.Add(vmed);
        }

    }
}
