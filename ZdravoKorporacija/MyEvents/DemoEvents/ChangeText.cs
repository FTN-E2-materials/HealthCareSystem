﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKorporacija.MyEvents.DemoEvents
{
    public class ChangeText
    {
        static public event EventHandler CustomEvent;

        static public void RaiseMyCustomEvent(object sender, EventArgs args)
        {
            if (CustomEvent != null) CustomEvent(sender, args);
        }
    }
}
