using System.Collections.Generic;
using UnityEngine;

public class ChunkLooper : MonoBehaviour
{
    [SerializeField] private List<ChunkSegment> chunks = new List<ChunkSegment>();
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float recycleX = -25f;

    private float rightMostX;

    private void Start()
    {
        rightMostX = float.MinValue;

        foreach (var chunk in chunks)
        {
            chunk.Initialize();

            float chunkEnd = chunk.transform.position.x + chunk.Length;
            if (chunkEnd > rightMostX)
                rightMostX = chunkEnd;
        }
    }

    private void Update()
    {
        float delta = moveSpeed * Time.deltaTime;

        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].transform.Translate(Vector3.left * delta);

            if (chunks[i].transform.position.x <= recycleX)
            {
                RecycleChunk(chunks[i]);
            }
        }
    }

    private void RecycleChunk(ChunkSegment chunk)
    {
        Vector3 newPos = chunk.transform.position;
        newPos.x = rightMostX;

        chunk.RecycleTo(newPos);

        rightMostX = newPos.x + chunk.Length;
    }
}