using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class MedicationData
    {
        public static List<ModelHCI.MedicationHCI> medsToValidate = new List<ModelHCI.MedicationHCI>();
        public static List<ModelHCI.MedicationHCI> meds = new List<ModelHCI.MedicationHCI>();
        public static List<SymptomsHCI> allSymptoms = new List<SymptomsHCI>();
        public static List<AllergensHCI> allAllergies = new List<AllergensHCI>();
        public MedicationData()
        {
            List<string> rndIngredients1 = new List<string>();
            List<string> rndAllergens1 = new List<string>();

            rndIngredients1.Add("3mg bromazepam");
            rndIngredients1.Add("laktoza");
            rndIngredients1.Add("monohidrat");
            rndIngredients1.Add("akacija, osušena raspršivanjem");
            rndIngredients1.Add("skrob");
            rndIngredients1.Add("talk");
            rndIngredients1.Add("natrijum-laurilsulfat");
            rndIngredients1.Add("magnezijum-stearat");
            rndIngredients1.Add("kroskarmeloza-natrijum");
            rndIngredients1.Add("Chinolingelb lack");

            rndAllergens1.Add("benzodiazepin");

            MedicationHCI med1 = new MedicationHCI("Bromazepam", true, rndAllergens1, rndIngredients1, "Tableta 3mg");
            med1.id = 1;
            rndIngredients1 = new List<string>();
            rndAllergens1 = new List<string>();

            rndIngredients1.Add("250 mg amoksicilina u obliku amoksicilin, trihidrata ");
            rndIngredients1.Add("magnezijum-stearat");
            rndIngredients1.Add("celuloza mikrokristalna");
            rndIngredients1.Add("tvrde želatinske kapsule");

            rndAllergens1.Add("amoksicilin");
            rndAllergens1.Add("penicilin");


            MedicationHCI med2 = new MedicationHCI("Sinacilin", false, rndAllergens1, rndIngredients1, "Kapsula 250mg");

            med2.id = 2;
            rndIngredients1 = new List<string>();
            rndAllergens1 = new List<string>();

            rndIngredients1.Add("24 mg betahistin dihidrohlorida");
            rndIngredients1.Add("manitol");
            rndIngredients1.Add("celuloza mikrokristalna");
            rndIngredients1.Add("limunska kiselina, monohidrat");
            /* Silicijum-dioksid, koloidni, bezvodni*/
            rndIngredients1.Add("silicijum-dioksid, koloidni, bezvodni");
            rndIngredients1.Add("talk");

            rndAllergens1.Add("betahistin");

            MedicationHCI med3 = new MedicationHCI("Betaserc", false, rndAllergens1, rndIngredients1, "Tableta 24mg");

            rndIngredients1 = new List<string>();
            rndAllergens1 = new List<string>();

            rndIngredients1.Add("3mg bromazepam");
            rndIngredients1.Add("laktoza");
            rndIngredients1.Add("monohidrat");
            rndIngredients1.Add("akacija, osušena raspršivanjem");
            rndIngredients1.Add("skrob");
            rndIngredients1.Add("talk");
            rndIngredients1.Add("natrijum-laurilsulfat");
            rndIngredients1.Add("magnezijum-stearat");
            rndIngredients1.Add("kroskarmeloza-natrijum");
            rndIngredients1.Add("Chinolingelb lack");

            rndAllergens1.Add("benzodiazepin");

            allAllergies.Add(new AllergensHCI() { allergen = "benzodiazepin" });
            MedicationHCI med4 = new MedicationHCI("Lexilium", true, rndAllergens1, rndIngredients1, "Tableta 3mg");
            medsToValidate.Add(med1);
            medsToValidate.Add(med4);

            meds.Add(med2);
            meds.Add(med3);
        }


    }
}
