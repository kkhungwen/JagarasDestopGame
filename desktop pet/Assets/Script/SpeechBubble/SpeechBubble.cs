using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [Header("PARAMETERS")]
    [SerializeField] private Vector2 orginPosition;

    [Header("REFERENCE")]
    [SerializeField] private SpeechBubbleEvent speechBubbleEvent;
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private SpriteRenderer backGroundSprite;
    [SerializeField] private SpriteRenderer bubbleHeadSprite;
    [SerializeField] private Shake shake;

    private Vector2 bubbleImageOffset;
    private float outLineOffset;
    private bool isActive;
    private float printSpeed;
    private float bubbleStayDuration;
    private List<char> characters = new List<char>();
    private float timeCount;
    private int currentChar;

    private void Awake()
    {
        SetDown();
    }

    private void OnEnable()
    {
        if (speechBubbleEvent != null)
            speechBubbleEvent.OnSpeechBubble += SpeechBubbleEvent_OnSpeechBubble;
    }

    private void OnDisable()
    {
        if (speechBubbleEvent != null)
            speechBubbleEvent.OnSpeechBubble -= SpeechBubbleEvent_OnSpeechBubble;
    }

    private void SpeechBubbleEvent_OnSpeechBubble(SpeechBubbleEvent speechBubbleEvent, SpeechBubbleEventArgs speechBubbleEventArgs)
    {
        if (speechBubbleEventArgs.isActive)
            Activate(speechBubbleEventArgs.speech, speechBubbleEventArgs.isLeft);
        else
            DeActivate();
    }

    private void Update()
    {
        if (!isActive)
            return;

        PrintingText(Time.deltaTime);
    }

    public void Activate(SpeechSO speech, bool isLeft)
    {
        SetSpeechBubbleDetails(speech.speechBubbleSO);
        SetUpText(speech.fontSize, speech.speechString, speech.speechDuration, speech.bubbleStayDuration, isLeft);
        Shake(speech.isShake);

        isActive = true;
    }

    public void DeActivate()
    {
        SetDown();

        isActive = false;
    }

    private void SetUpText(float fontSize, string speechString, float speechDuration, float bubbleStayDuration, bool isLeft)
    {
        SetDown();

        //Set Text
        textMeshPro.fontSize = fontSize;
        textMeshPro.SetText(speechString);
        Vector2 textSize = new Vector2(textMeshPro.preferredWidth, textMeshPro.preferredHeight);
        textMeshPro.SetText("");
        characters = CreateCharacterList(speechString);

        //Set Size
        textMeshPro.GetComponent<RectTransform>().sizeDelta = textSize;
        backGroundSprite.size = new Vector2(textSize.x + bubbleImageOffset.x * 2, textSize.y + bubbleImageOffset.y * 2);

        //Set Position
        if (isLeft)
        {
            textMeshPro.rectTransform.localPosition = new Vector2(orginPosition.x - bubbleImageOffset.x - textSize.x / 2, orginPosition.y + bubbleImageOffset.y + textSize.y / 2);
            backGroundSprite.transform.localPosition = new Vector2(orginPosition.x - bubbleImageOffset.x - textSize.x / 2, orginPosition.y + bubbleImageOffset.y + textSize.y / 2);
            bubbleHeadSprite.transform.localPosition = new Vector2(orginPosition.x - bubbleImageOffset.x - bubbleHeadSprite.size.x / 2, outLineOffset + orginPosition.y - bubbleHeadSprite.size.y / 2);
            bubbleHeadSprite.flipX = true;
        }
        else
        {
            textMeshPro.rectTransform.localPosition = new Vector2(bubbleImageOffset.x + orginPosition.x + textSize.x / 2, bubbleImageOffset.y + orginPosition.y + textSize.y / 2);
            backGroundSprite.transform.localPosition = new Vector2(bubbleImageOffset.x + orginPosition.x + textSize.x / 2, bubbleImageOffset.y + orginPosition.y + textSize.y / 2);
            bubbleHeadSprite.transform.localPosition = new Vector2(orginPosition.x + bubbleImageOffset.x + bubbleHeadSprite.size.x / 2, outLineOffset + orginPosition.y - bubbleHeadSprite.size.y / 2);
            bubbleHeadSprite.flipX = false;
        }

        //speechBubbleTransform.localPosition = new Vector2(orginPosition.x + textSize.x/2 + bubbleImageOffset.x, orginPosition.y + textSize.y/2 + bubbleImageOffset.y);
        //bubbleHeadImage.rectTransform.localPosition = localPosition;

        //Set Print Parameters
        printSpeed = CaculatePrintSpeed(speechString, speechDuration);
        this.bubbleStayDuration = bubbleStayDuration;
        currentChar = 0;
        timeCount = 1000;

        //SetActicve
        bubbleHeadSprite.enabled = true;
        backGroundSprite.enabled = true;
    }

    private void SetSpeechBubbleDetails(SpeechBubbleSO speechBubbleSO)
    {
        backGroundSprite.sprite = speechBubbleSO.backGroundSprite;
        bubbleHeadSprite.sprite = speechBubbleSO.bubbleHeadSprite;
        bubbleImageOffset = speechBubbleSO.bubbleImageOffset;
        outLineOffset = speechBubbleSO.outLineOffset;
    }

    private void Shake(bool isShake)
    {
        if (isShake)
        {
            shake.StartShake();
        }
    }
    private void SetDown()
    {
        backGroundSprite.enabled = false;
        bubbleHeadSprite.enabled = false;
        textMeshPro.SetText("");
        textMeshPro.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    private List<char> CreateCharacterList(string text)
    {
        List<char> temp = new List<char>();
        foreach (char i in text)
        {
            temp.Add(i);
        }
        return temp;
    }

    private float CaculatePrintSpeed(string text, float duration)
    {
        float speed = duration / text.Length;

        if (speed < 0)
            speed = 0;

        return speed;
    }

    public void PrintingText(float delta)
    {
        timeCount += delta;

        if (currentChar < characters.Count)
        {
            if (timeCount >= printSpeed)
            {
                textMeshPro.text += characters[currentChar];
                currentChar++;
                timeCount = 0;
            }
        }
        else if (currentChar >= characters.Count)
        {
            if (timeCount >= bubbleStayDuration)
                DeActivate();
        }
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(textMeshPro), textMeshPro);
        HelperUtils.ValidateCheckNullValue(this, nameof(backGroundSprite), backGroundSprite);
        HelperUtils.ValidateCheckNullValue(this, nameof(bubbleHeadSprite), bubbleHeadSprite);
        HelperUtils.ValidateCheckNullValue(this, nameof(shake), shake);
    }
    #endregion
#endif
}
