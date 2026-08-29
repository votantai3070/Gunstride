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
    private Button[] buttons;

    private void Awake()
    {
        Instance = this;

        IngameUI = GetComponentInChildren<IngameUI>(true);
        SettingsUI = GetComponentInChildren<SettingsUI>(true);
    }

    private void Start()
    {
        if (IngameUI != null)
        {
            GameManager.Instance.OnCoinChanged += UpgradeCoinUI;
            UpgradeCoinUI(GameManager.Instance.Coin);
        }

        RegisterAllButtonSounds();
    }

    private void RegisterAllButtonSounds()
    {
        buttons = GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            button.onClick.RemoveListener(AudioManager.instance.PlayButtonClickSFX);
            button.onClick.AddListener(AudioManager.instance.PlayButtonClickSFX);
        }
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
