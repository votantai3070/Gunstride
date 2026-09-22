using System;

[Serializable]
public class GameData
{
    public int coins;

    public string selectedWeaponId;
    public SerializableDictionary<string, WeaponDataSO> weaponPurchased;

    public GameData()
    {
        coins = 0;
        selectedWeaponId = "";

        weaponPurchased = new SerializableDictionary<string, WeaponDataSO>();
    }
}
