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
    public float cooldown;
}
