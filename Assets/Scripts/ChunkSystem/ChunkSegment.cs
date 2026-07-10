using UnityEngine;

public class ChunkSegment : MonoBehaviour
{
    [SerializeField] private float length = 20f;
    [SerializeField] private ChunkContentGenerator contentGenerator;

    public float Length => length;

    public void Initialize()
    {
        contentGenerator?.Generate();
    }

    public void RecycleTo(Vector3 newPosition)
    {
        transform.position = newPosition;
        contentGenerator?.Regenerate();
    }
}