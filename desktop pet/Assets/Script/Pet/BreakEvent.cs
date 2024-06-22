using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class BreakEvent : MonoBehaviour
{
    public event Action<BreakEvent> OnBreak;

    public void CallOnBreak()
    {
        OnBreak?.Invoke(this);
    }
}
