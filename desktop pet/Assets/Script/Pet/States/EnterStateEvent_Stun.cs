using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Stun : MonoBehaviour
{
    public event Action<EnterStateEvent_Stun> OnEnterStateStun;

    public void CallOnEnterStateStun()
    {
        OnEnterStateStun?.Invoke(this);
    }
}
