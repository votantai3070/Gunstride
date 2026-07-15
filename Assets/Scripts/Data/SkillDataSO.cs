using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Skill Data/Skill")]
public class SkillDataSO : ScriptableObject
{
    public GameObject projectileObj;
    public SkillType skillType;

    [Header("Projectile")]
    public int damage;
    public float speed;
    public float cooldown;
}
