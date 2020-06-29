using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class PatientsData
    {
        public static List<Patient> patientsList = new List<Patient>();
        public static List<MedicalRecordHCI> medicalRecords = new List<MedicalRecordHCI>();
        public static List<DiagnosisHCI> allDiagnosis = new List<DiagnosisHCI>();

        public PatientsData()
        {
            
            Patient patient1 = new Patient("Nikola", "Nikolić", "40");
            patient1.Portrait = new BitmapImage(new Uri("/Resources/ragnar.jpg", UriKind.Relative));
            Patient patient2 = new Patient("Marko", "Marković", "");
            patient2.Portrait = new BitmapImage(new Uri("/Resources/Marhal.jpg", UriKind.Relative));
            Patient patient3 = new Patient("Žarko", "Žarković", "");
            patient3.Portrait = new BitmapImage(new Uri("/Resources/office.jpg", UriKind.Relative));
            Patient patient4 = new Patient("Petar", "Petrović", "");
            patient4.Portrait = new BitmapImage(new Uri("/Resources/Luke.jpg", UriKind.Relative));
            patient4.LastAppointment = "04.05.2020";
            patient2.LastAppointment = "05.05.2020";
            patient3.LastAppointment = "03.05.2020.";
            patient1.LastAppointment = "01.05.2020";
            patient4.LastDiagnosis = "Pesak u bubregu";
            patient3.LastDiagnosis = "Diabetes II";
            patient2.LastDiagnosis = "Tahikardija";
            patient1.LastDiagnosis = "COVID-19";

           


            patientsList.Add(patient1);
            patientsList.Add(patient2);
            patientsList.Add(patient3);
            patientsList.Add(patient4);

            MedicalRecordHCI medicalRecord1 = new MedicalRecordHCI();
            medicalRecord1.medicalRecordNumber = 1;
            medicalRecord1.patient = patient1;

            medicalRecord1.illnessHistory = new List<DiagnosisHCI>();
            MedicationData.allSymptoms.Add(new SymptomsHCI() { symptom = "kašalj" });
            MedicationData.allSymptoms.Add(new SymptomsHCI() { symptom = "zapaljenje pluća" });
            MedicationData.allSymptoms.Add(new SymptomsHCI() { symptom = "migrena" });
            MedicationData.allSymptoms.Add(new SymptomsHCI() { symptom = "vrtoglavica" });
            MedicationData.allSymptoms.Add(new SymptomsHCI() { symptom = "povraćanje" });
            medicalRecord1.Therapies = new List<TherapyHCI>();
            medicalRecord1.Therapies.Add(new TherapyHCI() { medication = MedicationData.meds[0], hourlyUsage = 12 });

            medicalRecord1.allergies = new List<AllergensHCI>();
            medicalRecord1.allergies.Add(new AllergensHCI() { allergen = "Polen" });
            medicalRecord1.examinations = new List<ExaminationSurgeryHCI>();

            medicalRecord1.vaccines = new List<VaccineHCI>();
            medicalRecord1.vaccines.Add(new VaccineHCI() { vaccine = "COVID-19" });

            LabTestingHCI test = new LabTestingHCI();
            test.types.Add("RBC");
            test.types.Add("WBC");
            test.types.Add("PLT");
            test.types.Add("Htc");
            test.types.Add("Hgb");

            test.values.Add(7.3);
            test.values.Add(5);
            test.values.Add(300);
            test.values.Add(0.5);
            test.values.Add(150);

            test.minRefValue.Add(3.8);
            test.minRefValue.Add(3.9);
            test.minRefValue.Add(140);
            test.minRefValue.Add(0.35);
            test.minRefValue.Add(110);

            test.maxRefValue.Add(5.70);
            test.maxRefValue.Add(109);
            test.maxRefValue.Add(450);
            test.maxRefValue.Add(0.53);
            test.maxRefValue.Add(180);

            test.dateString = DateTime.Today.ToString("dd.MM.yyyy.");
            medicalRecord1.testing.Add(test);
            
            patient1.record = medicalRecord1;

            MedicalRecordHCI medicalRecord2 = new MedicalRecordHCI();
            medicalRecord2.patient = patient2;
            patient2.record = medicalRecord2;
            medicalRecord2.medicalRecordNumber = 2;
            MedicalRecordHCI medicalRecord3 = new MedicalRecordHCI();
            medicalRecord3.patient = patient3;
            patient3.record = medicalRecord3;
            medicalRecord3.medicalRecordNumber = 3;
            MedicalRecordHCI medicalRecord4 = new MedicalRecordHCI();
            medicalRecord4.patient = patient4;
            patient4.record = medicalRecord4;
            medicalRecord4.medicalRecordNumber = 4;
            medicalRecord4.testing = new List<LabTestingHCI>();
            medicalRecord4.testing.Add(test);

            medicalRecord4.vaccines = new List<VaccineHCI>();

            medicalRecords = new List<MedicalRecordHCI>();

            medicalRecords.Add(medicalRecord1);
            medicalRecords.Add(medicalRecord2);
            medicalRecords.Add(medicalRecord3);
            medicalRecords.Add(medicalRecord4);

        }
    }
}
