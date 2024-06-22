using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TryHitEvent))]
[RequireComponent(typeof(GetHitEvent))]
[DisallowMultipleComponent]
public class Hit : MonoBehaviour
{
    // Events
    private TryHitEvent tryHitEvent;
    private GetHitEvent getHitEvent;

    [Space(10f)]
    [Header("DAMAGE")]
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float criticalMultiplier;


    [Space(10f)]
    [Header("CONFIGURABLES")]
    // Hit cooldown
    [SerializeField] private float hitCooldown;
    private float hitColldownCount = 0;

    // Critical
    [SerializeField] private float criticalChancePercentage;

    private void Awake()
    {
        tryHitEvent = GetComponent<TryHitEvent>();
        getHitEvent = GetComponent<GetHitEvent>();
    }

    private void OnEnable()
    {
        tryHitEvent.OnTryHit += TryHitEvent_OnTryHit;
    }

    private void OnDisable()
    {
        tryHitEvent.OnTryHit -= TryHitEvent_OnTryHit;
    }

    private void Update()
    {
        if (hitColldownCount > 0)
            hitColldownCount -= Time.deltaTime;
    }

    private void TryHitEvent_OnTryHit(TryHitEvent tryHitEvent, TryHitEventArgs tryHitEventArgs)
    {
        TryHit(tryHitEventArgs.worldPosition);
    }

    private void TryHit(Vector3 worldPosition)
    {
        if (hitColldownCount > 0)
            return;

        int damage = Random.Range(minDamage, maxDamage + 1);

        if (isCritical())
        {
            damage = (int)(damage * criticalMultiplier);
            getHitEvent.CallGetHit(worldPosition, true, damage);
        }
        else
        {
            getHitEvent.CallGetHit(worldPosition, false, damage);
        }
            
        hitColldownCount = hitCooldown;
    }

    private bool isCritical()
    {
        float criticalRoll = Random.Range(0, 100);
        return criticalRoll <= criticalChancePercentage;
    }
}
