﻿// 2015,2014 ,MIT, WinterDev
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LayoutFarm.WebDom;

namespace LayoutFarm.Ease
{
    public class EaseViewport
    {
        UserHtmlWorkspace userWorkspace = new UserHtmlWorkspace();
        LayoutFarm.UI.UISurfaceViewportControl viewportControl;
        SampleViewport sampleViewport;

        internal EaseViewport(LayoutFarm.UI.UISurfaceViewportControl viewportControl)
        {
            this.viewportControl = viewportControl;
            this.sampleViewport = new SampleViewport(viewportControl);
        }
        public Control ViewportControl
        {
            get { return this.viewportControl; }
        }
        public void Ready()
        {
            userWorkspace.OnStartDemo(sampleViewport);
        }
        public void LoadHtml(string originalFileName, string htmlText)
        {
            userWorkspace.LoadHtml(originalFileName, htmlText);
            viewportControl.PaintMeFullMode();
        }
        public WebDocument GetHtmlDom()
        {
            return userWorkspace.GetHtmlDom();
        }
        public void Print(EaseCanvas easeCanvas)
        {
            viewportControl.PrintMe(easeCanvas.Canvas);
        }
    }

}
