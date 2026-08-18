using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_Effects entityEffects;
    private StatusIconBarUI statusIconBarUI;

    private ElementType currentElement;

    [Header("Chill/Ice Element")]
    private int slowStacks;
    private float slowExpireTime;
    private float freezeExpireTime;
    [SerializeField] private Sprite iceSprite;

    private bool isSlowed;
    private bool isFrozen;

    [Header("Shock/Lightning Effect")]
    [SerializeField] private float currentCharged;
    [SerializeField] private Sprite lightningSprite;

    private Coroutine elementEffectCo;
    private bool isThunder;

    [Header("Burn/Fire Effect")]
    [SerializeField] private Sprite fireSprite;

    private bool isFire;


    private void Awake()
    {
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
    public void ApplyStatusEffect(ElementType element, ElementalEffectData effectData, StatusIconBarUI iconBar, Entity entity)
    {
        this.entity = entity;
        statusIconBarUI = iconBar;

        if (element == ElementType.Ice)
            ApplyChilledEffect(effectData);

        if (element == ElementType.Fire && CanBeApplyEffect(ElementType.Fire))
            ApplyBurnEffect(effectData);

        if (element == ElementType.Lightning && CanBeApplyEffect(ElementType.Lightning))
            ApplyLightningEffect(effectData);
    }

    private void ApplyChilledEffect(ElementalEffectData effectData)
    {
        if (isFrozen)
            return;

        statusIconBarUI.AddOrRefreshEffect(currentElement.ToString(), iceSprite, effectData.chillDuration, entity, slowStacks);

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

        statusIconBarUI.AddOrRefreshEffect(currentElement.ToString(), iceSprite, freezeDuration, entity);

        entityEffects.CreateIceActive(transform, freezeDuration);
        entity.ResetMoveSpeedMultiplier();

        isFrozen = true;

        freezeExpireTime = Time.time + freezeDuration;
    }

    private void ApplyBurnEffect(ElementalEffectData effectData)
    {
        if (elementEffectCo != null)
            StopCoroutine(elementEffectCo);

        elementEffectCo = StartCoroutine(HandleBurnEffectCo(effectData));
    }

    private IEnumerator HandleBurnEffectCo(ElementalEffectData effectData)
    {
        SetElement(ElementType.Fire);
        entity.entityEffects.CreateFire(transform, effectData.burnDuration);

        int ticksPerSecond = 2;
        int tickCount = Mathf.Max(1, Mathf.RoundToInt(ticksPerSecond * effectData.burnDuration));

        float tickInterval = effectData.burnDuration / tickCount;

        float totalDamage = effectData.burnDamage;

        int baseDamagePerTick = Mathf.RoundToInt(totalDamage / tickCount);

        for (int i = tickCount - 1; i >= 0; i--)
        {
            entity.entityHealth.DecreaseHealth(Mathf.RoundToInt(baseDamagePerTick));
            statusIconBarUI.AddOrRefreshEffect(currentElement.ToString(), fireSprite, effectData.burnDuration, entity, i);
            yield return new WaitForSeconds(tickInterval);
        }

        SetElement(ElementType.None);
    }

    private void ApplyLightningEffect(ElementalEffectData effectData)
    {
        if (elementEffectCo != null)
            StopCoroutine(elementEffectCo);

        elementEffectCo = StartCoroutine(HandleLightningEffectCo(effectData));
    }

    private IEnumerator HandleLightningEffectCo(ElementalEffectData effectData)
    {
        float maxCharge = effectData.shockThreshold;

        SetElement(ElementType.Lightning);
        currentCharged += effectData.shockCharge;


        if (currentCharged >= maxCharge)
        {
            isThunder = true;

            entityEffects.CreateThunder(transform, effectData.lightningThunderDuration);

            currentCharged = 0f;

            statusIconBarUI.AddOrRefreshEffect(currentElement.ToString(), lightningSprite, effectData.lightningThunderDuration, entity);

            yield return new WaitForSeconds(effectData.lightningThunderDuration);

            isThunder = false;
        }

        statusIconBarUI.AddOrRefreshEffect(currentElement.ToString(), lightningSprite, effectData.lightningThunderDuration, entity, (int)currentCharged);

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