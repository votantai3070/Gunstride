using System;
using UnityEngine;

[Serializable]
public class ElementDataScale
{
    [Header("Chill")]
    public int slowStacksPerHit = 1;
    public float slowDuration = 2f;
    public float slowPercentPerStack = 0.2f;

    [Header("Freeze")]
    public int freezeThreshold = 3;
    public float freezeDuration = 1.5f;

    [Header("Burn")]
    public float burnDuration = 3;
    public float burnDamageScale = 1;

    [Header("Shock")]
    public float shockDuration = 3;
    public float shockDamageScale = 1;
    public float shockCharge = .4f;
}
