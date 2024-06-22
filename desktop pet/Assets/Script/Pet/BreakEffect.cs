using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakEffect : MonoBehaviour
{
    [SerializeField] private BreakEvent breakEvent;

    [Space(10f)]
    [Header("BREAK POP UP")]
    [SerializeField] private GameObject breakPopUpPrefab;

    [Space(10f)]
    [Header("SLOW MOTION")]
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private float slowdownTime = 1;
    [SerializeField] private float slowDownTimeSubtractPopUpTime = 0.9f;

    private void Awake()
    {
        breakPopUpPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        breakEvent.OnBreak += BreakEvent_OnBreak;
    }
    private void OnDisable()
    {
        breakEvent.OnBreak -= BreakEvent_OnBreak;
    }

    private void BreakEvent_OnBreak(BreakEvent breakEvent)
    {
        StartCoroutine(BreakEffectRoutine());
    }

    private void BreakPopUp()
    {
        breakPopUpPrefab.SetActive(false);
        breakPopUpPrefab.SetActive(true);
    }

    private void SlowMotion()
    {
        timeManager.SlowMotion(slowdownTime);
    }

    private IEnumerator BreakEffectRoutine()
    {
        SlowMotion();

        yield return new WaitForSecondsRealtime(slowdownTime - slowDownTimeSubtractPopUpTime);

        BreakPopUp();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(breakEvent), breakEvent);
        HelperUtils.ValidateCheckNullValue(this, nameof(breakPopUpPrefab), breakPopUpPrefab);
    }
#endif
}
