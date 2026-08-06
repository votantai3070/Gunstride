using System.Collections.Generic;
using UnityEngine;

public class CoinGroup : MonoBehaviour
{
    [SerializeField] private string coinTag = "Coin";
    [SerializeField] private Transform[] spawnPoints;

    [Header("Coin Group Setting")]
    [SerializeField] private int minCoin = 1;
    [SerializeField] private int maxCoin = 6;

    private readonly List<GameObject> spawnedCoins = new();

    private void Start()
    {
        SpawnCoins();
    }

    private void OnEnable()
    {
        if (ObjectPool.Instance != null)
            SpawnCoins();
    }

    private void OnDisable()
    {
        if (ObjectPool.Instance == null)
        {
            spawnedCoins.Clear();
            return;
        }

        ClearSpawnedCoins();
    }

    private void SpawnCoins()
    {
        if (ObjectPool.Instance == null)
        {
            Debug.LogError("ObjectPool.Instance is NULL", this);
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points are empty", this);
            return;
        }

        ClearSpawnedCoins();

        int maxSpawn = Mathf.Min(maxCoin, spawnPoints.Length);
        int minSpawn = Mathf.Clamp(minCoin, 0, maxSpawn);
        int coinAmount = Random.Range(minSpawn, maxSpawn + 1);


        for (int i = 0; i < coinAmount; i++)
        {
            if (spawnPoints[i] == null)
                continue;

            GameObject coin = ObjectPool.Instance.Spawn(coinTag, spawnPoints[i].position, Quaternion.identity, null);

            if (coin != null)
                spawnedCoins.Add(coin);
        }
    }

    private void ClearSpawnedCoins()
    {
        if (ObjectPool.Instance == null)
        {
            spawnedCoins.Clear();
            return;
        }

        for (int i = 0; i < spawnedCoins.Count; i++)
        {
            if (spawnedCoins[i] != null)
                ObjectPool.Instance.Despawn(spawnedCoins[i]);
        }

        spawnedCoins.Clear();
    }
}