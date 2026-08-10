using System;
using UnityEngine;

[Serializable]
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

    public ElementalEffectData(ElementDataScale damageScale)
    {
        chillStacksPerHit = damageScale.slowStacksPerHit;
        chillDuration = damageScale.shockDuration;
        chillPercentPerStack = damageScale.slowPercentPerStack;

        freezeThreshold = damageScale.freezeThreshold;
        freezeDuration = damageScale.freezeDuration;

        burnDuration = damageScale.burnDuration;

        shockCharge = damageScale.shockCharge;
        shockDuration = damageScale.shockDuration;
    }
}
