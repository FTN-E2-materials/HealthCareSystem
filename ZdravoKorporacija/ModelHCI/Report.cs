using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class Report
    {

        public List<PrescriptionHCI> prescription { get; set; }
        public int numOnumOfPrescription { get; set; }
        public int numOfTaken { get; set;
                   
        }
        public List<MedicationHCI> medications { get; set; }
        public bool flag { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        public Report(MedicationHCI medToGenerate, bool all, DateTime start, DateTime end)
        {
            prescription = new List<PrescriptionHCI>();
            flag = all;
            dateFrom = start;
            medications = new List<MedicationHCI>();
            dateTo = end;
            if (!all)
            {
                medications.Add(medToGenerate);
                PrescriptionsForReports prescrtiptions = new PrescriptionsForReports();
                prescrtiptions.addPrescriptions();
                foreach (PrescriptionHCI med in prescrtiptions.GetPrescriptions())
                {
                    if (med.medication.name.ToLower().Equals(medToGenerate.name.ToLower()))
                    {
                        prescription.Add(med);
                    }
                }
            } else
            {
                PrescriptionsForReports prescrtiptions = new PrescriptionsForReports();
                prescrtiptions.addPrescriptions();
                foreach (PrescriptionHCI med in prescrtiptions.GetPrescriptions())
                {
                    if (!medications.Contains(med.medication))
                    {
                        medications.Add(med.medication);
                    }
                    prescription.Add(med);
                    
                }
            }
        }
  

        public List<PrescriptionHCI> GetPrescriptions()
        {
            return prescription;
        }

        public int calculateNumOfPresc(MedicationHCI meds)
        {
            int num = 0;
            foreach(PrescriptionHCI p in prescription)
            {
                if (p.medication.name.ToLower().Equals(meds.name.ToLower()))
                {
                    ++num;
                }
            }

            return num;
        }

        public int getTaken(MedicationHCI meds)
        {
            int num = 0;

            foreach (PrescriptionHCI p in prescription)
            {
                if (p.taken)
                {
                    ++num;
                }
            }

            return num;
        }
    }



}
