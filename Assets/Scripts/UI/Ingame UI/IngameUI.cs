using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public HealthBarUI HealthBarUI { get; private set; }
    public CoinUI CoinUI { get; private set; }
    public StatusIconBarUI IconBarUI { get; private set; }

    [SerializeField] private TextMeshProUGUI distanceText;

    private void Awake()
    {
        HealthBarUI = GetComponentInChildren<HealthBarUI>();
        CoinUI = GetComponentInChildren<CoinUI>();
        IconBarUI = GetComponentInChildren<StatusIconBarUI>();

        if (distanceText != null)
        {
            distanceText.color = GameColors.TextDistance; // Set the color of the distance text
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayGlobalSFX("playList_ingame");
    }

    public void UpdateDistance(float distance)
    {
        float distanceInMeters = Mathf.Max(0f, distance); // Ensure distance is not negative

        distanceText.text = $"Distance: {distanceInMeters:F2} m";
    }
}
