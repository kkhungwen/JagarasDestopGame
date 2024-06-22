using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(NoInputEvent))]
[RequireComponent(typeof(EnterStateEvent_Sleep))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
public class State_Sleep : MonoBehaviour, IPetState
{
    private Pet pet;

    private NoInputEvent noInputEvent;
    private EnterStateEvent_Sleep enterStateEvent_Sleep;
    private EnterStateEvent_Idle enterStateEvent_Idle;

    private bool isNoInput = false;


    private void Awake()
    {
        pet = GetComponent<Pet>();

        noInputEvent = GetComponent<NoInputEvent>();
        enterStateEvent_Sleep = GetComponent<EnterStateEvent_Sleep>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
    }

    private void OnEnable()
    {
        noInputEvent.OnNoInput += NoInputEvent_OnNoInput;
        enterStateEvent_Sleep.OnEnterState_Sleep += EnterStateEvent_Sleep_OnEnterState_Sleep;
    }
    private void OnDisable()
    {
        noInputEvent.OnNoInput -= NoInputEvent_OnNoInput;
        enterStateEvent_Sleep.OnEnterState_Sleep -= EnterStateEvent_Sleep_OnEnterState_Sleep;
    }

    private void NoInputEvent_OnNoInput(bool isNoInput)
    {
        this.isNoInput = isNoInput;
    }

    private void EnterStateEvent_Sleep_OnEnterState_Sleep(EnterStateEvent_Sleep enterStateEvent_Sleep)
    {
        EnterState();
    }


    public void StateUpdate()
    {
        if (!isNoInput)
        {
            ExitState();

            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    public void GetHit(Vector3 position, bool isCritical)
    {

    }

    public void LeftClick(Vector3 position)
    {

    }

    public void LeftDrag(Vector3 position, Vector3 positionRelative)
    {
  
    }

    public void LeftMouseUp(Vector3 position, Vector3 positionRelative)
    {

    }

    public void StartLeftDrag()
    {

    }

    public void Stun()
    {

    }

    public void TryAttack(Vector3 targetPosition)
    {
 
    }

    private void EnterState()
    {
        pet.SetState(this);
    }

    private void ExitState()
    {

    }
}
