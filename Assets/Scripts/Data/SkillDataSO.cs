using UnityEngine;

[CreateAssetMenu(fileName = "Skill - ", menuName = "Hybrid Casual/Skill Data/Skill")]
public class SkillDataSO : ScriptableObject
{
    public GameObject projectileObj;
    public SkillType skillType;

    [Header("Projectile")]
    public string skillName;
    public int damage;
    public float speed;
    public UpgradeData upgradeData;

    [System.Serializable]
    public class UpgradeData
    {
        public SkillUpgradeType upgradeType;
        public int amount = 1;
        public float cooldown = 1f;
        public float delayBetweenShots = 0.2f;

        [Header("Pierce")]
        public int pierceCount = 0;

        [Header("Explode")]
        public float explodeRadius = 1.5f;
        public int explodeDamage = 1;
        public LayerMask explodeTargetMask;
    }
}