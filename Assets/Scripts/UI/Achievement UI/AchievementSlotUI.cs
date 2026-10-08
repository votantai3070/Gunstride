using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlotUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI achievementNameText;
    [SerializeField] private TextMeshProUGUI achievementDescriptionText;
    [SerializeField] private TextMeshProUGUI achievementRewardText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Button")]
    [SerializeField] private Button claimButton;

    [Header("Visual")]
    [SerializeField] private CanvasGroup canvasGroup;

    private AchievementDataSO achievementData;


    public void SetAchievementData(AchievementDataSO data)
    {
        if (data == null)
        {
            Debug.LogWarning("Achievement data is null.");
            return;
        }

        achievementData = data;

        achievementNameText.text = data.achievementName;
        achievementDescriptionText.text = data.achievementDescription;
        achievementRewardText.text = $"Reward: {data.achievementRewardDescription}";

        if (claimButton != null)
        {
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(ClaimReward);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (achievementData == null || AchievementManager.Instance == null)
        {
            return;
        }

        AchievementManager manager = AchievementManager.Instance;

        int progress = manager.GetProgress(achievementData);
        int target = achievementData.targetValue;

        AchievementState state = manager.GetState(achievementData);

        if (progressText != null)
        {
            progressText.text = $"{progress}/{target}";
        }

        if (statusText != null)
        {
            statusText.text = GetStatusText(state);
            statusText.color = GetStatusColor(state);
        }

        if (claimButton != null)
        {
            claimButton.gameObject.SetActive(state == AchievementState.Completed);
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = state == AchievementState.Claimed ? 0.55f : 1f;
        }
    }

    private void ClaimReward()
    {
        if (achievementData == null ||
            AchievementManager.Instance == null)
        {
            return;
        }

        AchievementManager.Instance.ClaimReward(achievementData);

        RefreshUI();
    }

    private string GetStatusText(AchievementState state)
    {
        return state switch
        {
            AchievementState.NotStarted => "NOT STARTED",
            AchievementState.InProgress => "IN PROGRESS",
            AchievementState.Completed => "COMPLETED - CLAIM!",
            AchievementState.Claimed => "CLAIMED",
            _ => string.Empty
        };
    }

    private Color GetStatusColor(AchievementState state)
    {
        return state switch
        {
            AchievementState.NotStarted => new Color(0.65f, 0.65f, 0.65f),
            AchievementState.InProgress => new Color(1f, 0.82f, 0.15f),
            AchievementState.Completed => new Color(0.25f, 1f, 0.45f),
            AchievementState.Claimed => new Color(0.45f, 0.75f, 0.55f),
            _ => Color.white
        };
    }
}