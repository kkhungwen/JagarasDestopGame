using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(NoInputEvent))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Patrol))]
[RequireComponent(typeof(EnterStateEvent_Attack))]
[RequireComponent(typeof(EnterStateEvent_Speech))]
[RequireComponent(typeof(EnterStateEvent_Sleep))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[DisallowMultipleComponent]
public class State_Idle : MonoBehaviour, IPetState
{
    private Pet pet;

    // Events
    private TryHitEvent tryHitEvent;
    private NoInputEvent noInputEvent;  
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Patrol enterStateEvent_Patrol;
    private EnterStateEvent_Attack enterStateEvent_Attack;
    private EnterStateEvent_Speech enterStateEvent_Speech;
    private EnterStateEvent_Sleep enterStateEvent_Sleep;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    // Configurables
    [Space(10f)]
    [Header("IDLE MOVEMENT")]
    [SerializeField] private float idleTimeMin;
    [SerializeField] private float idleTimeMax;

    // Reminder
    [Space(10f)]
    [Header("REMINDER")]
    [SerializeField] ReminderSpeech[] reminderSpeechArray;

    // Idle Parameters
    private float idleTime;

    // Sleep Parameters
    private bool isNoInput = false;

    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        tryHitEvent = GetComponent<TryHitEvent>();
        noInputEvent = GetComponent<NoInputEvent>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Patrol = GetComponent<EnterStateEvent_Patrol>();
        enterStateEvent_Attack = GetComponent<EnterStateEvent_Attack>();
        enterStateEvent_Speech = GetComponent<EnterStateEvent_Speech>();
        enterStateEvent_Sleep = GetComponent<EnterStateEvent_Sleep>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
    }

    private void OnEnable()
    {
        enterStateEvent_Idle.OnEnterState_Idle += EnterStateEvent_Idle_OnEnterState_Idle;
        noInputEvent.OnNoInput += NoInputEvent_OnNoInput;
    }
    private void OnDisable()
    {
        enterStateEvent_Idle.OnEnterState_Idle -= EnterStateEvent_Idle_OnEnterState_Idle;
        noInputEvent.OnNoInput -= NoInputEvent_OnNoInput;
    }

    private void EnterStateEvent_Idle_OnEnterState_Idle(EnterStateEvent_Idle args)
    {
        EnterState();
    }

    private void NoInputEvent_OnNoInput(bool isNoInput)
    {
        this.isNoInput = isNoInput;
    }

    public void StateUpdate()
    {
        idleTime -= Time.deltaTime;

        if (idleTime <= 0)
        {
            ExitState();

            if (GetReminderSpeech(out SpeechSO speech))
            {
                enterStateEvent_Speech.CallEnterStateSpeech(speech);
            }
            else if (isNoInput)
            {
                ExitState();

                enterStateEvent_Sleep.CallOnEnterStateSleep();
            }
            else
            {
                ExitState();

                enterStateEvent_Patrol.CallOnEnterStatePatrol();
            }
        }
    }

    public void LeftClick(Vector3 position)
    {
        tryHitEvent.CallTryHit(position);
    }

    public void StartLeftDrag()
    {
        ExitState();

        enterStateEvent_Drag.CallOnEnterStateDrag();
    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {

    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {

    }

    public void GetHit(Vector3 position, bool isCritical)
    {
        ExitState();

        enterStateEvent_Stagger.CallOnEnterStateStagger();
    }

    public void Stun()
    {
        ExitState();

        enterStateEvent_Stun.CallOnEnterStateStun();
    }

    public void TryAttack(Vector3 targetPosition)
    {
        ExitState();

        enterStateEvent_Attack.CallEnterStateAttack(targetPosition);
    }

    private void EnterState()
    {
        //Debug.Log("idle");

        idleTime = Random.Range(idleTimeMin, idleTimeMax);

        pet.SetState(this);
    }

    private void ExitState()
    {

    }

    private bool GetReminderSpeech(out SpeechSO remindSpeech)
    {
        remindSpeech = null;

        foreach (ReminderSpeech reminderSpeech in reminderSpeechArray)
        {
            if (reminderSpeech.GetRemind(out remindSpeech))
                return true;
        }

        return false;
    }
}
