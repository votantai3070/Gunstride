using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement List", menuName = "Gunstrike Data/Achievement Data/Achievement List")]
public class Achievement_ListDataSO : ScriptableObject
{
    public AchievementDataSO[] achievementList;

    public AchievementDataSO GetAchievementById(string saveId)
    {
        return achievementList.FirstOrDefault(item => item != null && item.AchievementID == saveId);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill with all AchievementDataSO")]
    public void CollectItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:AchievementDataSO");

        achievementList = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<AchievementDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(item => item != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
