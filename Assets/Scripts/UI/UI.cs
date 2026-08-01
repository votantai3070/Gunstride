using Managers;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public IngameUI IngameUI { get; private set; }
    private Player player;

    private void Awake()
    {
        Instance = this;

        IngameUI = GetComponentInChildren<IngameUI>(true);
    }

    private void Start()
    {
        GameManager.Instance.OnCoinChanged += UpgradeCoinUI;
        UpgradeCoinUI(GameManager.Instance.Coin);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinChanged -= UpgradeCoinUI;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        Debug.Log("Player availiable");
    }

    public void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        IngameUI.HealthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void UpgradeCoinUI(int amount)
    {
        IngameUI.CoinUI.SetupCoin(amount);
    }
}
