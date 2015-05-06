﻿//BSD 2014-2015,WinterDev

using System;
using System.Collections.Generic;
using PixelFarm.Drawing;

namespace LayoutFarm.HtmlBoxes
{
    class CssFloatContainerBox : CssBox
    {
        public CssFloatContainerBox(Css.BoxSpec boxSpec, IRootGraphics rootgfx, Css.CssDisplay display)
            : base(boxSpec, rootgfx, display)
        {
        }
        internal override bool JustTempContainer
        {
            get
            {
                return true;
            }
        }
    }

}