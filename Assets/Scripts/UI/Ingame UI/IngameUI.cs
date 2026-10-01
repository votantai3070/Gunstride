using Managers;
using System.Collections;
using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public HealthBarUI HealthBarUI { get; private set; }
    public CoinUI CoinUI { get; private set; }
    public StatusIconBarUI IconBarUI { get; private set; }

    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private GameObject killAmount;

    private Coroutine showSkillCo;

    private void Awake()
    {
        HealthBarUI = GetComponentInChildren<HealthBarUI>();
        CoinUI = GetComponentInChildren<CoinUI>();
        IconBarUI = GetComponentInChildren<StatusIconBarUI>();

        if (distanceText != null)
        {
            distanceText.color = GameColors.TextDistance; // Set the color of the distance text
        }
    }

    private void OnEnable()
    {
        GameManager.OnKillChanged += UpdateKillAmountText;
        AudioManager.Instance.StartBGM("playList_ingame");
    }

    private void OnDisable()
    {
        GameManager.OnKillChanged -= UpdateKillAmountText;
    }

    public void UpdateDistance(float distance)
    {
        float distanceInMeters = Mathf.Max(0f, distance); // Ensure distance is not negative

        distanceText.text = $"Distance: {distanceInMeters:F2} m";
    }

    public void UpdateKillAmountText(string text)
    {
        if (killAmount != null)
        {
            ShowKillAmount();
            killAmount.GetComponentInChildren<TextMeshProUGUI>(true).text = text;
        }
    }

    private void ShowKillAmount()
    {
        if (showSkillCo != null)
            StopCoroutine(showSkillCo);

        showSkillCo = StartCoroutine(ShowHUDKillAmount());
    }

    private IEnumerator ShowHUDKillAmount()
    {
        killAmount.SetActive(true);
        yield return new WaitForSeconds(1f);
        killAmount.SetActive(false);
    }
}
