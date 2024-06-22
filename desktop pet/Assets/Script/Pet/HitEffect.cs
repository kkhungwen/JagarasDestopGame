using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] GetHitEvent getHitEvent;
    [SerializeField] SpriteFlashEffect spriteFlashEffect;
    [SerializeField] ObjectPoolManager objectPoolManager;
    [SerializeField] GameObject spriteAnimtorPoolKey;


    [Space(10f)]
    [Header("SPEECH BUBBLE")]
    [SerializeField] int speechBubbleChancePercentage;
    [SerializeField] SpeechBubbleEvent speechBubbleEvent;
    [SerializeField] SpeechArraySO speechArray;
    [SerializeField] float speechCooldown = 1f;
    private float speechCooldownCount = 0;

    [Space(10f)]
    [Header("WEAPON HIT ANMIMATION")]
    [SerializeField] SpriteAnimationSO weaponHitAnimationSO;

    [Space(10f)]
    [Header("WEAPON HIT PARTICAL")]
    [SerializeField] GameObject weaponHitParticalPoolKey;

    [Space(10f)]
    [Header("CRITICAL HIT ANMIMATION")]
    [SerializeField] SpriteAnimationSO criticalAnimationSO;
    [SerializeField] float criticalAnimationAngleSpread;

    [Space(10f)]
    [Header("CRITICAL HIT PARTICAL")]
    [SerializeField] GameObject criticalHitParticalPoolKey;

    [Space(10f)]
    [Header("DAMAGE POP UP")]
    [SerializeField] GameObject popUpTextPoolKey;
    [SerializeField] float popUpLifeTime;
    [SerializeField] float textSize;
    [SerializeField] float criticalTextSize;
    [SerializeField] Color textColor;
    [SerializeField] Color criticalTextColor;
    [SerializeField] float spreadRadious;

    private void OnEnable()
    {
        getHitEvent.OnGetHit += GetHitEvent_OnGetHit;
    }
    private void OnDisable()
    {
        getHitEvent.OnGetHit -= GetHitEvent_OnGetHit;
    }

    private void Update()
    {
        speechCooldownCount -= Time.deltaTime;
    }

    private void GetHitEvent_OnGetHit(GetHitEvent getHitEvent, GetHitEventArgs getHitEventArgs)
    {
        PlayWeaponHitAnimation(getHitEventArgs.worldPosition);
        WeaponHitPartical(getHitEventArgs.worldPosition);
        SpriteFlashEffect();
        DamagePopUp(getHitEventArgs.worldPosition, getHitEventArgs.damage, getHitEventArgs.isCritical);
        SpeechBubble(getHitEventArgs.isCritical);

        if (getHitEventArgs.isCritical)
        {
            PlayCriticalAnimation(getHitEventArgs.worldPosition);
            CriticalHitPartical(getHitEventArgs.worldPosition);
        }
    }

    private void PlayWeaponHitAnimation(Vector3 worldPosition)
    {
        float animationAngle = Random.Range(0f, 360f);

        SpriteAnimatorPoolObject spriteAnimatorPoolObject = (SpriteAnimatorPoolObject)objectPoolManager.GetComponentFromPool(spriteAnimtorPoolKey);
        spriteAnimatorPoolObject.SetPositionAngle(worldPosition, animationAngle);
        spriteAnimatorPoolObject.PlayAnimation(weaponHitAnimationSO, spriteAnimtorPoolKey, objectPoolManager);
    }

    private void WeaponHitPartical(Vector3 worldPosition)
    {
        ParticalPoolObject particalPoolObject = (ParticalPoolObject)objectPoolManager.GetComponentFromPool(weaponHitParticalPoolKey);

        particalPoolObject.Emit(objectPoolManager, weaponHitParticalPoolKey, worldPosition);
    }

    private void CriticalHitPartical(Vector3 worldPosition)
    {
        ParticalPoolObject particalPoolObject = (ParticalPoolObject)objectPoolManager.GetComponentFromPool(criticalHitParticalPoolKey);

        particalPoolObject.Emit(objectPoolManager, criticalHitParticalPoolKey, worldPosition);
    }

    private void PlayCriticalAnimation(Vector3 worldPosition)
    {
        float animationAngle = Random.Range(90 - criticalAnimationAngleSpread, 90 + criticalAnimationAngleSpread);

        SpriteAnimatorPoolObject spriteAnimatorPoolObject = (SpriteAnimatorPoolObject)objectPoolManager.GetComponentFromPool(spriteAnimtorPoolKey);
        spriteAnimatorPoolObject.SetPositionAngle(worldPosition, animationAngle);
        spriteAnimatorPoolObject.PlayAnimation(criticalAnimationSO, spriteAnimtorPoolKey, objectPoolManager);
    }

    private void SpriteFlashEffect()
    {
        spriteFlashEffect.Flash();
    }

    private void DamagePopUp(Vector3 position, int damage, bool isCritical)
    {
        Vector3 randomSpreadPositon = position + (Vector3)Random.insideUnitCircle * spreadRadious;

        PopUpTextPoolObject popUpTextPoolObject = (PopUpTextPoolObject)objectPoolManager.GetComponentFromPool(popUpTextPoolKey);

        if (isCritical)
            popUpTextPoolObject.PopText(randomSpreadPositon, popUpLifeTime, damage.ToString(), criticalTextSize, criticalTextColor, popUpTextPoolKey, objectPoolManager);
        else
            popUpTextPoolObject.PopText(randomSpreadPositon, popUpLifeTime, damage.ToString(), textSize, textColor, popUpTextPoolKey, objectPoolManager);
    }

    private void SpeechBubble(bool isCritical)
    {
        if (!isCritical)
            return;

        if (speechCooldownCount > 0)
            return;

        speechBubbleEvent.CallOnSpeechBubble(speechArray.GetRandomSpeech(), false, true);
        speechCooldownCount = speechCooldown;

        /*
        int chance = Random.Range(0, 100);
        if (chance < speechBubbleChancePercentage)
            speechBubbleEvent.CallOnSpeechBubble(speechArray.GetRandomSpeech(), false, true);
        */
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(getHitEvent), getHitEvent);
        HelperUtils.ValidateCheckNullValue(this, nameof(objectPoolManager), objectPoolManager);
        HelperUtils.ValidateCheckNullValue(this, nameof(spriteAnimtorPoolKey), spriteAnimtorPoolKey);
        HelperUtils.ValidateCheckNullValue(this, nameof(weaponHitAnimationSO), weaponHitAnimationSO);
        HelperUtils.ValidateCheckNullValue(this, nameof(criticalAnimationSO), criticalAnimationSO);
        HelperUtils.ValidateCheckNullValue(this, nameof(weaponHitParticalPoolKey), weaponHitParticalPoolKey);
        HelperUtils.ValidateCheckNullValue(this, nameof(spriteFlashEffect), spriteFlashEffect);
        HelperUtils.ValidateCheckNullValue(this, nameof(popUpTextPoolKey), popUpTextPoolKey);
        HelperUtils.ValidateCheckNullValue(this, nameof(speechBubbleEvent), speechBubbleEvent);
        HelperUtils.ValidateCheckNullValue(this, nameof(speechArray), speechArray);
    }
#endif
}
