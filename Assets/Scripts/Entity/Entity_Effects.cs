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

    private Material originalMat;
    private Coroutine hurtEffectCo;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        originalMat = sr.material;
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

    private void OnDisable()
    {
        sr.material = originalMat;
        hurtEffectCo = null;
    }
}