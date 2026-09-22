using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeSlotUI : MonoBehaviour
{
    [SerializeField] private StatType statType;

    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI coinAmountText;
    [SerializeField] private Slider statSlider;
    [SerializeField] private Button statDecreaseButton;
    [SerializeField] private Button statInscreaseButton;

    [Header("Upgrade Settings")]
    [SerializeField] private int maxPoint = 10;

    [Header("Coin Cost")]
    [Tooltip("Giá coin base để tăng 1 point. Giá thực tế sẽ tăng dần theo level.")]
    [SerializeField] private int baseCoinCost = 5;

    [Header("Refund Settings")]
    [Range(0, 100)]
    [SerializeField] private int refundPercentage = 80;

    private int statPoint = 0;

    private void Awake()
    {
        if (statNameText != null)
            statNameText.text = statType.ToString();

        statPoint = 0;
        if (statSlider != null)
            statSlider.value = 0;

        UpdateCoinAmountText();

        statDecreaseButton.onClick.AddListener(DecreasePoint);
        statInscreaseButton.onClick.AddListener(InscreasePoint);
    }

    private void OnValidate()
    {
        if (statNameText != null)
            statNameText.text = statType.ToString();
    }

    private int GetCoinCostForNextPoint()
    {
        return baseCoinCost * (statPoint + 1);
    }

    private void DecreasePoint()
    {
        if (statPoint > 0)
        {
            int currentLevelCost = GetCoinCostForNextPoint();
            int refundAmount = Mathf.RoundToInt(currentLevelCost * (refundPercentage / 100f));

            statPoint--;
            statSlider.value = statPoint;

            GameManager.Instance.AddCoin(refundAmount);
            Debug.Log($"Decreased {statType}: {statPoint} | Refunded {refundAmount}/{currentLevelCost} coins ({refundPercentage}%)");
        }
    }

    private void InscreasePoint()
    {
        if (statPoint >= maxPoint)
        {
            Debug.Log($"{statType} already at max ({maxPoint})!");
            return;
        }

        // ✅ Uncomment logic check coin
        int coinCost = GetCoinCostForNextPoint();

        if (!GameManager.Instance.CanSpendCoin(coinCost))
        {
            Debug.Log($"Insufficient coins! Need {coinCost} coins to upgrade {statType} from {statPoint} to {statPoint + 1}");
            return;
        }

        GameManager.Instance.RemoveCoin(coinCost);

        statPoint++;
        statSlider.value = statPoint;

        UpdateCoinAmountText();

        Debug.Log($"Increased {statType}: {statPoint} | Cost: {coinCost} coins");
    }

    private void UpdateCoinAmountText()
    {
        if (coinAmountText == null)
            return;

        if (statPoint >= maxPoint)
            coinAmountText.text = "MAX";
        else
            coinAmountText.text = GetCoinCostForNextPoint().ToString();
    }
}