using UnityEngine;

public class SkillBuffDataSO : ScriptableObject
{
    public SkillType skillType;
    public SkillUpgradeType upgradeType;
    public RuntimeAnimatorController skillAnim;
    public GameObject hitEffect;

    public virtual void ApplyEffect(GameObject playerObject) { }
}