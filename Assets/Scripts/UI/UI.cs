using Managers;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private GameObject[] uiElements;

    public IngameUI IngameUI { get; private set; }
    public SettingsUI SettingsUI { get; private set; }

    private Player player;

    private void Awake()
    {
        Instance = this;

        IngameUI = GetComponentInChildren<IngameUI>(true);
        SettingsUI = GetComponentInChildren<SettingsUI>(true);
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

    private void ActiveUI(GameObject activeUI)
    {
        if (activeUI == IngameUI.gameObject)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;

        foreach (var uiElement in uiElements)
        {
            uiElement.SetActive(uiElement == activeUI);


        }
    }

    public void OpenSettingUI()
    {
        ActiveUI(SettingsUI.gameObject);
    }

    public void OpenIngameUI()
    {
        ActiveUI(IngameUI.gameObject);
    }
}
