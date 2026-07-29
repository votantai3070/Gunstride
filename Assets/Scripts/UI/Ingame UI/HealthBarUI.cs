using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private HeartUI[] hearts;

    private void Awake()
    {
        hearts = GetComponentsInChildren<HeartUI>(true);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        for (int i = 0; i < hearts.Length; i++)
        {
            bool isActive = i < maxHealth;
            hearts[i].gameObject.SetActive(isActive);

            if (!isActive)
                continue;

            if (i < currentHealth)
                hearts[i].SetHeartFull();
            else
                hearts[i].SetHeartEmpty();
        }
    }
}