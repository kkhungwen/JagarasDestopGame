using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class AttackDetectEvent : MonoBehaviour
{
    public event Action<AttackDetectEvent, AttackDetectEventArgs> OnAttackDetect;

    public void CallOnAttackDetect(Vector3 targetPosition)
    {
        OnAttackDetect?.Invoke(this, new AttackDetectEventArgs { targetPosition = targetPosition });
    }
}

public class AttackDetectEventArgs : EventArgs
{
    public Vector3 targetPosition;
}
