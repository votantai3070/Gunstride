using UnityEngine;
using UnityEngine.UI;

public class ToggleBtn : MonoBehaviour
{
    private Image toggleImage;

    [SerializeField] private Sprite toggleOn;
    [SerializeField] private Sprite toggleOff;

    private void Awake()
    {
        toggleImage = GetComponent<Image>();
    }

    public void SetToggle(bool isOn)
    {
        toggleImage.sprite = isOn ? toggleOn : toggleOff;
    }
}
