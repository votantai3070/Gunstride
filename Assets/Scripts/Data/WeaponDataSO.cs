using System;
using System.ComponentModel;
using UnityEngine;

public enum AmmoType
{
    [Description("Arrow")]
    Arrow,

    [Description("9mm")]
    NineMm,

    [Description("12 Gauge")]
    TwelveGauge,

    [Description("5.56mm")]
    FiveFiveSixMm,

    [Description("7.62mm")]
    SevenSixTwoMm
}

[CreateAssetMenu(fileName = "Weapon - ", menuName = "Hybrid Casual/Weapon Data/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponID;
    public Sprite weaponSprite;
    public RuntimeAnimatorController animatorController;

    [Header("Weapon Stats")]
    public string weaponName;
    public int damage;
    public float fireRate;

    [Header("Ammo Data")]
    public AmmoData ammoData;

    [Header("Price")]
    public int price;
}

[Serializable]
public class AmmoData
{
    public Sprite bulletSprite;
    public AmmoType ammoType;
}