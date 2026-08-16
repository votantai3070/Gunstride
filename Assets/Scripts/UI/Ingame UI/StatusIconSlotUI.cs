using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusIconSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private TMP_Text stackText;

    private StatusEffectUIData effectData;

    public string EffectId => effectData != null ? effectData.id : string.Empty;

    public void SetEffect(StatusEffectUIData data)
    {
        effectData = data;

        iconImage.sprite = data.icon;
        data.remainingTime = data.duration;

        cooldownImage.fillAmount = 1f;

        UpdateStackUI();
    }

    private void Update()
    {
        if (effectData == null)
            return;

        effectData.remainingTime -= Time.deltaTime;

        cooldownImage.fillAmount = Mathf.Clamp01(effectData.remainingTime / effectData.duration);

        if (effectData.stack > 1)
            UpdateStackUI();

        if (effectData.remainingTime <= 0f)
        {
            effectData = null;
            ObjectPool.Instance.Despawn(gameObject);
        }
    }

    private void UpdateStackUI()
    {
        if (stackText == null)
            return;

        stackText.text = effectData.stack > 1 ? effectData.stack.ToString() : string.Empty;
    }
}