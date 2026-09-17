using System;

[Serializable]
public class GameData
{
    public int coins;

    public string selectedWeaponId;
    public SerializableDictionary<string, WeaponDataSO> weaponPurchased;

    public GameData()
    {
        selectedWeaponId = "";

        weaponPurchased = new SerializableDictionary<string, WeaponDataSO>();
    }
}
