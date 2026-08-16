using System.Collections;
using UnityEngine;

public class Entity_Effects : MonoBehaviour
{
    private Player player;
    private SpriteRenderer sr;

    [Header("Hurt Effect")]
    [SerializeField] private Material hurtMat;
    [SerializeField] private float effectDelay = 0.2f;
    [SerializeField] private float effectDuration = 0.6f;

    [Header("Element Effect")]
    [SerializeField] private GameObject lightningThunderEffectPrefab;
    [SerializeField] private GameObject iceFreezeActiveEffectPrefab;
    [SerializeField] private GameObject iceFreezeEndEffectPrefab;
    [SerializeField] private GameObject burnEffectPrefab;

    [Header("Passive Effect")]
    [SerializeField] private GameObject shieldEffectPrefab;

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

    #region Element Effect
    public void CreateThunder(Transform transform, float duration)
    {
        GameObject lightningThunder = ObjectPool.Instance.Spawn(lightningThunderEffectPrefab.name, transform.position, Quaternion.identity);
        lightningThunder.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }

    public void CreateIceActive(Transform transform, float duration)
    {
        GameObject iceFreezeActive = ObjectPool.Instance.Spawn(iceFreezeActiveEffectPrefab.name, transform.position, Quaternion.identity);
        iceFreezeActive.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }

    public void CreateIceEnd(Transform transform, float duration)
    {
        GameObject iceFreezeEnd = ObjectPool.Instance.Spawn(iceFreezeEndEffectPrefab.name, transform.position, Quaternion.identity);
        iceFreezeEnd.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }

    public void CreateFire(Transform transform, float duration)
    {
        GameObject burn = ObjectPool.Instance.Spawn(burnEffectPrefab.name, transform.position, Quaternion.identity, transform);
        burn.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }
    #endregion

    #region Passive Effect
    public void CreateShield(Transform transform, float duration)
    {
        GameObject shield = ObjectPool.Instance.Spawn(shieldEffectPrefab.name, transform.position, Quaternion.identity, transform);
        shield.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }


    #endregion

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
            sr.sharedMaterial = visible ? originalMat : hurtMat;

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

        elementalVfxCo = StartCoroutine(ElementVfxCo(duration, element));
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