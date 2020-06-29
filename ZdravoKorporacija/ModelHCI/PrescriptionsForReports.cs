using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class PrescriptionsForReports
    {

        public List<PrescriptionHCI> allPrescriptions = new List<PrescriptionHCI>();
        public static List<Report> reports = new List<Report>();
        public List<PrescriptionHCI> GetPrescriptions()
        {
            return allPrescriptions;
        }
        public void addPrescriptions()
        {
            PrescriptionHCI p1 = new PrescriptionHCI();
            PrescriptionHCI p2 = new PrescriptionHCI();
            PrescriptionHCI p3 = new PrescriptionHCI();
            PrescriptionHCI p4 = new PrescriptionHCI();
            PrescriptionHCI p5 = new PrescriptionHCI();
            PrescriptionHCI p6 = new PrescriptionHCI();
            PrescriptionHCI p7 = new PrescriptionHCI();
            PrescriptionHCI p8 = new PrescriptionHCI();

            PrescriptionHCI p9 = new PrescriptionHCI();
            PrescriptionHCI p10 = new PrescriptionHCI();
            PrescriptionHCI p11 = new PrescriptionHCI();
            PrescriptionHCI p12 = new PrescriptionHCI();

            p1.id = 1;

            p2.id = 2;
            p3.id = 3;
            p4.id = 4;
            p5.id = 5;
            p6.id = 6;
            p7.id = 7;
            p8.id = 8;
            p9.id = 9;
            p10.id = 10;
            p11.id = 11;
            p12.id = 12;

            p1.dateOfPrescription = DateTime.Today;
            p2.dateOfPrescription = DateTime.Today.AddDays(-20);
            p3.dateOfPrescription = DateTime.Today.AddDays(-10);
            p4.dateOfPrescription = DateTime.Today.AddDays(-15);
            p5.dateOfPrescription = DateTime.Today.AddDays(-12);
            p6.dateOfPrescription = DateTime.Today.AddDays(-31);
            p7.dateOfPrescription = DateTime.Today.AddDays(-21);
            p8.dateOfPrescription = DateTime.Today.AddDays(-4);
            p9.dateOfPrescription = DateTime.Today.AddDays(-5);
            p10.dateOfPrescription = DateTime.Today.AddDays(-7);
            p11.dateOfPrescription = DateTime.Today.AddDays(-9);
            p12.dateOfPrescription = DateTime.Today.AddDays(-10);

            p1.reserved = true;
            p2.reserved = false;
            p3.reserved = true;
            p4.reserved = false;
            p5.reserved = true;
            p6.reserved = false;
            p7.reserved = true;
            p8.reserved = false;
            p9.reserved = true;
            p10.reserved = false;
            p11.reserved = true;
            p12.reserved = false;

            p1.startDate = DateTime.Today;
            p1.endDate = DateTime.Today.AddDays(10);
            p3.startDate = DateTime.Today.AddDays(-10);
            p3.endDate = DateTime.Today;
            p5.startDate = DateTime.Today.AddDays(-12);
            p5.endDate = DateTime.Today.AddDays(-2);
            p7.startDate = DateTime.Today.AddDays(-21);
            p7.endDate = DateTime.Today.AddDays(-11);
            p9.startDate = DateTime.Today.AddDays(-5);
            p9.endDate = DateTime.Today.AddDays(5);
            p11.startDate = DateTime.Today.AddDays(-9);
            p11.endDate = DateTime.Today.AddDays(1);

            p3.taken = true;
            p5.taken = true;
            p1.taken = false;
            p7.taken = false;
            p9.taken = false;
            p11.taken = true;

            p1.medication = MedicationData.meds[1];
            p2.medication = MedicationData.meds[0];
            p3.medication = MedicationData.meds[1];
            p4.medication = MedicationData.meds[0];
            p5.medication = MedicationData.meds[1];
            p6.medication = MedicationData.meds[0];
            p7.medication = MedicationData.meds[1];
            p8.medication = MedicationData.meds[0];
            p9.medication = MedicationData.meds[1];
            p10.medication = MedicationData.meds[0];
            p11.medication = MedicationData.meds[1];
            p12.medication = MedicationData.meds[0];

            allPrescriptions.Add(p1);
            allPrescriptions.Add(p2);
            allPrescriptions.Add(p3);
            allPrescriptions.Add(p4);
            allPrescriptions.Add(p5);
            allPrescriptions.Add(p6);
            allPrescriptions.Add(p7);
            allPrescriptions.Add(p8);
            allPrescriptions.Add(p9);
            allPrescriptions.Add(p10);
            allPrescriptions.Add(p11);
            allPrescriptions.Add(p12);
        }

    }
}
