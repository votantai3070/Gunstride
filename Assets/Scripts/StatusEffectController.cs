using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    [Header("Chill")]
    [SerializeField] private int slowStacks;
    [SerializeField] private int freezeThreshold = 3;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowPercentPerStack = 0.2f;

    [Header("Freeze")]
    [SerializeField] private float freezeDuration = 1.5f;

    private float slowExpireTime;
    private float freezeExpireTime;

    private bool isSlowed;
    private bool isFrozen;

    private Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
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

    public void ApplySlow(int amount = 1)
    {
        if (isFrozen)
            return;

        slowStacks += Mathf.Max(1, amount);
        slowStacks = Mathf.Min(slowStacks, freezeThreshold);

        slowExpireTime = Time.time + slowDuration;

        if (slowStacks >= freezeThreshold)
        {
            ApplyFreeze();
            return;
        }

        isSlowed = true;

        float slowPercent = slowStacks * slowPercentPerStack;

        entity.SetMoveSpeedMultiplier(Mathf.Clamp01(1f - slowPercent));
        entity.SlowDown(slowDuration);
    }

    private void ApplyFreeze()
    {
        slowStacks = 0;
        isSlowed = false;

        entity.ResetMoveSpeedMultiplier();

        isFrozen = true;
        freezeExpireTime = Time.time + freezeDuration;

        entity.SetFrozen(true);
    }

    private void RemoveSlow()
    {
        slowStacks = 0;
        isSlowed = false;

        entity.ResetMoveSpeedMultiplier();
    }

    private void RemoveFreeze()
    {
        isFrozen = false;
        entity.SetFrozen(false);
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
}