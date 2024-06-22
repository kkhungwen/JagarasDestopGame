using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(SetFaceDirectionEvent))]
[RequireComponent(typeof(ActiveAttackEvent))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Patrol))]
[RequireComponent(typeof(EnterStateEvent_Stagger))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(EnterStateEventArgs_Speech))]
[RequireComponent(typeof(EnterStateEvent_Sleep))]
[RequireComponent(typeof(EnterStateEvent_Attack))]
[DisallowMultipleComponent]
public class AnimatePet : MonoBehaviour
{
    [SerializeField] SpriteRenderer petSpriteRenderer;

    private Animator anima;

    private MovementByVelocityEvent movementByVelocityEvent;
    private SetFaceDirectionEvent setFaceDirectionEvent;
    private ActiveAttackEvent activeAttackEvent;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Patrol enterStateEvent_Patrol;
    private EnterStateEvent_Stagger enterStateEvent_Stagger;
    private EnterStateEvent_Stun enterStateEvent_Stun;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Fall enterStateEvent_Fall;
    private EnterStateEvent_Speech enterStateEvent_Speech;
    private EnterStateEvent_Sleep enterStateEvent_Sleep;
    private EnterStateEvent_Attack enterStateEvent_Attack;

    private void Awake()
    {
        anima = GetComponent<Animator>();

        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        setFaceDirectionEvent = GetComponent<SetFaceDirectionEvent>();
        activeAttackEvent = GetComponent<ActiveAttackEvent>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Patrol = GetComponent<EnterStateEvent_Patrol>();
        enterStateEvent_Stagger = GetComponent<EnterStateEvent_Stagger>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
        enterStateEvent_Speech = GetComponent<EnterStateEvent_Speech>();
        enterStateEvent_Sleep = GetComponent<EnterStateEvent_Sleep>();
        enterStateEvent_Attack = GetComponent<EnterStateEvent_Attack>();

        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        setFaceDirectionEvent.OnSetFaceDirection += SetFaceDirectionEvent_OnSetFaceDirection;
        activeAttackEvent.OnActiveAttack += ActiveAttackEvent_OnActiveAttack;
        enterStateEvent_Idle.OnEnterState_Idle += EnterStateEvent_Idle_OnEnterState_Idle;
        enterStateEvent_Patrol.OnEnterState_Patrol += EnterStateEvent_Patrol_OnEnterState_Patrol;
        enterStateEvent_Stagger.OnEnterStateStagger += EnterStateEvent_Stagger_OnEnterStateStagger;
        enterStateEvent_Stun.OnEnterStateStun += EnterStateEvent_Stun_OnEnterStateStun;
        enterStateEvent_Drag.OnEnterState_Drag += EnterStateEvent_Drag_OnEnterState_Drag;
        enterStateEvent_Fall.OnEnterState_Fall += EnterStateEvent_Fall_OnEnterState_Fall;
        enterStateEvent_Speech.OnEnterStateSpeech += EnterStateEvent_Speech_OnEnterStateSpeech;
        enterStateEvent_Sleep.OnEnterState_Sleep += EnterStateEvent_Sleep_OnEnterState_Sleep;
        enterStateEvent_Attack.OnEnterStateAttack += EnterStateEvent_Attack_OnEnterStateAttack;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        FlipPetSpriteRenderer(movementByVelocityEventArgs.moveDirection);
    }

    private void SetFaceDirectionEvent_OnSetFaceDirection(SetFaceDirectionEventArgs setFaceDirectionEventArgs)
    {
        FlipPetSpriteRenderer(setFaceDirectionEventArgs.direction);
    }

    private void ActiveAttackEvent_OnActiveAttack()
    {
        anima.Play(Settings.attackActive);
    }

    private void EnterStateEvent_Idle_OnEnterState_Idle(EnterStateEvent_Idle enterStateEvent_Idle)
    {
        anima.Play(Settings.idle);
    }

    private void EnterStateEvent_Patrol_OnEnterState_Patrol(EnterStateEvent_Patrol enterStateEvent_Patrol)
    {
        anima.Play(Settings.patrol);
    }
    private void EnterStateEvent_Stagger_OnEnterStateStagger(EnterStateEvent_Stagger enterStateEvent_Stagger)
    {
        anima.Play(Settings.stagger);
    }
    private void EnterStateEvent_Stun_OnEnterStateStun(EnterStateEvent_Stun enterStateEvent_Stun)
    {
        anima.Play(Settings.stunStart);
    }
    private void EnterStateEvent_Drag_OnEnterState_Drag(EnterStateEvent_Drag enterStateEvent_Drag)
    {
        anima.Play(Settings.drag);
    }
    private void EnterStateEvent_Fall_OnEnterState_Fall(EnterStateEvent_Fall enterStateEvent_Fall)
    {
        anima.Play(Settings.fall);
    }

    private void EnterStateEvent_Speech_OnEnterStateSpeech(EnterStateEvent_Speech enterStateEvent_Speech, EnterStateEventArgs_Speech stateEventArgs_Speech)
    {
        anima.Play(Settings.speech);
    }

    private void EnterStateEvent_Sleep_OnEnterState_Sleep(EnterStateEvent_Sleep enterStateEvent_Sleep)
    {
        anima.Play(Settings.sleep);
    }

    private void EnterStateEvent_Attack_OnEnterStateAttack(EnterStateEvent_Attack enterStateEvent_Attack, EnterStateEventArgs_Attack enterStateEventArgs_Attack)
    {
        anima.Play(Settings.attackStart);
    }

    private void FlipPetSpriteRenderer(Vector3 direction)
    {
        if (direction.x > 0)
            petSpriteRenderer.flipX = true;
        else if (direction.x < 0)
            petSpriteRenderer.flipX = false;
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(petSpriteRenderer), petSpriteRenderer);
    }
    #endregion
#endif
}
