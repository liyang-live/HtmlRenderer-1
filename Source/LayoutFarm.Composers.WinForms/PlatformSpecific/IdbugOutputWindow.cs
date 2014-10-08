﻿//2014 Apache2, WinterDev
using System;
using System.Collections.Generic;
using LayoutFarm.Drawing;

namespace LayoutFarm.Drawing
{
#if DEBUG

    public interface IdbugOutputWindow
    { 
        List<dbugLayoutMsg> dbug_rootDocDebugMsgs { get; }
        void dbug_InvokeVisualRootDrawMsg();
        void dbug_HighlightMeNow(Rectangle r); 
        event EventHandler dbug_VisualRootDrawMsg;
        event EventHandler dbug_VisualRootHitChainMsg; 
        List<dbugLayoutMsg> dbug_rootDocHitChainMsgs { get; } 
        void dbug_InvokeHitChainMsg();
        void dbug_BeginLayoutTraceSession(string beginMsg);
        void dbug_DisableAllDebugInfo();
        void dbug_EnableAllDebugInfo();
        void dbug_ReArrangeWithBreakOnSelectedNode();
        bool vinv_dbugBreakOnSelectedVisuallElement { get; set; }

    }
#endif
}