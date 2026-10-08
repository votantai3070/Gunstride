using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int coins;

    public string selectedWeaponId;
    public SerializableDictionary<string, WeaponDataSO> weaponPurchased;
    public SerializableDictionary<StatType, int> statBuffs;

    [Header("Achievements")]
    public SerializableDictionary<string, int> achievementProgress;
    public SerializableDictionary<string, bool> achievementCompleted;
    public SerializableDictionary<string, bool> achievementRewardClaimed;

    public GameData()
    {
        coins = 0;
        selectedWeaponId = "";

        weaponPurchased = new SerializableDictionary<string, WeaponDataSO>();
        statBuffs = new SerializableDictionary<StatType, int>();

        achievementProgress = new SerializableDictionary<string, int>();
        achievementCompleted = new SerializableDictionary<string, bool>();
        achievementRewardClaimed = new SerializableDictionary<string, bool>();
    }
}