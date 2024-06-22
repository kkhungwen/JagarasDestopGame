using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EnterStateEvent_Speech : MonoBehaviour
{
    public event Action<EnterStateEvent_Speech, EnterStateEventArgs_Speech> OnEnterStateSpeech;

    public void CallEnterStateSpeech(SpeechSO speech)
    {
        OnEnterStateSpeech?.Invoke(this, new EnterStateEventArgs_Speech() { speech = speech });
    }
}

public class EnterStateEventArgs_Speech : EventArgs
{
    public SpeechSO speech;
}
