﻿using UnityEngine;

public enum XEventDefine
{
    XEvent_Invalid = -1,
    XEvent_JoyStick_Cancel = 0,
    XEvent_Gesture_Cancel,
    XEvent_Camera_CloseUp,
    XEvent_Camera_CloseUpEnd,
    XEvent_Camera_Action,

    XEvent_Num
}


public class XJoyStickCancelEvent : XEventArgs
{
    public XJoyStickCancelEvent() : base()
    {
        _eDefine = XEventDefine.XEvent_JoyStick_Cancel;
    }
}


public class XGestureCancelEvent : XEventArgs
{
    public XGestureCancelEvent() : base()
    {
        _eDefine = XEventDefine.XEvent_Gesture_Cancel;
    }
}


public class XCameraCloseUpEvent : XEventArgs
{
    public XEntity Target;

    public XCameraCloseUpEvent() : base()
    {
        _eDefine = XEventDefine.XEvent_Camera_CloseUp;
    }
}

public class XCameraCloseUpEndEvent : XEventArgs
{
    public XCameraCloseUpEndEvent() : base()
    {
        _eDefine = XEventDefine.XEvent_Camera_CloseUpEnd;
    }

    public override void Recycle()
    {
        base.Recycle();
    }

}

public class XCameraActionEvent : XEventArgs
{
    public float To_Rot_X, To_Rot_Y = 0;

    public XCameraActionEvent() : base()
    {
        _eDefine = XEventDefine.XEvent_Camera_Action;
    }

    public override void Recycle()
    {
        base.Recycle();
        To_Rot_X = 0;
        To_Rot_Y = 0;
    }

}


public class XAIEventArgs : XEventArgs
{
    public bool DepracatedPass;
    public int EventType;
    public string EventArg;

    public XAIEventArgs() : base()
    {
        _eDefine = XEventDefine.XEvent_Camera_Action;
    }

    public override void Recycle()
    {
        base.Recycle();
        DepracatedPass = false;
        EventType = 1;
        EventArg = string.Empty;
    }

}