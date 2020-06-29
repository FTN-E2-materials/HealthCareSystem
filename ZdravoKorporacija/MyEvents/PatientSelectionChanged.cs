using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.MyEvents
{
    public class PatientSelectionChanged : EventArgs
    {
        public static ModelHCI.Patient SelectedRow = null;
        public PatientSelectionChanged()
        {
            SelectedRow = Pages.Patients.SelectedRow; 
        }
    }
}
