using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    public enum TabType
    {
        Shop,
        Stat
    }

    [SerializeField] private Button button;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private TabType tabType; // nút này thuộc tab nào

    private Sprite originalSprite;
    private SwitchContainer container;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        var img = button.GetComponent<Image>();
        if (img != null && normalSprite == null)
            normalSprite = img.sprite;

        originalSprite = normalSprite;

        button.transition = Selectable.Transition.SpriteSwap;

        var spriteState = button.spriteState;
        spriteState.highlightedSprite = normalSprite;
        spriteState.pressedSprite = pressedSprite;
        button.spriteState = spriteState;

        container = GetComponentInParent<SwitchContainer>(true);

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (container == null) return;

        if (tabType == TabType.Shop)
            container.SwitchToShopList();
        else if (tabType == TabType.Stat)
            container.SwitchToStatList();
    }

    public void SetActiveVisual(bool isActive)
    {
        var img = button.GetComponent<Image>();
        if (img == null) return;

        img.sprite = isActive ? pressedSprite : originalSprite;
    }

    public TabType GetTabType() => tabType;
}