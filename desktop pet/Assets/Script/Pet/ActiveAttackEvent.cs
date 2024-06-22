using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class ActiveAttackEvent : MonoBehaviour
{
    public event Action OnActiveAttack;

    public void CallOnActiveAttack()
    {
        OnActiveAttack?.Invoke();
    }
}
