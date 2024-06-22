using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(EnterStateEvent_Drag))]
[RequireComponent(typeof(EnterStateEvent_Fall))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[DisallowMultipleComponent]
public class State_Drag : MonoBehaviour, IPetState
{
    private Pet pet;

    // Events
    private MovementByVelocityEvent movementByVelocityEvent;
    private EnterStateEvent_Drag enterStateEvent_Drag;
    private EnterStateEvent_Fall enterStateEvent_Fall;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    // Configurables
    [SerializeField] private float dragStrength;
    [SerializeField] private float throwStrength;
    [SerializeField] private float maxThrowSpeed;
    [SerializeField] private float followMouseSoftRange;



    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        enterStateEvent_Drag = GetComponent<EnterStateEvent_Drag>();
        enterStateEvent_Fall = GetComponent<EnterStateEvent_Fall>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
    }

    private void OnEnable()
    {
        enterStateEvent_Drag.OnEnterState_Drag += EnterStateEvent_Drag_OnEnterState_Drag;
    }
    private void OnDisable()
    {
        enterStateEvent_Drag.OnEnterState_Drag -= EnterStateEvent_Drag_OnEnterState_Drag;
    }

    private void EnterStateEvent_Drag_OnEnterState_Drag(EnterStateEvent_Drag args)
    {
        EnterState();
    }

    public void StateUpdate()
    {

    }

    public void LeftClick(Vector3 position)
    {

    }


    public void StartLeftDrag()
    {

    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {
        MoveToMousePosition(position);
    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {
        Throw(position);

        enterStateEvent_Fall.CallOnEnterStateFall();

        ExitState();
    }

    public void GetHit(Vector3 position, bool isCritical)
    {

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
        //Debug.Log("drag");

        pet.SetState(this);
    }

    private void ExitState()
    {

    }

    private void MoveToMousePosition(Vector3 mousePosition)
    {
        float mouseDistance = Vector2.Distance(mousePosition, transform.position);
        if (mouseDistance < followMouseSoftRange)
        {
            transform.position = mousePosition;
            return;
        }

        Vector2 direction = (mousePosition - transform.position).normalized;
        float speed = Vector2.Distance(mousePosition, transform.position) * dragStrength;

        movementByVelocityEvent.CallMovementByVelocityEvent(direction, speed);
    }

    private void Throw(Vector3 mousePosition)
    {
        Vector2 direction = (mousePosition - transform.position).normalized;
        float speed = Vector2.Distance(mousePosition, transform.position) * throwStrength;
        if (speed > maxThrowSpeed) speed = maxThrowSpeed;

        movementByVelocityEvent.CallMovementByVelocityEvent(direction, speed);
    }
}
