using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[DisallowMultipleComponent]
public class State_Stagger : MonoBehaviour, IPetState
{
    private Pet pet;

    // Events
    private TryHitEvent tryHitEvent;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    // Stagger
    [SerializeField] private float staggerTime;
    private float staggerTimeCount;

    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        tryHitEvent = GetComponent<TryHitEvent>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
    }

    private void OnEnable()
    {
        enterStateEvent_Stagger.OnEnterStateStagger += EnterStateEvent_Stagger_OnEnterStateStagger;
    }

    private void OnDisable()
    {
        enterStateEvent_Stagger.OnEnterStateStagger -= EnterStateEvent_Stagger_OnEnterStateStagger;
    }

    private void EnterStateEvent_Stagger_OnEnterStateStagger(EnterStateEvent_Stagger args)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        staggerTimeCount += Time.deltaTime;

        if(staggerTimeCount >= staggerTime)
        {
            ExitState();

            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    public void LeftClick(Vector3 position)
    {
        tryHitEvent.CallTryHit(position);
    }

    public void StartLeftDrag()
    {

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

        EnterState();
    }

    public void Stun()
    {
        ExitState();

        enterStateEvent_Stun.CallOnEnterStateStun();
    }

    public void TryAttack(Vector3 targetPosition)
    {

    }

    private void EnterState()
    {
        //Debug.Log("stagger");

        staggerTimeCount = 0;

        pet.SetState(this);
    }

    private void ExitState()
    {

    }
}
