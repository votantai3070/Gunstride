using UnityEngine;

public class ElementalEffectData
{
    [Header("Chill")]
    public int chillStacksPerHit;
    public float chillDuration;
    public float chillPercentPerStack;

    [Header("Freeze")]
    public int freezeThreshold;
    public float freezeDuration;

    [Header("Burn")]
    public float burnDuration;
    public float burnDamageScale;

    [Header("Shock")]
    public float shockDuration;
    public float shockDamageScal;
    public float shockCharge;

    [Header("Lightning Thunder")]
    public float lightningThunderDuration;

    public ElementalEffectData(ElementDataScale damageScale)
    {
        chillStacksPerHit = damageScale.chillStacksPerHit;
        chillDuration = damageScale.chillDuration;
        chillPercentPerStack = damageScale.chillPercentPerStack;

        freezeThreshold = damageScale.freezeThreshold;
        freezeDuration = damageScale.freezeDuration;

        burnDuration = damageScale.burnDuration;

        shockCharge = damageScale.shockCharge;
        shockDuration = damageScale.shockDuration;

        lightningThunderDuration = damageScale.lightningThunderDuration;
    }
}
