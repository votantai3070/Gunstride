using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;

    [Header("Elemental Info")]
    [SerializeField] private ElementType currentElement;

    private int slowStacks;
    private float slowExpireTime;
    private float freezeExpireTime;

    private bool isSlowed;
    private bool isFrozen;


    private void Awake()
    {
        entity = GetComponent<Entity>();
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

        //if (element == ElementType.Lightning && CanBeApplyEffect(ElementType.Lightning))
        //    ApplyShockEffect(effectData.shockDuration, effectData.shockDamage, effectData.shockCharge);
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

        entity.ResetMoveSpeedMultiplier();

        isFrozen = true;

        freezeExpireTime = Time.time + freezeDuration;
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

        SetElement(ElementType.None);
    }

    public bool IsSlowed()
    {
        return isSlowed;
    }

    public bool IsFrozen()
    {
        return isFrozen;
    }

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