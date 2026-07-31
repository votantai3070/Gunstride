using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Sprite coin;

    private Image coinImage;
    private TextMeshProUGUI coinText;


    private void Awake()
    {
        coinImage = GetComponentInChildren<Image>();
        coinText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (coinImage != null && coinImage.sprite == null)
            coinImage.sprite = coin;
    }

    public void SetupCoin(int amount)
    {
        coinText.text = amount.ToString();
    }
}
