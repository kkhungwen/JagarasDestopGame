using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Space(10f)]
    [Header("SLOW MOTION")]

    [SerializeField] private AnimationCurve slowdownCurve;
    private float slowdownTime = 1f;
    private float slowdownTimeCount;
    private bool isSlowDown;

    void Update()
    {
        HandleSlowMotion();
    }

    public void SlowMotion(float slowdownTime)
    {
        this.slowdownTime = slowdownTime;

        slowdownTimeCount = 0;

        Time.timeScale = slowdownCurve.Evaluate(0);

        isSlowDown = true;
    }

    private void HandleSlowMotion()
    {
        if (!isSlowDown)
            return;

        slowdownTimeCount += Time.unscaledDeltaTime;

        if (slowdownTimeCount >= slowdownTime)
        {
            Time.timeScale = 1;
            isSlowDown = false;
            return;
        }

        Time.timeScale = slowdownCurve.Evaluate(slowdownTimeCount / slowdownTime);
    }
}
