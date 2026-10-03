using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FadeWhenPlayerBehind : MonoBehaviour
{
    [Header("Renderers to fade")]
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    [Header("Fade settings")]
    [Range(0f, 1f)]
    [SerializeField] private float transparentAlpha = 0.25f;

    [SerializeField] private float fadeSpeed = 6f;

    private float targetAlpha = 1f;
    private int playersInside;

    private void Awake()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        Collider2D triggerCollider = GetComponent<Collider2D>();
        triggerCollider.isTrigger = true;
    }

    private void Update()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer == null)
                continue;

            Color color = spriteRenderer.color;

            color.a = Mathf.MoveTowards(
                color.a,
                targetAlpha,
                fadeSpeed * Time.deltaTime
            );

            spriteRenderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playersInside++;
        targetAlpha = transparentAlpha;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playersInside = Mathf.Max(0, playersInside - 1);

        if (playersInside == 0)
        {
            targetAlpha = 1f;
        }
    }
}