using System.Collections.Generic;
using UnityEngine;

public class ChunkLooper : MonoBehaviour
{
    private Transform player;
    private float startPlayerX;

    [SerializeField] private List<ChunkSegment> chunks = new();
    [SerializeField] private float recycleOffset = 10f;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>().transform;
        startPlayerX = player.position.x;

        if (chunks == null || chunks.Count == 0)
            chunks = new List<ChunkSegment>(GetComponentsInChildren<ChunkSegment>());
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

        float playerDistance = GetPlayerTravelDistance();

        for (int i = 0; i < chunks.Count; i++)
            chunks[i].Initialize(playerDistance);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted())
            return;

        ChunkLoop();
    }

    private void ChunkLoop()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            ChunkSegment chunk = chunks[i];
            if (chunk == null)
                continue;

            if (player.position.x > chunk.EndPoint.position.x + recycleOffset)
                RecycleChunk(chunk);
        }
    }

    private void RecycleChunk(ChunkSegment chunk)
    {
        ChunkSegment farthestChunk = GetFarthestChunk();
        if (farthestChunk == null)
            return;

        float playerDistance = GetPlayerTravelDistance();

        Vector3 offsetFromStart = chunk.transform.position - chunk.StartPoint.position;
        Vector3 newPos = chunk.transform.position;
        newPos.x = farthestChunk.EndPoint.position.x + offsetFromStart.x;

        chunk.RecycleTo(newPos, playerDistance);
    }

    private float GetPlayerTravelDistance()
    {
        return Mathf.Max(0f, player.position.x - startPlayerX);
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
}