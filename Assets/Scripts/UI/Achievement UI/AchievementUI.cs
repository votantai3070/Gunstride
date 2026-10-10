using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private Achievement_ListDataSO achievementListDataSO;
    [SerializeField] private AchievementListUI achievementListUI;
    [SerializeField] private GameObject achievementSlotPrefab;

    [Header("Coin UI")]
    [SerializeField] private TextMeshProUGUI coinTotalText;

    private void Awake()
    {
        if (achievementListUI == null)
        {
            achievementListUI = GetComponentInChildren<AchievementListUI>(true);
        }

        achievementListUI.InitialzeAchievementSlot(achievementSlotPrefab, achievementListDataSO);
    }

    private void OnEnable()
    {
        if (AchievementManager.Instance == null)
            return;

        if (CoinManager.Instance != null)
        {
            CoinManager.OnCoinChanged += UpdateTotalCoin;
            UpdateTotalCoin(CoinManager.Instance.totalCoins);
        }

        AchievementManager.Instance.OnAchievementProgressChanged += RefreshAchievementUI;

        RefreshAchievementUI();
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance == null)
            return;

        if (CoinManager.Instance != null)
            CoinManager.OnCoinChanged -= UpdateTotalCoin;

        AchievementManager.Instance.OnAchievementProgressChanged -= RefreshAchievementUI;
    }

    private void RefreshAchievementUI()
    {
        achievementListUI.RefreshAllSlots();
    }

    private void UpdateTotalCoin(int totalCoin)
    {
        if (coinTotalText != null)
            coinTotalText.text = totalCoin.ToString();
    }
}
