using System.Collections;
using UnityEngine;

public class Player_Effect : Entity_Effects
{
    private Player player;

    [Header("Hurt Effect")]
    [SerializeField] private Material hurtMat;
    [SerializeField] private float effectDelay = 0.2f;
    [SerializeField] private float effectDuration = 0.6f;

    [Header("Passive Effect")]
    [SerializeField] private GameObject shieldEffectPrefab;

    private Coroutine hurtEffectCo;


    protected override void Awake()
    {
        player = GetComponent<Player>();
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

    #region Passive Effect
    public void CreateShield(Transform transform, float duration)
    {
        GameObject shield = ObjectPool.Instance.Spawn(shieldEffectPrefab.name, transform.position, Quaternion.identity, transform);
        shield.GetComponent<VFX_AutomationEffectItem>().Play(duration);
    }


    #endregion

    protected override void OnDisable()
    {
        if (hurtEffectCo != null)
        {
            StopCoroutine(hurtEffectCo);
            hurtEffectCo = null;
        }

        if (player != null)
            player.health.IsDamaged(false);
    }
}
