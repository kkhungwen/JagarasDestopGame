using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpeechSO_",menuName ="Scriptable Objects/Speech SO")]
public class SpeechSO : ScriptableObject
{
    public SpeechBubbleSO speechBubbleSO;
    [TextArea] public string speechString;
    public float speechDuration;
    public float bubbleStayDuration;
    public float fontSize;
    public bool isShake;

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(speechBubbleSO), speechBubbleSO);
    }
    #endregion
#endif
}
