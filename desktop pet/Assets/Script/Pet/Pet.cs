using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(AttackDetectEvent))]
[RequireComponent(typeof(GetHitEvent))]
[RequireComponent(typeof(BreakEvent))]
[DisallowMultipleComponent]
public class Pet : MonoBehaviour
{
    [HideInInspector] public IPetState currentState;

    // Events
    private AttackDetectEvent attackDetectEvent;
    private GetHitEvent getHitEvent;
    private BreakEvent breakEvent;
    private EnterStateEvent_Idle enterStateEvent_Idle;

    private void Awake()
    {
        // Cache required components
        attackDetectEvent = GetComponent<AttackDetectEvent>();
        getHitEvent = GetComponent<GetHitEvent>();
        breakEvent = GetComponent<BreakEvent>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
    }

    private void OnEnable()
    {
        attackDetectEvent.OnAttackDetect += AttackDetectEvent_OnAttackDetect;
        getHitEvent.OnGetHit += GetHitEvent_OnGetHit;
        breakEvent.OnBreak += BreakEvent_OnBreak;
    }

    private void OnDisable()
    {
        attackDetectEvent.OnAttackDetect -= AttackDetectEvent_OnAttackDetect;
        getHitEvent.OnGetHit -= GetHitEvent_OnGetHit;
        breakEvent.OnBreak -= BreakEvent_OnBreak;
    }

    private void Start()
    {
        // Start as idle
        enterStateEvent_Idle.CallOnEnterStateIdle();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.StateUpdate();
    }

    private void AttackDetectEvent_OnAttackDetect(AttackDetectEvent attackDetectEvent, AttackDetectEventArgs attackDetectEventArgs)
    {
        currentState.TryAttack(attackDetectEventArgs.targetPosition);
    }

    private void GetHitEvent_OnGetHit(GetHitEvent getHitEvent, GetHitEventArgs getHitEventArgs)
    {
        currentState.GetHit(getHitEventArgs.worldPosition, getHitEventArgs.isCritical);
    }

    private void BreakEvent_OnBreak(BreakEvent breakEvent)
    {
        currentState.Stun();
    }

    public void SetState(IPetState stateToSet)
    {
        currentState = stateToSet;
    }

    public void LeftClick(Vector3 position)
    {
        currentState.LeftClick(position);
    }

    public void StartLeftDrag()
    {
        currentState.StartLeftDrag();
    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {
        currentState.LeftDrag(position, positionRelative);
    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {
        currentState.LeftMouseUp(position, positionRelative);
    }
}
