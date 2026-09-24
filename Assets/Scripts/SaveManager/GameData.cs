using System;

[Serializable]
public class GameData
{
    public int coins;

    public string selectedWeaponId;
    public SerializableDictionary<string, WeaponDataSO> weaponPurchased;
    public SerializableDictionary<StatType, int> statBuffs;

    public GameData()
    {
        selectedWeaponId = "";

        weaponPurchased = new SerializableDictionary<string, WeaponDataSO>();
        statBuffs = new SerializableDictionary<StatType, int>();
    }
}
