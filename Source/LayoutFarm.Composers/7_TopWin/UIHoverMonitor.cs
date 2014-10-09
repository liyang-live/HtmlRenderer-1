﻿//2014 Apache2, WinterDev
using System;
using System.Collections.Generic;
using System.Text;
using LayoutFarm.Drawing;

namespace LayoutFarm.UI
{


    public class UIHoverMonitorTask : VisualRootTimerTask
    {

        int mouseMoveCounter = -1;
        EventHandler targetEventHandler;
        public UIHoverMonitorTask(TopWindowRenderBoxBase winroot, EventHandler targetEventHandler)
            : base(winroot)
        {
            this.targetEventHandler = targetEventHandler;
        }
        public override void Reset()
        {
            mouseMoveCounter = -1;
        }
        public override void Tick()
        {
            mouseMoveCounter++;
            if (mouseMoveCounter > 1)
            {
                targetEventHandler(this, EventArgs.Empty);
            }
        }
    }


}