using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public HealthBarUI HealthBarUI { get; private set; }
    public CoinUI CoinUI { get; private set; }

    private void Awake()
    {
        HealthBarUI = GetComponentInChildren<HealthBarUI>();
        CoinUI = GetComponentInChildren<CoinUI>();
    }
}
