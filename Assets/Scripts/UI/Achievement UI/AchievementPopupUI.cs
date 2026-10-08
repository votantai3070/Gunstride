using TMPro;
using UnityEngine;

public class AchievementPopupUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void OnEnable()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementCompleted +=
                ShowCompletedPopup;
        }
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementCompleted -=
                ShowCompletedPopup;
        }
    }

    private void ShowCompletedPopup(AchievementDataSO achievement)
    {
        popupPanel.SetActive(true);

        titleText.text = "ACHIEVEMENT COMPLETED!";
        descriptionText.text = achievement.achievementName;

        Debug.Log(
            $"Show achievement popup: {achievement.achievementName}"
        );
    }
}