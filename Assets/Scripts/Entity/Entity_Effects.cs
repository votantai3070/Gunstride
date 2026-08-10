using System.Collections;
using UnityEngine;

public class Entity_Effects : MonoBehaviour
{
    private SpriteRenderer sr;
    private Player player;

    [Header("Hurt Effect")]
    [SerializeField] private Material hurtMat;
    [SerializeField] private float effectDelay = 0.2f;
    [SerializeField] private float effectDuration = 0.6f;

    [Header("Elemental Vfx")]
    private Coroutine elementalVfxCo;

    private Material originalMat;
    private Color originalColor;
    private Coroutine hurtEffectCo;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        originalMat = sr.material;
        originalColor = sr.color;
    }

    public void HurtEffect()
    {
        if (hurtEffectCo != null)
        {
            StopCoroutine(hurtEffectCo);
            sr.material = originalMat;
        }

        hurtEffectCo = StartCoroutine(HurtEffectCo());
    }

    private IEnumerator HurtEffectCo()
    {
        player.health.IsDamaged(true);
        float elapsed = 0f;

        while (elapsed < effectDuration)
        {
            sr.material = hurtMat;
            yield return new WaitForSeconds(effectDelay);
            elapsed += effectDelay;

            if (elapsed >= effectDuration)
                break;

            sr.material = originalMat;
            yield return new WaitForSeconds(effectDelay);
            elapsed += effectDelay;
        }

        sr.material = originalMat;
        player.health.IsDamaged(false);
        hurtEffectCo = null;
    }

    public void GetElementVfx(float duration, ElementType element)
    {
        if (element == ElementType.None)
            return;

        if (elementalVfxCo != null)
            StopCoroutine(elementalVfxCo);

        elementalVfxCo = StartCoroutine(ElementVfxCo(duration, element));
    }

    private IEnumerator ElementVfxCo(float duration, ElementType elementType)
    {
        float elapsed = 0f;
        float interval = 0.2f;

        bool toggle = false;

        Color lightColor = GetElementLightColor(elementType);
        Color darkColor = GetElementDarkColor(elementType);

        while (elapsed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        sr.color = originalColor;
    }

    private Color GetElementLightColor(ElementType elementType)
    {
        return elementType switch
        {
            ElementType.Ice => GameColors.Chill,
            ElementType.Fire => GameColors.Fire,
            ElementType.Lightning => GameColors.Lightning,

            _ => Color.white,
        };
    }

    private Color GetElementDarkColor(ElementType elementType)
    {
        return elementType switch
        {
            ElementType.Ice => GameColors.ChillDark,
            ElementType.Fire => GameColors.FireDark,
            ElementType.Lightning => GameColors.LightningDark,

            _ => Color.white,
        };
    }

    private void OnDisable()
    {
        sr.material = originalMat;
        hurtEffectCo = null;
    }
}