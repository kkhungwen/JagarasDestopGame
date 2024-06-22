using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackDetectEvent))]
public class AttackDetector : MonoBehaviour
{
    private AttackDetectEvent attackDetectEvent;
    [SerializeField] private float detectCooldown;
    [SerializeField] private BoxCollider2D detectCollider;
    private float detectCooldownCount;

    private void Awake()
    {
        attackDetectEvent = GetComponent<AttackDetectEvent>();

        detectCooldownCount = detectCooldown;
    }

    private void Update()
    {
        detectCooldownCount -= Time.deltaTime;

        if (detectCooldownCount > 0)
            return;

        Vector3 target = HelperUtils.GetMouseWorldPosition();

        if (detectCollider.OverlapPoint(target))
        {
            attackDetectEvent.CallOnAttackDetect(target);

            detectCooldownCount = detectCooldown;
        }
    }

#if UNITY_EDITOR
    #region Validation
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(detectCollider), detectCollider);
    }
    #endregion
#endif
}
