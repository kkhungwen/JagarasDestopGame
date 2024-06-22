using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpeechBubbleSO_",menuName ="Scriptable Objects/Speech Bubble SO")]
public class SpeechBubbleSO : ScriptableObject
{
    public Sprite bubbleHeadSprite;
    public Sprite backGroundSprite;
    public Vector2 bubbleImageOffset;
    public float outLineOffset;

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(backGroundSprite), backGroundSprite);
        HelperUtils.ValidateCheckNullValue(this, nameof(bubbleHeadSprite), bubbleHeadSprite);
    }
    #endregion
#endif
}
