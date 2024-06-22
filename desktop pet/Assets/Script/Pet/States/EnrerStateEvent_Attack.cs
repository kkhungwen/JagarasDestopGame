using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Attack : MonoBehaviour
{
    public event Action<EnterStateEvent_Attack, EnterStateEventArgs_Attack> OnEnterStateAttack;

    public void CallEnterStateAttack(Vector3 targetPosition)
    {
        OnEnterStateAttack?.Invoke(this, new EnterStateEventArgs_Attack() { targetPosition = targetPosition });
    }
}

public class EnterStateEventArgs_Attack : EventArgs
{
    public Vector3 targetPosition;
}
