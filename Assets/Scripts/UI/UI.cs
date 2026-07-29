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

    public void SetPlayer(Player player)
    {
        this.player = player;
        Debug.Log("Player availiable");
    }

    public void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        ingameUI.HealthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }
}
