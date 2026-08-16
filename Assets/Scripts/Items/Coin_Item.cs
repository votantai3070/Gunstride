using Managers;
using System.Collections;
using UnityEngine;

public class Coin_Item : Pickup_Item
{
    [SerializeField] private int amount;

    private Collider2D col;

    [Header("Effect Pickup")]
    [SerializeField] private float effectDuration = 0.2f;
    [SerializeField] private Vector3 moveOffset = new Vector3(0f, 0.5f, 0f);

    private Coroutine effectCo;
    private bool isPicked;

    private Color originColor;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

        if (sr != null)
            originColor = sr.color;
    }
    private void OnDisable()
    {
        if (effectCo != null)
        {
            StopCoroutine(effectCo);
            effectCo = null;
        }

        isPicked = false;

        if (col != null)
            col.enabled = true;

        if (sr != null)
            sr.color = originColor;
    }

    public override void Pickup(Collider2D collider)
    {
        if (isPicked)
            return;

        if (!collider.CompareTag("Player"))
            return;

        isPicked = true;
        GameManager.Instance.AddCoin(amount);
        PickupEffect();
    }

    private void PickupEffect()
    {
        if (effectCo != null)
            StopCoroutine(effectCo);

        effectCo = StartCoroutine(PickupEffectCo());
    }

    private IEnumerator PickupEffectCo()
    {
        if (col != null)
            col.enabled = false;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + moveOffset;

        float time = 0f;

        while (time < effectDuration)
        {
            time += Time.deltaTime;
            float t = time / effectDuration;

            transform.position = Vector3.Lerp(startPos, targetPos, t);

            if (sr != null)
            {
                Color color = sr.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                sr.color = color;
            }

            yield return null;
        }

        effectCo = null;
        ObjectPool.Instance.Despawn(gameObject);
    }
}