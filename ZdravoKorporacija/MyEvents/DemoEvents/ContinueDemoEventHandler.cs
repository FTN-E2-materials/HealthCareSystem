﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.MyEvents.DemoEvents
{
    class ContinueDemoEventHandler
    {
        static public event EventHandler CustomEvent;

        static public void RaiseMyCustomEvent(object sender, EventArgs args)
        {
            if (CustomEvent != null) CustomEvent(sender, args);
        }
    }
}