using UnityEngine;

[CreateAssetMenu(fileName = "Skill - ", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff")]
public class SkillBuffDataSO : ScriptableObject
{
    public SkillType skillType;
    public RuntimeAnimatorController skillAnim;
    public BuffUpgradeData upgradeData;

    [System.Serializable]
    public class BuffUpgradeData
    {
        public SkillUpgradeType upgradeType;
        public int amount = 1;

        [Header("Pierce")]
        public int pierceCount = 0;

        [Header("Explode")]
        public float explodeRadius = 1.5f;
        public int explodeDamage = 1;
        public LayerMask explodeTargetMask;
    }
}
