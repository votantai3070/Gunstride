public class Player_Stats : Entity_Stats, ISaveable
{
    public void AddModifier(StatType type, int value)
    {
        Stat stat = GetStatByType(type);
        bool isPercent = GetPercentByStatType(type);

        stat.AddModifier(value, type.ToString(), isPercent);
    }

    private bool GetPercentByStatType(StatType type)
    {
        return type switch
        {
            StatType.Speed => false,
            StatType.MaxHealth => false,
            StatType.Strengh => false,
            StatType.CritDamage => true,
            StatType.CritChange => true,
            _ => false,
        };
    }

    public void LoadData(GameData data)
    {
        if (data == null) return;

        foreach (var buff in data.statBuffs)
        {
            StatType statType = buff.Key;
            int value = buff.Value;

            AddModifier(statType, value);
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}
