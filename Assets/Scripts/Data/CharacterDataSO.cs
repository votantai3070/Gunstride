using UnityEngine;

[CreateAssetMenu(fileName = "Character - ", menuName = "Hybrid Casual/Character Data/Character")]
public class CharacterDataSO : ScriptableObject
{
    public CharacterType characterType;
    public SkillType skillType;
    public float maxHealth = 1;
}
