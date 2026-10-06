using UnityEngine;

[CreateAssetMenu(
    fileName = "Achievement - ",
    menuName = "Gunstrike Data/Achievement Data"
)]
public class AchievementDataSO : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string achievementID;

    [Header("Display")]
    public string achievementName;

    [TextArea(3, 10)]
    public string achievementDescription;

    [TextArea(4, 10)]
    public string achievementRewardDescription;

    public string AchievementID => achievementID;

#if UNITY_EDITOR
    [ContextMenu("Generate Random ID")]
    private void GenerateRandomID()
    {
        achievementID = System.Guid.NewGuid().ToString("N");
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}