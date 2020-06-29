using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class ReportData
    {
        public List<Report> medicalReports = new List<Report>();
        public static List<ExaminationSurgeryHCI> examinationReports = new List<ExaminationSurgeryHCI>();
        public static List<ValidationMed> validations = new List<ValidationMed>();
        public List<PrescriptionHCI> prescriptions = new List<PrescriptionHCI>();
        
        public void addMedical(Report report)
        {
            medicalReports.Add(report);
        }

        public Report getLastMedicalReport()
        {
            if (medicalReports.Count > 0)
            {
                return medicalReports[medicalReports.Count - 1];
            } else
            {
                return null;
            }
        }


        public void addValidationMed(ValidationMed report)
        {
            validations.Add(report);
        }

        public ValidationMed getLastValidation()
        {
            if (validations.Count > 0)
            {
                return validations[validations.Count - 1];
            }
            else
            {
                return null;
            }
        }


        public void addPrescriptionReport(PrescriptionHCI report)
        {
            prescriptions.Add(report);
        }

        public PrescriptionHCI getLastPrescription()
        {
            if (prescriptions.Count > 0)
            {
                return prescriptions[prescriptions.Count - 1];
            }
            else
            {
                return null;
            }
        }



        public void addExamination(ExaminationSurgeryHCI report)
        {
            examinationReports.Add(report);
        }

        public ExaminationSurgeryHCI getLastExamination()
        {
            if (examinationReports.Count > 0)
            {
                return examinationReports[examinationReports.Count - 1];
            }
            else
            {
                return null;
            }
        }
    }
}
