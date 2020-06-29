using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.ModelHCI
{
    public class VacationRequestHCI
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Doctor doctor { get; set; }
    }
}
