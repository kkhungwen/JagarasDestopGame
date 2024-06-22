using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class SpeechBubbleEvent : MonoBehaviour
{
    public event Action<SpeechBubbleEvent, SpeechBubbleEventArgs> OnSpeechBubble;

    public void CallOnSpeechBubble(SpeechSO speech, bool isLeft, bool isActive)
    {
        OnSpeechBubble?.Invoke(this, new SpeechBubbleEventArgs() { speech = speech, isLeft = isLeft, isActive = isActive });
    }
}

public class SpeechBubbleEventArgs : EventArgs
{
    public SpeechSO speech;

    public bool isLeft;

    public bool isActive;
}
