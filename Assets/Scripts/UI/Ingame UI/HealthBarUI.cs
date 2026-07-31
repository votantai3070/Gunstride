using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private HeartUI[] hearts;
    private RectTransform rect;
    private HorizontalLayoutGroup layoutGroup;

    private void Awake()
    {
        hearts = GetComponentsInChildren<HeartUI>(true);
        rect = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        ResizeBackground(Mathf.RoundToInt(maxHealth));

        for (int i = 0; i < hearts.Length; i++)
        {
            bool active = i < maxHealth;
            hearts[i].gameObject.SetActive(active);

            if (!active)
                continue;

            if (i < currentHealth)
                hearts[i].SetHeartFull();
            else
                hearts[i].SetHeartEmpty();
        }
    }

    private void ResizeBackground(int heartCount)
    {
        if (rect == null || layoutGroup == null || heartCount <= 0)
            return;

        RectTransform heartRect = hearts[0].GetComponent<RectTransform>();
        float heartWidth = heartRect.rect.width;

        float spacing = layoutGroup.spacing;
        float leftPadding = layoutGroup.padding.left;
        float rightPadding = layoutGroup.padding.right;

        float width = leftPadding + rightPadding + (heartCount * heartWidth) + ((heartCount - 1) * spacing);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}