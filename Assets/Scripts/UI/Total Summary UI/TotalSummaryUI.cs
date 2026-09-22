using TMPro;
using UnityEngine;

public class TotalSummaryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerDistance;
    [SerializeField] private TextMeshProUGUI playerCoins;
    [SerializeField] private TextMeshProUGUI enemiesDefeated;
    [SerializeField] private TextMeshProUGUI buffReceived;

    public void UpdateTotalSummaryUI(int distance, int coins, int enemies, int buffs)
    {
        playerDistance.text = $"{distance} m";
        playerCoins.text = $"{coins}";
        enemiesDefeated.text = $"{enemies}";
        buffReceived.text = $"{buffs}";

        CoinManager.Instance.AddTotalCoin();
    }
}
