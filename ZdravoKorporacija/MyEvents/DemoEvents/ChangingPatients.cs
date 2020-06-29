using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.MyEvents.DemoEvents
{
    class ChangingPatients : EventArgs
    {
        public string text { get; set; }
    }

}
