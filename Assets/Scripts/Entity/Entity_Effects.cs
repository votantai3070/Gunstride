using System.Collections;
using UnityEngine;

public class Entity_Effects : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material hurtMat;

    [Header("Hurt Effect")]
    [SerializeField] private float effectDelay = 0.2f;
    [SerializeField] private float effectDuration = 0.6f;

    [SerializeField] private GameObject lightningThunderEffectPrefab;

    private Player player;

    private Material originalMat;
    private Color originalColor;

    private Coroutine hurtEffectCo;
    private Coroutine elementalVfxCo;

    private void Awake()
    {
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>(true);

        player = GetComponent<Player>();

        if (sr == null)
        {
            Debug.LogError($"{name}: SpriteRenderer not found.", this);
            return;
        }

        originalMat = sr.sharedMaterial;
        originalColor = sr.color;
    }

    public void CreateThunder(Transform transform, float duration)
    {
        GameObject lightningThunder = ObjectPool.Instance.Spawn(lightningThunderEffectPrefab.name, transform.position, Quaternion.identity);
        lightningThunder.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }


    public void HurtEffect()
    {
        if (sr == null || hurtMat == null)
            return;

        if (hurtEffectCo != null)
        {
            StopCoroutine(hurtEffectCo);
            RestoreVisual();
        }

        hurtEffectCo = StartCoroutine(HurtEffectCo());
    }

    private IEnumerator HurtEffectCo()
    {
        if (player != null)
            player.health.IsDamaged(true);

        float elapsed = 0f;
        bool visible = false;

        while (elapsed < effectDuration)
        {
            sr.sharedMaterial = visible
                ? originalMat
                : hurtMat;

            visible = !visible;

            yield return new WaitForSeconds(effectDelay);
            elapsed += effectDelay;
        }

        RestoreVisual();

        if (player != null)
            player.health.IsDamaged(false);

        hurtEffectCo = null;
    }

    public void GetElementVfx(float duration, ElementType element)
    {
        if (element == ElementType.None || sr == null)
            return;

        if (elementalVfxCo != null)
            StopCoroutine(elementalVfxCo);

        elementalVfxCo =
            StartCoroutine(ElementVfxCo(duration, element));
    }

    private IEnumerator ElementVfxCo(
        float duration,
        ElementType elementType)
    {
        float elapsed = 0f;
        float interval = 0.2f;
        bool toggle = false;

        Color lightColor = GetElementLightColor(elementType);
        Color darkColor = GetElementDarkColor(elementType);

        Debug.Log("Dark: " + darkColor);
        Debug.Log("Light: " + lightColor);
        Debug.Log("Element Type: " + elementType);

        while (elapsed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        sr.color = originalColor;
        elementalVfxCo = null;
    }

    private void RestoreVisual()
    {
        if (sr == null)
            return;

        if (originalMat != null)
            sr.sharedMaterial = originalMat;

        sr.color = originalColor;
    }

    private Color GetElementLightColor(ElementType elementType)
    {
        return elementType switch
        {
            ElementType.Ice => GameColors.Chill,
            ElementType.Fire => GameColors.Fire,
            ElementType.Lightning => GameColors.Lightning,
            _ => Color.white
        };
    }

    private Color GetElementDarkColor(ElementType elementType)
    {
        return elementType switch
        {
            ElementType.Ice => GameColors.ChillDark,
            ElementType.Fire => GameColors.FireDark,
            ElementType.Lightning => GameColors.LightningDark,
            _ => Color.white
        };
    }

    private void OnDisable()
    {
        if (hurtEffectCo != null)
        {
            StopCoroutine(hurtEffectCo);
            hurtEffectCo = null;
        }

        if (elementalVfxCo != null)
        {
            StopCoroutine(elementalVfxCo);
            elementalVfxCo = null;
        }

        RestoreVisual();

        if (player != null)
            player.health.IsDamaged(false);
    }
}