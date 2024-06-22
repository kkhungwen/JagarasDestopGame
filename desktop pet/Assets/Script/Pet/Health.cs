using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GetHitEvent))]
[RequireComponent(typeof(BreakEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    // Events
    private GetHitEvent getHitEvent;
    private BreakEvent breakEvent;

    [Space(10f)]
    [Header("HEALTH POINTS")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        getHitEvent = GetComponent<GetHitEvent>();
        breakEvent = GetComponent<BreakEvent>();
    }

    private void Start()
    {
        FullHealth();
    }

    private void OnEnable()
    {
        getHitEvent.OnGetHit += GetHitEvent_OnGetHit;
    }

    private void OnDisable()
    {
        getHitEvent.OnGetHit -= GetHitEvent_OnGetHit;
    }

    private void GetHitEvent_OnGetHit(GetHitEvent getHitEvent, GetHitEventArgs getHitEventArgs)
    {
        DecreaseHealth(getHitEventArgs.damage);
    }

    private void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        if(currentHealth < 0)
        {
            Break();
            FullHealth();
        }
    }

    private void FullHealth()
    {
        currentHealth = maxHealth;
    }

    private void Break()
    {
        breakEvent.CallOnBreak();
    }
}
