using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[DisallowMultipleComponent]
public class State_Fall : MonoBehaviour, IPetState
{
    // Fall parameters
    [SerializeField] private float lowerYBoundSoftRange = 0.05f;

    [Space(10f)]
    [Header("BOUNCE")]
    // BounceParameters
    [SerializeField] private float bounceXStrength = 1;
    [SerializeField] private float bounceXRange = 1;
    [SerializeField] private float minBounceVelocityY = 1f;
    [SerializeField] private float maxBounceVelocityY = 1f;
    [SerializeField] private float bounceSpeed = 1;
    [SerializeField] private float criticalModifier;
    [SerializeField] private CircleCollider2D bounceCollider;
    [SerializeField] private BoxCollider2D originalCollider;
    [SerializeField] private PhysicsMaterial2D bounceMaterial;
    [SerializeField] private PhysicsMaterial2D originalMaterial;
    [SerializeField] private Rotate spriteRotate;
    [SerializeField] private float rotateModifier;

    private Pet pet;
    private Rigidbody2D petRigidBody;

    // Events
    private TryHitEvent tryHitEvent;
    private MovementByVelocityEvent movementByVelocityEvent;
    private EnterStateEvent_Fall enterStateEvent_Fall;
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    // Screen boundaries world position
    private float workingspaceLowerBound;

    private void Awake()
    {
        // // Cache required components
        pet = GetComponent<Pet>();
        petRigidBody = GetComponent<Rigidbody2D>();
        tryHitEvent = GetComponent<TryHitEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();

        // Set screen boundaries world position
        workingspaceLowerBound = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition().y;

        // Initialize
        bounceCollider.enabled = false;
    }

    private void OnEnable()
    {
        enterStateEvent_Fall.OnEnterState_Fall += EnterStateEvent_Fall_OnEnterState_Fall;
    }

    private void OnDisable()
    {
        enterStateEvent_Fall.OnEnterState_Fall -= EnterStateEvent_Fall_OnEnterState_Fall;
    }

    private void EnterStateEvent_Fall_OnEnterState_Fall(EnterStateEvent_Fall args)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        if (transform.position.y <= workingspaceLowerBound + lowerYBoundSoftRange)
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
        BounceUp(position, isCritical);
    }

    public void Stun()
    {
        ExitState();

        enterStateEvent_Stun.CallOnEnterStateStun();
    }

    public void TryAttack(Vector3 targetPosition)
    {

    }

    private void BounceUp(Vector3 position, bool isCritical)
    {
        float bounceVelocityX;

        /*
        bounceVelocityX = transform.position.x - position.x;
        bounceVelocityX = Mathf.Clamp(bounceVelocityX, -bounceXRange, bounceXRange);
        bounceVelocityX = bounceVelocityX * bounceXStrength;
        */

        bounceVelocityX = Random.Range(-bounceXRange, bounceXRange);

        float bounceVelocityY = Random.Range(minBounceVelocityY, maxBounceVelocityY);

        if (isCritical)
        {
            movementByVelocityEvent.CallMovementByVelocityEvent(new Vector2(bounceVelocityX, bounceVelocityY), bounceSpeed * criticalModifier);
            spriteRotate.StartRotate(-bounceVelocityX * criticalModifier * rotateModifier);
        }
        else
        {
            movementByVelocityEvent.CallMovementByVelocityEvent(new Vector2(bounceVelocityX, bounceVelocityY), bounceSpeed);
            spriteRotate.StartRotate(-bounceVelocityX * rotateModifier);
        }
    }

    private void EnterState()
    {
        //Debug.Log("fall");

        bounceCollider.enabled = true;
        originalCollider.enabled = false;

        petRigidBody.sharedMaterial = bounceMaterial;

        pet.SetState(this);
    }

    private void ExitState()
    {
        spriteRotate.StopRotate();
        originalCollider.enabled = true;
        bounceCollider.enabled = false;

        petRigidBody.sharedMaterial = originalMaterial;
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(bounceCollider), bounceCollider);
        HelperUtils.ValidateCheckNullValue(this, nameof(originalCollider), originalCollider);
        HelperUtils.ValidateCheckNullValue(this, nameof(bounceMaterial), bounceMaterial);
        HelperUtils.ValidateCheckNullValue(this, nameof(originalCollider), originalCollider);
    }
    #endregion
#endif
}
