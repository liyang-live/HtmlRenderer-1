﻿using System;
using System.Collections.Generic;
using System.Drawing;
 


namespace LayoutFarm.Presentation
{
    partial class ArtUIElement
    {
                                                                int oneBitNativeEventFlags;

                                        protected void RegisterNativeEvent(int eventFlags)
        {
            this.oneBitNativeEventFlags |= eventFlags;
        }


        protected virtual void OnDoubleClick(ArtMouseEventArgs e)
        {


        }
        protected virtual void OnMouseDown(ArtMouseEventArgs e)
        {


        }
        protected virtual void OnMouseWheel(ArtMouseEventArgs e)
        {
        }
        protected virtual void OnDragStart(ArtDragEventArgs e)
        {

        }
        protected virtual void OnDragEnter(ArtDragEventArgs e)
        {

        }
        protected virtual void OnDragOver(ArtDragEventArgs e)
        {

        }
        protected virtual void OnDragLeave(ArtDragEventArgs e)
        {

        }
        protected virtual void OnDragStop(ArtDragEventArgs e)
        {

        }
        protected virtual void OnDragging(ArtDragEventArgs e)
        {
        }
        protected virtual void OnDragDrop(ArtDragEventArgs e)
        {
        }
                protected virtual void OnCollapsed()
        {
        }
        protected virtual void OnExpanded()
        {

        }
                protected virtual void OnElementLanded()
        {

        }

        protected virtual void OnShown()
        {
        }
        protected virtual void OnHide()
        {
        }
        protected virtual void OnNotifyChildDoubleClick(ArtUIElement fromElement, ArtMouseEventArgs e)
        {

        }
                protected virtual void OnKeyDown(ArtKeyEventArgs e)
        { 
        }
        protected virtual void OnKeyUp(ArtKeyEventArgs e)
        { 
        }
        protected virtual void OnKeyPress(ArtKeyPressEventArgs e)
        {
        }
        protected virtual bool OnProcessDialogKey(ArtKeyEventArgs e)
        {
            return false;
        }

        protected virtual void OnMouseMove(ArtMouseEventArgs e)
        {
                                            }
        protected virtual void OnMouseHover(ArtMouseEventArgs e)
        {
                    }
        protected virtual void OnMouseUp(ArtMouseEventArgs e)
        {
                    }
        protected virtual void OnMouseEnter(ArtMouseEventArgs e)
        {
                                    
                    }
        protected virtual void OnMouseLeave(ArtMouseEventArgs e)
        {
                    }
        protected virtual void OnDropInto()
        {

        }
                protected virtual void OnSizeChanged(ArtSizeChangedEventArgs e)
        {

        }

    }
}