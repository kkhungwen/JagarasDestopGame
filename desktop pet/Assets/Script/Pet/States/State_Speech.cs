using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(SpeechBubbleEvent))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(EnterStateEvent_Speech))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[RequireComponent(typeof(EnterStateEvent_Drag))]

public class State_Speech : MonoBehaviour, IPetState
{
    private Pet pet;
    private SpeechBubbleEvent speechBubbleEvent;
    private TryHitEvent tryHitEvent;
    private EnterStateEvent_Speech enterStateEvent_Speech;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Stun enterStateEvent_Stun;
    private EnterStateEvent_Drag enterStateEvent_Drag;

    private float speechDuration = 0;

    private void Awake()
    {
        pet = GetComponent<Pet>();
        speechBubbleEvent = GetComponent<SpeechBubbleEvent>();
        tryHitEvent = GetComponent<TryHitEvent>();
        enterStateEvent_Speech = GetComponent<EnterStateEvent_Speech>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
    }

    private void OnEnable()
    {
        enterStateEvent_Speech.OnEnterStateSpeech += EnterStateEvent_Speech_OnEnterStateSpeech;
    }
    private void OnDisable()
    {
        enterStateEvent_Speech.OnEnterStateSpeech -= EnterStateEvent_Speech_OnEnterStateSpeech;
    }

    private void EnterStateEvent_Speech_OnEnterStateSpeech(EnterStateEvent_Speech enterStateEvent_Speech, EnterStateEventArgs_Speech enterStateEventArgs_Speech)
    {
        EnterState(enterStateEventArgs_Speech.speech);
    }

    public void StateUpdate()
    {
        speechDuration -= Time.deltaTime;

        if (speechDuration < 0)
        {
            ExitState();

            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    public void GetHit(Vector3 position, bool isCritical)
    {
        ExitState();

        enterStateEvent_Stagger.CallOnEnterStateStagger();
    }

    public void LeftClick(Vector3 position)
    {
        tryHitEvent.CallTryHit(position);
    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {

    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {

    }

    public void StartLeftDrag()
    {
        ExitState();

        enterStateEvent_Drag.CallOnEnterStateDrag();
    }

    public void Stun()
    {
        ExitState();

        enterStateEvent_Stun.CallOnEnterStateStun();
    }

    public void TryAttack(Vector3 targetPosition)
    {

    }

    private void EnterState(SpeechSO speech)
    {
        speechDuration = speech.speechDuration + speech.bubbleStayDuration;

        speechBubbleEvent.CallOnSpeechBubble(speech, IsTransformScreenPositionRight(), true);

        pet.SetState(this);
    }

    private void ExitState()
    {
        speechBubbleEvent.CallOnSpeechBubble(null, true, false);
    }

    private bool IsTransformScreenPositionRight()
    {
        Vector2 midPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));

        if (transform.position.x > midPosition.x)
            return true;
        else return false;
    }
}
