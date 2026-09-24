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
    [SerializeField] private int baseCoinCost = 5;

    [Header("Refund Settings")]
    [Range(0, 100)]
    [SerializeField] private int refundPercentage = 80;

    private int statPoint;

    private void Awake()
    {
        if (statNameText != null)
            statNameText.text = GetStatName(statType);

        if (statSlider == null)
            statSlider = GetComponentInChildren<Slider>();

        if (statDecreaseButton != null)
            statDecreaseButton.onClick.AddListener(DecreasePoint);

        if (statInscreaseButton != null)
            statInscreaseButton.onClick.AddListener(InscreasePoint);
    }

    private void Start()
    {
        if (statSlider != null)
        {
            statSlider.minValue = 0;
            statSlider.maxValue = maxPoint;
            statSlider.wholeNumbers = true;
            statSlider.interactable = false;
            statSlider.value = statPoint;
        }

        UpdateCoinAmountText();
    }

    private void OnDestroy()
    {
        if (statDecreaseButton != null)
            statDecreaseButton.onClick.RemoveListener(DecreasePoint);

        if (statInscreaseButton != null)
            statInscreaseButton.onClick.RemoveListener(InscreasePoint);
    }

    private void OnValidate()
    {
        maxPoint = Mathf.Max(0, maxPoint);
        baseCoinCost = Mathf.Max(0, baseCoinCost);

        if (statNameText != null)
            statNameText.text = GetStatName(statType);

        gameObject.name = $"Attribute Slot - {statType}";
    }

    private int GetCoinCostForNextPoint()
    {
        // 0 → 1 = base × 1
        // 1 → 2 = base × 2
        return baseCoinCost * (statPoint + 1);
    }

    private int GetCoinCostForCurrentPoint()
    {
        // Point = 2 means last paid upgrade was 1 → 2.
        return baseCoinCost * statPoint;
    }

    private void InscreasePoint()
    {
        if (statPoint >= maxPoint)
        {
            Debug.Log($"{statType} already at max.");
            return;
        }

        int coinCost = GetCoinCostForNextPoint();

        if (!CoinManager.Instance.CanSpendCoin(coinCost))
        {
            Debug.Log(
                $"Insufficient coins. Need {coinCost} coins for " +
                $"{statType} {statPoint} → {statPoint + 1}."
            );
            return;
        }

        CoinManager.Instance.RemoveCoin(coinCost);

        AddPoint(1);

        // AddPoint() đã gọi UpdateCoinAmountText.
        SaveManager.instance.SaveGame();

        Debug.Log(
            $"Increased {statType}: {statPoint} | Cost: {coinCost}"
        );
    }

    private void DecreasePoint()
    {
        if (statPoint <= 0)
            return;

        int paidCost = GetCoinCostForCurrentPoint();

        int refundAmount = Mathf.RoundToInt(paidCost * (refundPercentage / 100f));

        SetPoint(statPoint - 1);

        CoinManager.Instance.AddTotalCoin(refundAmount);

        SaveManager.instance.SaveGame();

        Debug.Log(
            $"Decreased {statType}: {statPoint} | " +
            $"Refunded {refundAmount}/{paidCost} coins"
        );
    }

    public void AddPoint(int amount)
    {
        SetPoint(statPoint + amount);
    }

    public void SetPoint(int point)
    {
        statPoint = Mathf.Clamp(point, 0, maxPoint);

        if (statSlider != null)
            statSlider.value = statPoint;

        UpdateCoinAmountText();
    }

    private void UpdateCoinAmountText()
    {
        if (coinAmountText == null)
            return;

        coinAmountText.text = statPoint >= maxPoint
            ? "MAX"
            : GetCoinCostForNextPoint().ToString();
    }

    public StatType GetStatType()
    {
        return statType;
    }

    public int ExistPoint()
    {
        return statPoint;
    }

    private string GetStatName(StatType statType)
    {
        return statType switch
        {
            StatType.MaxHealth => "MaxHealth",
            StatType.Speed => "Speed",
            StatType.Strengh => "Strength",
            StatType.CritDamage => "Crit Damage",
            StatType.CritChange => "Crit Rate",
            _ => "",
        };
    }
}