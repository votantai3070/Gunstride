using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff List", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff List")]
public class SkillBuff_ListDataSO : ScriptableObject
{
    public SkillBuffDataSO[] skillList;

    //public Skill_DataSO GetItemData(string saveId)
    //{
    //    return itemList.FirstOrDefault(item => item != null && item.saveId == saveId);
    //}

#if UNITY_EDITOR
    [ContextMenu("Auto-fill with all SkillBuffDataSO")]
    public void CollectItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:SkillBuffDataSO");

        skillList = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<SkillBuffDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
