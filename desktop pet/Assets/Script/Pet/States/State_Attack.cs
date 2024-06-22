using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(ActiveAttackEvent))]
[RequireComponent(typeof(SetFaceDirectionEvent))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(EnterStateEvent_Attack))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
public class State_Attack : MonoBehaviour, IPetState
{
    private Pet pet;
    private MovementByVelocityEvent movementByVelocityEvent;
    private SetFaceDirectionEvent setFaceDirectionEvent;
    private TryHitEvent tryHitEvent;
    private ActiveAttackEvent activeAttackEvent;
    private EnterStateEvent_Attack enterStateEvent_Attack;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    [Space(10f)]
    [Header("ATTACK PARAMETERS")]
    [SerializeField] private float attackStartTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackSpeed;
    private float attackTimeCount;
    private Vector2 attackDirection;
    private bool isActiveAttckCalled;

    private void Awake()
    {
        pet = GetComponent<Pet>();
        activeAttackEvent = GetComponent<ActiveAttackEvent>();
        setFaceDirectionEvent = GetComponent<SetFaceDirectionEvent>();
        tryHitEvent = GetComponent<TryHitEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        enterStateEvent_Attack = GetComponent<EnterStateEvent_Attack>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
    }

    private void OnEnable()
    {
        enterStateEvent_Attack.OnEnterStateAttack += EnterStateEvent_Attack_OnEnterStateAttack;
    }
    private void OnDisable()
    {
        enterStateEvent_Attack.OnEnterStateAttack -= EnterStateEvent_Attack_OnEnterStateAttack;
    }

    public void StateUpdate()
    {
        attackTimeCount += Time.deltaTime;

        if (attackTimeCount >= attackTime)
        {
            ExitState();
            enterStateEvent_Idle.CallOnEnterStateIdle();
            return;
        }

        if (attackTimeCount < attackStartTime)
        {
            return;
        }

        if (!isActiveAttckCalled)
        {
            // Set AnimatePet animation to attack active 
            activeAttackEvent.CallOnActiveAttack();
            isActiveAttckCalled = true;
        }

        movementByVelocityEvent.CallMovementByVelocityEvent(attackDirection, attackSpeed);
    }

    private void EnterStateEvent_Attack_OnEnterStateAttack(EnterStateEvent_Attack enterStateEvent_Attack, EnterStateEventArgs_Attack enterStateEventArgs_Attack)
    {
        EnterState(enterStateEventArgs_Attack.targetPosition);
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

    private void EnterState(Vector3 targetPosition)
    {
        attackTimeCount = 0;
        isActiveAttckCalled = false;

        SetAttckVelocity(targetPosition);

        // Update animate pet face direction
        setFaceDirectionEvent.CallOnSetFaceDirection(attackDirection);

        pet.SetState(this);
    }

    private void ExitState()
    {
        movementByVelocityEvent.CallMovementByVelocityEvent(Vector2.zero, 0);
    }

    private void SetAttckVelocity(Vector3 targetDirection)
    {
        if (targetDirection.x > transform.position.x)
        {
            attackDirection = Vector2.right;
        }
        else
        {
            attackDirection = Vector2.left;
        }
    }
}
