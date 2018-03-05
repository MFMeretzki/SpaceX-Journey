using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;
using TouchScript.Pointers;

public class JoystickGesture : Gesture
{
    #region public properties
    public Vector2 Direction
    {
        get
        {
            switch (State)
            {
                case GestureState.Began:
                case GestureState.Changed:
                case GestureState.Ended:
                    return direction;
                default:
                    return Vector2.zero;
            }
        }
    }

    public float Magnitude
    {
        get
        {

            switch (State)
            {
                case GestureState.Began:
                case GestureState.Changed:
                case GestureState.Ended:
                    return magnitude / distThreshold;
                default:
                    return 0.0f;
            }
        }
    }
    #endregion

    #region serialized fields
    [SerializeField]
    public Image border;
    [SerializeField]
    public Image pivot;
    [SerializeField]
    public float distThreshold;
    #endregion

    #region private fields
    Vector2 direction;
    float magnitude;
    Pointer primaryPointer;
    Vector2 startPosition;
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
            startPosition = primaryPointer.Position;
            border.transform.SetPositionAndRotation(startPosition, Quaternion.identity);
            border.enabled = true;
            pivot.transform.SetPositionAndRotation(startPosition, Quaternion.identity);
            pivot.enabled = true;
            setState(GestureState.Began);
        }
    }

    protected override void pointersUpdated (IList<Pointer> pointers)
    {
        foreach (var p in pointers)
        {
            if (p.Id == primaryPointer.Id)
            {
                ComputeValues(p.Position);
                pivot.transform.SetPositionAndRotation(startPosition + (direction * magnitude), Quaternion.identity);
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
                border.enabled = false;
                pivot.enabled = false;
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
                border.enabled = false;
                pivot.enabled = false;
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

    #region private methods
    private void ComputeValues (Vector2 pos)
    {
        Vector2 dir = pos - startPosition;
        direction = dir.normalized;
        magnitude = Mathf.Clamp(dir.magnitude, 0.0f, distThreshold);
    }    
    #endregion
}
