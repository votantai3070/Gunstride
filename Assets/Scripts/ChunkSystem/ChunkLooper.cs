using System.Collections.Generic;
using UnityEngine;

public class ChunkLooper : MonoBehaviour
{
    private Transform player;

    [SerializeField] private List<ChunkSegment> chunks = new();
    [Space]
    [SerializeField] private float moveSpeed = 6f;
    //[SerializeField] private float recycleX = -25f;
    [SerializeField] private float recycleOffset = 10f;

    //private ChunkSegment rightMostChunk;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>().transform;

        if (chunks == null || chunks.Count == 0)
        {
            chunks = new List<ChunkSegment>(GetComponentsInChildren<ChunkSegment>());
        }
    }

    private void Start()
    {
        if (chunks == null || chunks.Count == 0)
        {
            Debug.LogWarning("ChunkLooper: No chunks assigned.");
            enabled = false;
            return;
        }

        for (int i = chunks.Count - 1; i >= 0; i--)
        {
            if (chunks[i] == null)
                chunks.RemoveAt(i);
        }

        if (chunks.Count == 0)
        {
            Debug.LogWarning("ChunkLooper: Chunk list only contains null references.");
            enabled = false;
            return;
        }

        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].Initialize();
        }

        //rightMostChunk = GetRightMostChunk();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted()) return;

        ChunkLoop();
    }

    //private void ChunkLoop()
    //{
    //    //float delta = moveSpeed * Time.deltaTime;

    //    for (int i = 0; i < chunks.Count; i++)
    //    {
    //        ChunkSegment chunk = chunks[i];
    //        if (chunk == null) continue;

    //        //chunk.transform.Translate(Vector3.left * delta, Space.World);

    //        if (chunk.EndPoint.position.x <= recycleX)
    //        {
    //            RecycleChunk(chunk);
    //        }
    //    }
    //}

    private void ChunkLoop()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            ChunkSegment chunk = chunks[i];
            if (chunk == null)
                continue;

            if (player.position.x > chunk.EndPoint.position.x + recycleOffset)
            {
                RecycleChunk(chunk);
            }
        }
    }

    //private void RecycleChunk(ChunkSegment chunk)
    //{
    //    if (chunk == null) return;

    //    ChunkSegment currentRightMost = GetRightMostChunk(chunk);
    //    if (currentRightMost == null) return;

    //    Vector3 offsetFromStart = chunk.transform.position - chunk.StartPoint.position;
    //    Vector3 targetPos = currentRightMost.EndPoint.position + offsetFromStart;

    //    chunk.RecycleTo(targetPos);

    //    rightMostChunk = GetRightMostChunk();
    //}

    private void RecycleChunk(ChunkSegment chunk)
    {
        ChunkSegment farthestChunk = GetFarthestChunk();

        Vector3 offsetFromStart = chunk.transform.position - chunk.StartPoint.position;
        Vector3 newPos = chunk.transform.position;
        newPos.x = farthestChunk.EndPoint.position.x + offsetFromStart.x;

        chunk.RecycleTo(newPos);
    }

    private ChunkSegment GetFarthestChunk()
    {
        ChunkSegment farthest = null;
        float maxX = float.MinValue;

        for (int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i] == null)
                continue;

            float endX = chunks[i].EndPoint.position.x;
            if (endX > maxX)
            {
                maxX = endX;
                farthest = chunks[i];
            }
        }

        return farthest;
    }

    //private ChunkSegment GetRightMostChunk(ChunkSegment ignore = null)
    //{
    //    ChunkSegment result = null;
    //    float maxX = float.MinValue;

    //    for (int i = 0; i < chunks.Count; i++)
    //    {
    //        ChunkSegment chunk = chunks[i];
    //        if (chunk == null || chunk == ignore) continue;

    //        float endX = chunk.EndPoint.position.x;
    //        if (endX > maxX)
    //        {
    //            maxX = endX;
    //            result = chunk;
    //        }
    //    }

    //    return result;
    //}
}