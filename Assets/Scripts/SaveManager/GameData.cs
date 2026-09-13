using System;

[Serializable]
public class GameData
{
    public int coins;

    public string selectedWeaponId;

    public GameData()
    {
        selectedWeaponId = "";
    }
}
