using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private Quaternion originRot;

    private void Awake()
    {
        originRot = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = originRot;
    }

    public void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
