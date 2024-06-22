using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pet))]
[RequireComponent(typeof(EnterStateEvent_Idle))]
[RequireComponent(typeof(EnterStateEvent_Stun))]
[DisallowMultipleComponent]
public class State_Stun : MonoBehaviour, IPetState
{
    private Pet pet;

    // Events
    private EnterStateEvent_Idle enterStateEvent_Idle;
    private EnterStateEvent_Stun enterStateEvent_Stun;

    // Stun
    [SerializeField] private float stunTime;
    private float stunTimeCount = 0;

    private void Awake()
    {
        // Cache required components
        pet = GetComponent<Pet>();
        enterStateEvent_Idle = GetComponent<EnterStateEvent_Idle>();
        enterStateEvent_Stun = GetComponent<EnterStateEvent_Stun>();
    }

    private void OnEnable()
    {
        enterStateEvent_Stun.OnEnterStateStun += EnterStateEvent_Stun_OnEnterStateStun;
    }

    private void OnDisable()
    {
        enterStateEvent_Stun.OnEnterStateStun -= EnterStateEvent_Stun_OnEnterStateStun;
    }

    private void EnterStateEvent_Stun_OnEnterStateStun(EnterStateEvent_Stun obj)
    {
        EnterState();
    }

    public void StateUpdate()
    {
        stunTimeCount += Time.deltaTime;

        if(stunTimeCount >= stunTime)
        {
            ExitState();

            enterStateEvent_Idle.CallOnEnterStateIdle();
        }
    }

    public void LeftClick(Vector3 position)
    {

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

    public void GetHit(Vector3 position,bool isCritical)
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
        //Debug.Log("stun");

        stunTimeCount = 0;

        pet.SetState(this);
    }

    private void ExitState()
    {

    }
}
