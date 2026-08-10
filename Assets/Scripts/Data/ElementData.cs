public class ElementData
{
    public ElementType elementType;

    public ElementData(EntitySkillManager skillManager, SkillType skillType)
    {
        elementType = skillManager.GetElementType(skillType);
    }
}
