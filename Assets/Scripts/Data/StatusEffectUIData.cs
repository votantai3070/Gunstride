using System;
using UnityEngine;

[Serializable]
public class StatusEffectUIData
{
    public string id;
    public Sprite icon;
    public float duration;
    public float remainingTime;
    public int stack;
}