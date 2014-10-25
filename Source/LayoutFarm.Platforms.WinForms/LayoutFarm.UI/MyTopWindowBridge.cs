﻿//2014 Apache2, WinterDev
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using LayoutFarm.Drawing;
using LayoutFarm.UI;
namespace LayoutFarm.Drawing
{   
    partial class MyTopWindowBridge
    {   
        CanvasEventsStock eventStock = new CanvasEventsStock();
        CanvasViewport canvasViewport;
        IUserEventPortal winEventBridge;
        TopWindowRenderBox topwin;
        Control windowControl;
        bool isMouseDown;
        bool isDragging;

        public event EventHandler<ScrollSurfaceRequestEventArgs> VScrollRequest;
        public event EventHandler<ScrollSurfaceRequestEventArgs> HScrollRequest;
        public event EventHandler<UIScrollEventArgs> VScrollChanged;
        public event EventHandler<UIScrollEventArgs> HScrollChanged;

        RootGraphic rootGraphic;

        public MyTopWindowBridge(TopWindowRenderBox topwin, IUserEventPortal winEventBridge)
        {

            this.winEventBridge = winEventBridge;
            this.topwin = topwin;
            this.rootGraphic = topwin.Root;

            topwin.CanvasForcePaint += (s, e) =>
            {
                PaintToOutputWindow();
            };

        }
        public void BindWindowControl(Control windowControl)
        {

            this.windowControl = windowControl;
            this.canvasViewport = new CanvasViewport(topwin, this.Size.ToSize(), 4);
#if DEBUG
            this.canvasViewport.dbugOutputWindow = this;
#endif
            this.EvaluateScrollbar();
        }
        void PaintToOutputWindow()
        {
            IntPtr hdc = GetDC(this.windowControl.Handle);
            canvasViewport.PaintMe(hdc);
            ReleaseDC(this.windowControl.Handle, hdc);
        }

        void PaintToOutputWindowIfNeed()
        {

            if (!this.canvasViewport.IsQuadPageValid)
            {
                IntPtr hdc = GetDC(this.windowControl.Handle);
                canvasViewport.PaintMe(hdc);
                ReleaseDC(this.windowControl.Handle, hdc);
            }
            
        }

        public void UpdateCanvasViewportSize(int w, int h)
        {
            this.canvasViewport.UpdateCanvasViewportSize(w, h);
        }


        System.Drawing.Size Size
        {
            get { return this.windowControl.Size; }
        }


        static UIMouseButtons GetUIMouseButton(MouseButtons mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButtons.Right:
                    return UIMouseButtons.Right;
                case MouseButtons.Middle:
                    return UIMouseButtons.Middle;
                case MouseButtons.None:
                    return UIMouseButtons.None;
                default:
                    return UIMouseButtons.Left;
            }
        }
        void SetUIMouseEventArgsInfo(UIMouseEventArgs mouseEventArg, MouseEventArgs e)
        {
            mouseEventArg.SetEventInfo(
                e.Location.ToPoint(),
                GetUIMouseButton(e.Button),
                e.Clicks,
                e.Delta);



            mouseEventArg.OffsetCanvasOrigin(this.canvasViewport.LogicalViewportLocation);
        }

        public void Close()
        {
            canvasViewport.Close();
        }

        //---------------------------------------------------------------------
        public void EvaluateScrollbar()
        {
            ScrollSurfaceRequestEventArgs hScrollSupportEventArgs;
            ScrollSurfaceRequestEventArgs vScrollSupportEventArgs;
            canvasViewport.EvaluateScrollBar(out hScrollSupportEventArgs, out vScrollSupportEventArgs);
            if (hScrollSupportEventArgs != null)
            {
                viewport_HScrollRequest(this, hScrollSupportEventArgs);
            }
            if (vScrollSupportEventArgs != null)
            {
                viewport_VScrollRequest(this, vScrollSupportEventArgs);
            }
        }
        public void ScrollBy(int dx, int dy)
        {

            UIScrollEventArgs hScrollEventArgs;
            UIScrollEventArgs vScrollEventArgs;
            canvasViewport.ScrollByNotRaiseEvent(dx, dy, out hScrollEventArgs, out vScrollEventArgs);
            if (vScrollEventArgs != null)
            {
                viewport_VScrollChanged(this, vScrollEventArgs);
            }
            if (hScrollEventArgs != null)
            {
                viewport_HScrollChanged(this, hScrollEventArgs);
            }


            PaintToOutputWindow();
        }
        public void ScrollTo(int x, int y)
        {
            Point viewporyLocation = canvasViewport.LogicalViewportLocation;

            if (viewporyLocation.Y == y && viewporyLocation.X == x)
            {
                return;
            }
            UIScrollEventArgs hScrollEventArgs;
            UIScrollEventArgs vScrollEventArgs;
            canvasViewport.ScrollToNotRaiseScrollChangedEvent(x, y, out hScrollEventArgs, out vScrollEventArgs);

            if (vScrollEventArgs != null)
            {
                viewport_VScrollChanged(this, vScrollEventArgs);

            }
            if (hScrollEventArgs != null)
            {
                viewport_HScrollChanged(this, vScrollEventArgs);
            }

            PaintToOutputWindow();
        }

        void viewport_HScrollChanged(object sender, UIScrollEventArgs e)
        {
            if (HScrollChanged != null)
            {
                HScrollChanged.Invoke(sender, e);
            }
        }
        void viewport_HScrollRequest(object sender, ScrollSurfaceRequestEventArgs e)
        {
            if (HScrollRequest != null)
            {
                HScrollRequest.Invoke(sender, e);
            }
        }
        void viewport_VScrollChanged(object sender, UIScrollEventArgs e)
        {
            if (VScrollChanged != null)
            {
                VScrollChanged.Invoke(sender, e);
            }
        }
        void viewport_VScrollRequest(object sender, ScrollSurfaceRequestEventArgs e)
        {
            if (VScrollRequest != null)
            {
                VScrollRequest.Invoke(sender, e);
            }
        }


        public void OnGotFocus(EventArgs e)
        {
            UIFocusEventArgs focusEventArg = eventStock.GetFreeFocusEventArgs(null, null);

            canvasViewport.FullMode = false;

            focusEventArg.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);

            this.winEventBridge.PortalGotFocus(focusEventArg);
            PaintToOutputWindowIfNeed();

            eventStock.ReleaseEventArgs(focusEventArg);
        }
        public void OnLostFocus(EventArgs e)
        {
            UIFocusEventArgs focusEventArg = eventStock.GetFreeFocusEventArgs(null, null);
            canvasViewport.FullMode = false;
            focusEventArg.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);

            this.winEventBridge.PortalLostFocus(focusEventArg);
            eventStock.ReleaseEventArgs(focusEventArg);
        }
        public void OnDoubleClick(EventArgs e)
        {
            //UIMouseEventArgs mouseE = new UIMouseEventArgs(); 
            //MouseEventArgs newMouseEventArgs = new MouseEventArgs(MouseButtons.Left, 1,
            //    lastestLogicalMouseDownX,
            //    lastestLogicalMouseDownY, 0);

            UIMouseEventArgs mouseEventArg = eventStock.GetFreeMouseEventArgs(this.topwin);
            //SetUIMouseEventArgsInfo(mouseEventArg, newMouseEventArgs);
            canvasViewport.FullMode = false;

            this.winEventBridge.PortalDoubleClick(mouseEventArg);
            PaintToOutputWindowIfNeed();
            eventStock.ReleaseEventArgs(mouseEventArg);
        }
        //---------------------------------------------------------------------

        public void OnMouseDown(MouseEventArgs e)
        {

            this.isMouseDown = true;
            this.topwin.MakeCurrent();

            canvasViewport.FullMode = false;

            UIMouseEventArgs mouseEventArg = eventStock.GetFreeMouseEventArgs(this.topwin);
            SetUIMouseEventArgsInfo(mouseEventArg, e);

            this.winEventBridge.PortalMouseDown(mouseEventArg);

            PaintToOutputWindowIfNeed();
            //---------------
#if DEBUG
            RootGraphic visualroot = this.topwin.dbugVRoot;
            if (visualroot.dbug_RecordHitChain)
            {
                dbug_rootDocHitChainMsgs.Clear();
                visualroot.dbug_DumpCurrentHitChain(dbug_rootDocHitChainMsgs);
                dbug_InvokeHitChainMsg();
            }
#endif
            //----------- 
            eventStock.ReleaseEventArgs(mouseEventArg);
        }
        public void OnMouseMove(MouseEventArgs e)
        {

            //interprete meaning ?
            Point viewLocation = canvasViewport.LogicalViewportLocation;
            UIMouseEventArgs mouseEventArg = eventStock.GetFreeMouseEventArgs(this.topwin);
            this.isDragging = mouseEventArg.IsDragging = this.isMouseDown;

            SetUIMouseEventArgsInfo(mouseEventArg, e);
            this.winEventBridge.PortalMouseMove(mouseEventArg);
            PaintToOutputWindowIfNeed();
            eventStock.ReleaseEventArgs(mouseEventArg);

        }
        public void OnMouseUp(MouseEventArgs e)
        {

            UIMouseEventArgs mouseEventArg = eventStock.GetFreeMouseEventArgs(this.topwin);
            mouseEventArg.IsDragging = this.isDragging;
            this.isDragging = this.isMouseDown = false;
            SetUIMouseEventArgsInfo(mouseEventArg, e);

            canvasViewport.FullMode = false;
            this.winEventBridge.PortalMouseUp(mouseEventArg);

            PaintToOutputWindowIfNeed();
            eventStock.ReleaseEventArgs(mouseEventArg);

            //isMouseDown = false;

            //if (isDraging)
            //{

            //    Point viewLocation = canvasViewport.LogicalViewportLocation;
            //    var mouseDragEventArg = eventStock.GetFreeDragEventArgs(
            //       e.Location.ToPoint(),
            //       GetUIMouseButton(e.Button),
            //       lastestLogicalMouseDownX,
            //       lastestLogicalMouseDownY,
            //       (viewLocation.X + e.X),
            //       (viewLocation.Y + e.Y),
            //       (viewLocation.X + e.X) - lastestLogicalMouseDownX,
            //       (viewLocation.Y + e.Y) - lastestLogicalMouseDownY);

            //    canvasViewport.FullMode = false;
            //    mouseDragEventArg.OffsetCanvasOrigin(viewLocation);
            //    userInputEventBridge.OnDragStop(mouseDragEventArg);

            //    PaintToOutputWindowIfNeed();

            //    eventStock.ReleaseEventArgs(mouseDragEventArg);

            //}
            //else
            //{ 
            //    UIMouseEventArgs mouseEventArg = eventStock.GetFreeMouseEventArgs(this.topwin);

            //    SetUIMouseEventArgsInfo(mouseEventArg, e);
            //    canvasViewport.FullMode = false;
            //    mouseEventArg.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);

            //    this.winEventBridge.OnMouseUp(mouseEventArg);

            //    PaintToOutputWindowIfNeed();
            //    eventStock.ReleaseEventArgs(mouseEventArg);
            //}
        }

        public void PaintMe()
        {
            if (canvasViewport != null)
            {
                //temp ? for debug
                canvasViewport.FullMode = true;
                PaintToOutputWindow();
            }
        }

        public void PaintMe(PaintEventArgs e)
        {
            PaintMe();
        }

        public void OnMouseWheel(MouseEventArgs e)
        {

            UIMouseEventArgs mouseEventArg = eventStock.GetFreeMouseEventArgs(this.topwin);
            SetUIMouseEventArgsInfo(mouseEventArg, e);
            canvasViewport.FullMode = true;

            this.winEventBridge.PortalMouseWheel(mouseEventArg);
            PaintToOutputWindowIfNeed();

            eventStock.ReleaseEventArgs(mouseEventArg);
        }
        public void OnKeyDown(KeyEventArgs e)
        {


            this.topwin.MakeCurrent();
            UIKeyEventArgs keyEventArgs = eventStock.GetFreeKeyEventArgs();

            SetKeyData(keyEventArgs, e);

            StopCaretBlink();
            canvasViewport.FullMode = false;
            keyEventArgs.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);
#if DEBUG
            topwin.dbugVisualRoot.dbug_PushLayoutTraceMessage("======");
            topwin.dbugVisualRoot.dbug_PushLayoutTraceMessage("KEYDOWN " + (LayoutFarm.UI.UIKeys)e.KeyData);
            topwin.dbugVisualRoot.dbug_PushLayoutTraceMessage("======");
#endif


            this.winEventBridge.PortalKeyDown(keyEventArgs);
            PaintToOutputWindowIfNeed();
            eventStock.ReleaseEventArgs(keyEventArgs);
        }
        void StartCaretBlink()
        {
            this.rootGraphic.CaretStartBlink();
        }
        void StopCaretBlink()
        {
            this.rootGraphic.CaretStopBlink();
        }

        public void OnKeyUp(KeyEventArgs e)
        {

            this.topwin.MakeCurrent();
            UIKeyEventArgs keyEventArgs = eventStock.GetFreeKeyEventArgs();
            SetKeyData(keyEventArgs, e);


            StopCaretBlink();
            canvasViewport.FullMode = false;
            keyEventArgs.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);


            this.winEventBridge.PortalKeyUp(keyEventArgs);

            StartCaretBlink();
            eventStock.ReleaseEventArgs(keyEventArgs);
        }
        static void SetKeyData(UIKeyEventArgs keyEventArgs, KeyEventArgs e)
        {
            keyEventArgs.SetEventInfo((int)e.KeyCode, e.Shift, e.Alt, e.Control);
        }
        public void OnKeyPress(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }


            UIKeyEventArgs keyPressEventArgs = eventStock.GetFreeKeyPressEventArgs();
            keyPressEventArgs.SetKeyChar(e.KeyChar);


            StopCaretBlink();
#if DEBUG
            topwin.dbugVisualRoot.dbug_PushLayoutTraceMessage("======");
            topwin.dbugVisualRoot.dbug_PushLayoutTraceMessage("KEYPRESS " + e.KeyChar);
            topwin.dbugVisualRoot.dbug_PushLayoutTraceMessage("======");
#endif

            canvasViewport.FullMode = false;
            keyPressEventArgs.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);

            this.winEventBridge.PortalKeyPress(keyPressEventArgs);

            PaintToOutputWindowIfNeed();


            eventStock.ReleaseEventArgs(keyPressEventArgs);
        }

        public bool ProcessDialogKey(Keys keyData)
        {

            UIKeyEventArgs keyEventArg = eventStock.GetFreeKeyEventArgs();
            keyEventArg.KeyData = (int)keyData;

            StopCaretBlink();
            canvasViewport.FullMode = false;
            keyEventArg.OffsetCanvasOrigin(canvasViewport.LogicalViewportLocation);

            bool result = this.winEventBridge.PortalProcessDialogKey(keyEventArg);

            eventStock.ReleaseEventArgs(keyEventArg);
            if (result)
            {
                PaintToOutputWindowIfNeed();

            }
            return result;
        }



        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);

    }



}
