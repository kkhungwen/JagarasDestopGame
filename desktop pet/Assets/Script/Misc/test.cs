using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    public SpeechBubble speechBubble;
    public SpeechSO speech;
    public TextMeshPro tmp;

    private void Update()
    {
        tmp.text = Input.mousePosition.ToString();
    }
}
