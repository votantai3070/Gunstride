using System;
using UnityEngine;

[Serializable]
public class ElementDataScale
{
    [Header("Chill")]
    public int chillStacksPerHit = 1;
    public float chillDuration = 2f;
    public float chillPercentPerStack = 0.2f;

    [Header("Freeze")]
    public int freezeThreshold = 3;
    public float freezeDuration = 1.5f;

    [Header("Burn")]
    public float burnDuration = 3;
    public float burnDamage = 1;

    [Header("Shock")]
    public float shockDuration = 3f;
    public float shockDamageScale = 1;
    public float shockCharge = .4f;

    [Header("Shock Thunder")]
    public float lightningThunderDuration = 1f;
}
