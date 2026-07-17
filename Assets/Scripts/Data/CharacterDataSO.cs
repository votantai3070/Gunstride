using UnityEngine;

[CreateAssetMenu(fileName = "Character - ", menuName = "Hybrid Casual/Character Data/Character")]
public class CharacterDataSO : ScriptableObject
{
    public CharacterType characterType;
    public float maxHealth = 1;
    [Space]
    public SkillDataSO skillData;
}
