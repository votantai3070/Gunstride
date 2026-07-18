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
    }
}