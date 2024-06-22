using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeechArraySO_", menuName = "Scriptable Objects/Speech Array SO")]
public class SpeechArraySO : ScriptableObject
{
    public SpeechSO[] speechSOArray;

    public SpeechSO GetRandomSpeech()
    {
        return speechSOArray[Random.Range(0, speechSOArray.Length)];
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckEnumerableValues(this, nameof(speechSOArray), speechSOArray);
    }
    #endregion
#endif
}
