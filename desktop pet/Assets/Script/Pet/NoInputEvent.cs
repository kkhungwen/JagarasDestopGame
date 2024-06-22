using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class NoInputEvent : MonoBehaviour
{
    public event Action<bool> OnNoInput;

    public void CallNoInput(bool isNoInput)
    {
        OnNoInput?.Invoke(isNoInput);
    }
}
