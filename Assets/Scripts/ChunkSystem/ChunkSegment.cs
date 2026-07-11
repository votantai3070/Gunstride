using UnityEngine;

public class ChunkSegment : MonoBehaviour
{
    [Header("Chunk Points")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("Content")]
    [SerializeField] private ChunkContentGenerator contentGenerator;

    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;
    public float Length => EndPoint.position.x - StartPoint.position.x;

    private void Awake()
    {
        if (contentGenerator == null)
            contentGenerator = GetComponentInChildren<ChunkContentGenerator>();
    }

    public void Initialize()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError($"ChunkSegment '{name}' is missing StartPoint or EndPoint.", this);
            return;
        }

        contentGenerator?.Generate();
    }

    public void RecycleTo(Vector3 newPosition)
    {
        transform.position = newPosition;
        contentGenerator?.Regenerate();
    }
}