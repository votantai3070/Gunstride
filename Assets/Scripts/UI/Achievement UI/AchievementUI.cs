using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private Achievement_ListDataSO achievementListDataSO;
    [SerializeField] private AchievementListUI achievementListUI;
    [SerializeField] private GameObject achievementSlotPrefab;

    private void Awake()
    {
        if (achievementListUI == null)
        {
            achievementListUI = GetComponentInChildren<AchievementListUI>(true);
        }

        achievementListUI.InitialzeAchievementSlot(achievementSlotPrefab.name, achievementListDataSO);
    }

    private void OnEnable()
    {
        if (AchievementManager.Instance == null)
            return;

        AchievementManager.Instance.OnAchievementProgressChanged += RefreshAchievementUI;

        RefreshAchievementUI();
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance == null)
            return;

        AchievementManager.Instance.OnAchievementProgressChanged -= RefreshAchievementUI;
    }

    private void RefreshAchievementUI()
    {
        achievementListUI.RefreshAllSlots();
    }
}
