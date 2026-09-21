using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    [Header("Stat")]
    public Stat maxHealth;
    public Stat strength;
    public Stat speed;
    public Stat critChance;
    public Stat critDamage;

    public float GetSpeed()
    {
        float baseSpeed = speed.GetValue();

        return baseSpeed;
    }

    public int GetDamage(out bool isCriticalHit)
    {
        float baseDamage = GetBasePhysicalDamage();
        float baseCritChance = GetCritChance();
        float baseCritDamage = GetCritDamage();

        isCriticalHit = IsCriticalHit(baseCritChance);

        float critMultiplier = 1f + (baseCritDamage / 100f);
        float finalDamage = isCriticalHit ? baseDamage * critMultiplier : baseDamage;

        return Mathf.RoundToInt(finalDamage);
    }

    public float GetBasePhysicalDamage() => strength.GetValue();
    public float GetCritChance() => critChance.GetValue();
    public float GetCritDamage() => critDamage.GetValue();

    private bool IsCriticalHit(float critChance)
    {
        return Random.Range(0, 100) < critChance;
    }

    public void SetupStat(CharacterDataSO characterDataSO)
    {
        maxHealth.SetBaseValue(characterDataSO.maxHealth);
        speed.SetBaseValue(characterDataSO.speed);
        strength.SetBaseValue(characterDataSO.strength);
        critChance.SetBaseValue(characterDataSO.criticalChange);
        critDamage.SetBaseValue(characterDataSO.criticalDamage);
    }
}
