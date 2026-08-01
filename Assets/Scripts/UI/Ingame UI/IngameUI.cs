using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public HealthBarUI HealthBarUI { get; private set; }
    public CoinUI CoinUI { get; private set; }

    [SerializeField] private TextMeshProUGUI distanceText;

    private void Awake()
    {
        HealthBarUI = GetComponentInChildren<HealthBarUI>();
        CoinUI = GetComponentInChildren<CoinUI>();

        if (distanceText != null)
        {
            Debug.LogError("Distance Text is not assigned in the inspector.");
            distanceText.color = GameColors.TextDistance; // Set the color of the distance text
        }
    }

    public void UpdateDistance(float distance)
    {
        float distanceInMeters = Mathf.Max(0f, distance); // Ensure distance is not negative

        distanceText.text = $"{distanceInMeters:F2} m";
    }
}
