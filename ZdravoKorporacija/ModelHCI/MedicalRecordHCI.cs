using Model.ExaminationSurgery;
using Model.MedicalRecord;
using Model.Medications;
using System;
using System.Collections.Generic;

namespace ZdravoKorporacija.ModelHCI
{
    public abstract class TreatmentHCI
    {

    }
    
    public class HospitalTreatmentHCI : TreatmentHCI
    {
        public HospitalTreatment hospitalTreatment { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string department { get; set; }
        public string room { get; set; }

        public string reasonWhy { get; set; }

        public HospitalTreatmentHCI() { }
    }
    public class PrescriptionHCI : TreatmentHCI
    {
        public Prescription prescription { get; set; }
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public MedicationHCI medication { get; set; }
        public DateTime dateOfPrescription { get; set; }
        public Patient patient { get; set; }
        public bool reserved { get; set; }
        public bool taken { get; set; }
        public string reasonWhy { get; set; }
        public int intake { get; set; }

        public PrescriptionHCI() { }
    }

    public class TherapyHCI
    {

        public MedicationHCI medication { get; set; }

        public Therapy therapy { get; set; } 
        public int hourlyUsage { get; set; }

        public TherapyHCI() { }
    }


    public class DiagnosisHCI
    {
        public Diagnosis diagnosis { get; set; }
        public string name { get; set; }
        public List<Symptoms> symptoms { get; set; }

        public DiagnosisHCI() { }
    }

    public class SymptomsHCI
    {
        public Symptoms symptoms { get; set; }
        public string symptom { get; set; }

        public SymptomsHCI() { }
    }
    public class AllergensHCI
    {
        public Allergens allergens { get; set; }
        public string allergen { get; set; }

        public AllergensHCI() { }
    }

    public class VaccineHCI
    {
        public Vaccines vaccines { get; set; }
        public string vaccine { get; set; }

        public VaccineHCI() { }
    }

    public class ExaminationSurgeryHCI
    {
        public Type tip { get; set; }
        public List<TreatmentHCI> treatments { get; set; }
        public DateTime date { get; set; }
        public string dateString { get; set; }
        public MedicalRecordHCI medicalRecord { get; set; }
        public List<AppointmentHCI> appointmentsScheduled { get; set; }
        public List<String> labtest { get; set; }
        public List<String> dijagnoze { get; set; }
        public Patient patient { get; set; }
        public ExaminationSurgery examinationSurgery { get; set; }
        

        public ExaminationSurgeryHCI() {
            treatments = new List<TreatmentHCI>();
            labtest = new List<string>();
            dijagnoze = new List<string>();
            appointmentsScheduled = new List<AppointmentHCI>();
            date = DateTime.Today;
        }
    }
    public class LabTestingHCI
    {
        public List<String> types { get; set; }
        public List<double> values { get; set; }
        public string dateString { get; set; }
        public List<double> maxRefValue { get; set; }
        public List<double> minRefValue { get; set; }
        public MedicalRecordHCI medicalRecord { get; set; }
      
        public LabTestingHCI()
        {
            types = new List<string>();
            values = new List<double>();
            minRefValue = new List<double>();
            maxRefValue = new List<double>();
        }
    }

    public class MedicalRecordHCI
    {

        public Patient patient {get; set;}
        public List<HospitalTreatmentHCI> HospitalTreatments { get;  set; }

        public bool activeHospitalTreatment { get; set; }

        public int medicalRecordNumber { get; set; }
        public List<PrescriptionHCI> Prescriptions { get; set; }
        public List<TherapyHCI> Therapies { get; set; }
        public List<DiagnosisHCI> illnessHistory { get; set; }

        public List<AllergensHCI> allergies { get; set; }
        public List<DiagnosisHCI> familyIllnessHistory { get; set; }
        public List<ExaminationSurgeryHCI> examinations { get; set; }
        public List<VaccineHCI> vaccines { get; set; }
        public List<LabTestingHCI> testing { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public MedicalRecordHCI() {
            HospitalTreatments = new List<HospitalTreatmentHCI>();
            Prescriptions = new List<PrescriptionHCI>();
            Therapies = new List<TherapyHCI>();
            illnessHistory = new List<DiagnosisHCI>();
            familyIllnessHistory = new List<DiagnosisHCI>();
            allergies = new List<AllergensHCI>();
            examinations = new List<ExaminationSurgeryHCI>();
            testing = new List<LabTestingHCI>();
        }
    }
}
