using Managers;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private GameObject[] uiElements;
    private Player player;

    public IngameUI IngameUI { get; private set; }
    public SettingsUI SettingsUI { get; private set; }
    public TotalSummaryUI TotalSummaryUI { get; private set; }
    public ShopUI ShopUI { get; private set; }
    public UI_FadeScreen FadeUI { get; private set; }
    private Button[] buttons;

    private void Awake()
    {
        Instance = this;

        IngameUI = GetComponentInChildren<IngameUI>(true);
        SettingsUI = GetComponentInChildren<SettingsUI>(true);
        TotalSummaryUI = GetComponentInChildren<TotalSummaryUI>(true);
        ShopUI = GetComponentInChildren<ShopUI>(true);
        FadeUI = GetComponentInChildren<UI_FadeScreen>(true);
    }

    private void Start()
    {
        if (IngameUI != null)
        {
            CoinManager.OnCoinChanged += UpgradeCoinUI;
            UpgradeCoinUI(CoinManager.Instance.TakenCoins);
        }

        RegisterAllButtonSounds();
    }

    private void RegisterAllButtonSounds()
    {
        buttons = GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            button.onClick.RemoveListener(AudioManager.Instance.PlayButtonClickSFX);
            button.onClick.AddListener(AudioManager.Instance.PlayButtonClickSFX);
        }
    }

    private void OnDestroy()
    {
        CoinManager.OnCoinChanged -= UpgradeCoinUI;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        Debug.Log("Player availiable");
    }

    public void UpdateTotalSummaryUI()
    {
        int distance = Mathf.RoundToInt(GameManager.Instance.PlayerDistance);
        int coins = CoinManager.Instance.TakenCoins;
        int enemiesDefeated = GameManager.Instance.EnemiesDefeated;

        TotalSummaryUI.UpdateTotalSummaryUI(distance, coins, enemiesDefeated, 0);
    }

    public void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        IngameUI.HealthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void UpgradeCoinUI(int amount)
    {
        if (IngameUI == null) return;
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
            bool active = uiElement == activeUI;
            uiElement.SetActive(active);
        }
    }

    public void OpenTotalSummaryUI()
    {
        ActiveUI(TotalSummaryUI.gameObject);
    }

    public void OpenSettingUI()
    {
        ActiveUI(SettingsUI.gameObject);
    }

    public void OpenIngameUI()
    {
        ActiveUI(IngameUI.gameObject);
    }

    public void StartGame()
    {
        GameManager.Instance.ChangeScene("Level");
    }

    public void SwitchMainMenu()
    {
        SaveManager.instance.SaveGame();
        GameManager.Instance.ChangeScene("MainMenu");
    }
}
