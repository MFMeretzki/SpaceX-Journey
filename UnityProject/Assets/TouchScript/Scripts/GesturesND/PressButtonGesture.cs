using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;
using TouchScript.Pointers;

public class PressButtonGesture : Gesture
{
    #region public properties
    public bool ButtonPressed
    {
        get
        {
            switch (State)
            {
                case GestureState.Began:
                case GestureState.Changed:
                case GestureState.Ended:
                    return pressed;
                default:
                    return false;
            }
        }
    }
    #endregion

    #region private fields
    bool pressed;
    Pointer primaryPointer;
    #endregion

    #region events
    public event EventHandler<EventArgs> Pressed
    {
        add { pressedInvoker += value; }
        remove { pressedInvoker -= value; }
    }
    public event EventHandler<EventArgs> Moved
    {
        add { movedInvoker += value; }
        remove { movedInvoker -= value; }
    }
    public event EventHandler<EventArgs> Released
    {
        add { releasedInvoker += value; }
        remove { releasedInvoker -= value; }
    }
    private EventHandler<EventArgs> pressedInvoker, movedInvoker, releasedInvoker;
    #endregion

    #region Gesture callbacks
    protected override void pointersPressed (IList<Pointer> pointers)
    {
        if (State == GestureState.Idle)
        {
            primaryPointer = pointers[0];
            pressed = true;
            setState(GestureState.Began);
        }
    }

    protected override void pointersUpdated (IList<Pointer> pointers)
    {
        foreach (var p in pointers)
        {
            if (p.Id == primaryPointer.Id)
            {
                setState(GestureState.Changed);
                return;
            }
        }
    }

    protected override void pointersReleased (IList<Pointer> pointers)
    {
        foreach (var p in pointers)
        {
            if (p.Id == primaryPointer.Id)
            {
                pressed = false;
                setState(GestureState.Ended);
                return;
            }
        }
    }

    protected override void pointersCancelled (IList<Pointer> pointers)
    {
        foreach (var p in pointers)
        {
            if (p.Id == primaryPointer.Id)
            {
                pressed = false;
                setState(GestureState.Cancelled);
                return;
            }
        }
    }

    protected override void onBegan ()
    {
        if (pressedInvoker != null) pressedInvoker(this, EventArgs.Empty);
    }

    protected override void onRecognized ()
    {
        if (releasedInvoker != null) releasedInvoker(this, EventArgs.Empty);
    }

    protected override void onChanged ()
    {
        if (movedInvoker != null) movedInvoker(this, EventArgs.Empty);
    }

    protected override void reset ()
    {
        base.reset();
        primaryPointer = null;
    }
    #endregion
}
