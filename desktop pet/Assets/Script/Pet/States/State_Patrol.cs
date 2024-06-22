using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(EnterStateEvent_Patrol))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Attack))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[DisallowMultipleComponent]
public class State_Patrol : MonoBehaviour, IPetState
{
    private Pet pet;

    // Events
    private MovementByVelocityEvent movementByVelocityEvent;
    private TryHitEvent tryHitEvent;
    private EnterStateEvent_Patrol enterStateEvent_Patrol;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Attack enterStateEvent_Attack;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    // Movement Configurables
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 6f;
    [SerializeField] private float colliderHeight = 0.5f;

    // Movement parameters
    private float moveToTargetSoftRange = 0.1f;
    private float colliderHalfWidth = .5f;
    private Vector2 patrolTargetPosition;

    // Screen boundaries world position
    private Vector2 workingspaceLowerBoundPosition;
    private Vector2 workingspaceUpperBoundPosition;

    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        tryHitEvent = GetComponent<TryHitEvent>();
        enterStateEvent_Patrol = GetComponent<EnterStateEvent_Patrol>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Attack = GetComponent<EnterStateEvent_Attack>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();

        // Set screen boundaries world position
        workingspaceLowerBoundPosition = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition();
        workingspaceUpperBoundPosition = WindowFormsDllUtils.GetWorkingSpaceUpperBoundPosition();
    }

    private void OnEnable()
    {
        enterStateEvent_Patrol.OnEnterState_Patrol += EnterStateEvent_Patrol_OnEnterState_Patrol;
    }
    private void OnDisable()
    {
        enterStateEvent_Patrol.OnEnterState_Patrol -= EnterStateEvent_Patrol_OnEnterState_Patrol;
    }

    private void EnterStateEvent_Patrol_OnEnterState_Patrol(EnterStateEvent_Patrol args)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        if (Vector2.Distance(patrolTargetPosition, transform.position) > moveToTargetSoftRange)
        {
            MoveToPosition(patrolTargetPosition);
        }
        else
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
        //Debug.Log("patrol");

        patrolTargetPosition = GetPatrolTartgetPosition();

        pet.SetState(this);
    }

    private void ExitState()
    {
        movementByVelocityEvent.CallMovementByVelocityEvent(Vector2.zero,0);
    }

    private void MoveToPosition(Vector2 targetPosition)
    {
        Vector2 targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        movementByVelocityEvent.CallMovementByVelocityEvent(targetDirection, moveSpeed);
    }

    private Vector2 GetPatrolTartgetPosition()
    {
        Vector2 targetPosition = transform.position;

        int maxLoopCount = 20;
        int loopCount = 0;
        while (Vector2.Distance(targetPosition, transform.position) < minDistance || Vector2.Distance(targetPosition, transform.position) > maxDistance)
        {
            loopCount++;
            if (loopCount > maxLoopCount)
                break;

            targetPosition = new Vector2(Random.Range(workingspaceLowerBoundPosition.x + colliderHalfWidth, workingspaceUpperBoundPosition.x - colliderHalfWidth), workingspaceLowerBoundPosition.y + colliderHeight);
        }

        return targetPosition;
    }
}
