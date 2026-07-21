using UnityEngine;

[CreateAssetMenu(fileName = "Character - ", menuName = "Hybrid Casual/Character Data/Character")]
public class CharacterDataSO : ScriptableObject
{
    public CharacterType characterType;
    public float maxHealth = 1;
    public float speed = 2;
    [Space]
    public SkillDataSO skillData;
}
