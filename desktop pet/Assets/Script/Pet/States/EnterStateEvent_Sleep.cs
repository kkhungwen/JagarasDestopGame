using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Sleep : MonoBehaviour
{
    public event Action<EnterStateEvent_Sleep> OnEnterState_Sleep;

    public void CallOnEnterStateSleep()
    {
        OnEnterState_Sleep?.Invoke(this);
    }
}
