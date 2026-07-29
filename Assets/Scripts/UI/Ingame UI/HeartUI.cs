using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    private Image image;

    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartEmpty;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetHeartFull() => image.sprite = heartFull;

    public void SetHeartEmpty() => image.sprite = heartEmpty;
}
