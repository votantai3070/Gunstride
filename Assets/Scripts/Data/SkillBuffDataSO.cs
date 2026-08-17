using UnityEngine;

public class SkillBuffDataSO : ScriptableObject
{
    public SkillType skillType;
    public SkillUpgradeType upgradeType;
    public RuntimeAnimatorController skillAnim;
    public GameObject hitEffect;

    public virtual void ApplyEffect(GameObject playerObject) { }

    protected ElementType GetElementType()
    {
        if (upgradeType == SkillUpgradeType.Chill)
            return ElementType.Ice;
        else if (upgradeType == SkillUpgradeType.Burn)
            return ElementType.Fire;
        else if (upgradeType == SkillUpgradeType.Shock)
            return ElementType.Lightning;

        return ElementType.None;
    }
}