using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSortByY : MonoBehaviour
{
    [SerializeField] private int offset = 0;
    [SerializeField] private float precision = 100f;

    private SpriteRenderer sr;
    private Vector3 lastPosition;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        UpdateSortingOrder();
    }

    private void LateUpdate()
    {
        if (transform.position != lastPosition)
        {
            lastPosition = transform.position;
            UpdateSortingOrder();
        }
    }

    private void UpdateSortingOrder()
    {
        sr.sortingOrder = offset - Mathf.RoundToInt(transform.position.y * precision);
    }

    private void OnValidate()
    {
        sr = GetComponent<SpriteRenderer>();

        if (transform.position != lastPosition)
        {
            lastPosition = transform.position;
            UpdateSortingOrder();
        }
    }
}