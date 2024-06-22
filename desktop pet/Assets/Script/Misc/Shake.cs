using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float shakeStrength;

    private Vector3 originalPosition;
    float shakeTimeCount;
    bool isShaking;

    private void Update()
    {
        if (!isShaking)
            return;

        shakeTimeCount += Time.deltaTime;
        float strength = curve.Evaluate(shakeTimeCount / duration) * shakeStrength;
        transform.localPosition = (Vector2)originalPosition + Random.insideUnitCircle * strength;

        if (shakeTimeCount > duration)
        {
            transform.localPosition = originalPosition;
            isShaking = false;
        }
    }

    public void StartShake()
    {
        if (isShaking)
            return;

        originalPosition = transform.localPosition;
        shakeTimeCount = 0;

        isShaking = true;
    }
}
