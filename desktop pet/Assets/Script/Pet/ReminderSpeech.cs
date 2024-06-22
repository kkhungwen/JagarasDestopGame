using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderSpeech : MonoBehaviour
{
    [SerializeField] private string reminderName;
    [SerializeField] private float remindTime;
    [SerializeField] bool resetOnRemind;
    [SerializeField] SpeechSO remindSpeech;

    private bool isRemind;
    private float remindTimeCount;

    private void Awake()
    {
        remindTimeCount = 0;    
        isRemind = false;
    }

    private void Update()
    {
        remindTimeCount += Time.unscaledDeltaTime;

        if(remindTimeCount >= remindTime)
        {
            isRemind = true;

            if (!resetOnRemind)
                remindTimeCount = 0;
        }
    }

    public bool GetRemind( out SpeechSO remindSpeech)
    {
        remindSpeech = this.remindSpeech;

        if (isRemind)
        {
            if (resetOnRemind)
                remindTimeCount = 0;

            isRemind = false;

            return true;
        }
        else
        {
            return false;
        }      
    }
}
