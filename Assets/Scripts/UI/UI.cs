using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    private IngameUI ingameUI;
    private Player player;

    private void Awake()
    {
        Instance = this;

        ingameUI = GetComponentInChildren<IngameUI>(true);
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
        ingameUI.HealthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void UpgradeCoinUI(int amount)
    {
        ingameUI.CoinUI.SetupCoin(amount);
    }
}
