using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_Effects entityEffects;

    [Header("Elemental Info")]
    [SerializeField] private ElementType currentElement;

    private int slowStacks;
    private float slowExpireTime;
    private float freezeExpireTime;

    private bool isSlowed;
    private bool isFrozen;

    [Header("Lightning Effect")]
    [SerializeField] private float currentCharged;
    private bool isThunder;
    private Coroutine elementEffectCo;


    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityEffects = GetComponent<Entity_Effects>();
    }

    private void Start()
    {
        currentElement = ElementType.None;
    }

    private void Update()
    {
        if (isFrozen && Time.time >= freezeExpireTime)
        {
            RemoveFreeze();
        }

        if (isSlowed && Time.time >= slowExpireTime)
        {
            RemoveSlow();
        }
    }
    public void ApplyStatusEffect(ElementType element, ElementalEffectData effectData)
    {
        if (element == ElementType.Ice)
            ApplyChilledEffect(effectData);

        //if (element == ElementType.Fire && CanBeApplyEffect(ElementType.Fire))
        //    ApplyBurnedEffect(effectData.burnDuration, effectData.burnDamage);

        if (element == ElementType.Lightning && CanBeApplyEffect(ElementType.Lightning))
            ApplyLightningEffect(effectData);
    }

    private void ApplyChilledEffect(ElementalEffectData effectData)
    {
        if (isFrozen)
            return;

        slowStacks += Mathf.Max(1, effectData.chillStacksPerHit);
        slowStacks = Mathf.Min(slowStacks, effectData.freezeThreshold);

        slowExpireTime = Time.time + effectData.chillDuration;

        if (slowStacks >= effectData.freezeThreshold)
        {
            ApplyFreezeEffect(effectData.freezeDuration);
            return;
        }

        isSlowed = true;

        float slowPercent = slowStacks * effectData.chillPercentPerStack;

        entity.SetMoveSpeedMultiplier(Mathf.Clamp01(1f - slowPercent));
        entity.SlowDown(effectData.chillDuration);
    }

    private void ApplyFreezeEffect(float freezeDuration)
    {
        slowStacks = 0;
        isSlowed = false;

        entityEffects.CreateIceActive(transform, freezeDuration);
        entity.ResetMoveSpeedMultiplier();

        isFrozen = true;

        freezeExpireTime = Time.time + freezeDuration;
    }

    private void ApplyLightningEffect(ElementalEffectData effectData)
    {
        if (elementEffectCo != null)
            StopCoroutine(elementEffectCo);

        elementEffectCo = StartCoroutine(LightningEffectCo(effectData));
    }

    private IEnumerator LightningEffectCo(ElementalEffectData effectData)
    {
        float maxCharge = 1;

        SetElement(ElementType.Lightning);
        currentCharged += effectData.shockCharge;

        if (currentCharged >= maxCharge)
        {
            isThunder = true;
            entityEffects.CreateThunder(transform, effectData.lightningThunderDuration);
            currentCharged = 0f;
            yield return new WaitForSeconds(effectData.lightningThunderDuration);
            isThunder = false;
        }
        yield return new WaitForSeconds(effectData.shockDuration);

        SetElement(ElementType.None);
    }

    private void RemoveSlow()
    {
        slowStacks = 0;
        isSlowed = false;

        SetElement(ElementType.None);
        entity.ResetMoveSpeedMultiplier();
    }

    private void RemoveFreeze()
    {
        isFrozen = false;
        entityEffects.CreateIceEnd(transform, 1f);
        SetElement(ElementType.None);
    }

    public bool IsSlowed() => isSlowed;


    public bool IsFrozen() => isFrozen;

    public bool IsThunder() => isThunder;

    public int GetSlowStacks()
    {
        return slowStacks;
    }

    public void SetElement(ElementType element)
    {
        currentElement = element;
    }

    public bool CanBeApplyEffect(ElementType element)
    {
        if (currentElement == element)
            return false;

        return currentElement == ElementType.None;
    }
}