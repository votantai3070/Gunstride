using System.Collections.Generic;
using UnityEngine;

public class ChunkContentGenerator : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Optional Random Objects")]
    [SerializeField] private GameObject[] obstacleObjects;
    [SerializeField] private GameObject[] pickupObjects;
    [SerializeField] private GameObject[] randomObjects;
    [SerializeField] private int randomSpawnCount = 3;

    [Header("Distance Phases")]
    [SerializeField] private List<DistancePhase> phases = new();

    [Header("Debug")]
    [SerializeField] private bool showDebugLog;

    public void Generate(float playerDistance)
    {
        ClearSpawnPoints();
        RandomSpawn();
        SpawnByDistance(playerDistance);
    }

    public void Regenerate(float playerDistance)
    {
        ClearSpawnPoints();
        RandomSpawn();
        SpawnByDistance(playerDistance);
    }

    private void SpawnByDistance(float playerDistance)
    {
        DistancePhase phase = GetCurrentPhase(playerDistance);
        if (phase == null)
            return;

        if (showDebugLog)
            Debug.Log($"[{name}] Distance = {playerDistance}, Phase = {phase.phaseName}");

        if (phase.spawnBossOnly)
        {
            SpawnBossPhase(phase);
            return;
        }

        SpawnNormalPhase(phase, playerDistance);
    }

    private void SpawnNormalPhase(DistancePhase phase, float playerDistance)
    {
        if (phase.normalEnemies == null || phase.normalEnemies.Length == 0)
            return;

        int spawnCount = GetSpawnCountByDistance(phase, playerDistance);
        spawnCount = Mathf.Min(spawnCount, spawnPoints.Length);

        List<int> availableIndices = BuildAvailableSpawnIndices();
        ShuffleIndices(availableIndices);

        int spawned = 0;

        for (int i = 0; i < availableIndices.Count && spawned < spawnCount; i++)
        {
            int pointIndex = availableIndices[i];
            SpawnPoint spawnPoint = spawnPoints[pointIndex].GetComponent<SpawnPoint>();

            if (spawnPoint.GetObject() != null)
                continue;

            GameObject enemyPrefab = PickEnemyPrefab(phase);
            if (enemyPrefab == null)
                continue;

            GameObject enemy = ObjectPool.instance.Spawn(
                enemyPrefab.name,
                spawnPoints[pointIndex].position,
                Quaternion.identity,
                transform
            );

            spawnPoint.SetObject(enemy);
            spawned++;
        }
    }

    private void SpawnBossPhase(DistancePhase phase)
    {
        if (phase.bossEnemy == null)
            return;

        Transform bossPoint = GetBossSpawnPoint(phase);
        if (bossPoint == null)
            return;

        SpawnPoint spawnPoint = bossPoint.GetComponent<SpawnPoint>();
        if (spawnPoint == null)
            return;

        if (spawnPoint.GetObject() != null)
            return;

        GameObject boss = ObjectPool.instance.Spawn(
            phase.bossEnemy.name,
            bossPoint.position,
            Quaternion.identity,
            transform
        );

        spawnPoint.SetObject(boss);
    }

    private Transform GetBossSpawnPoint(DistancePhase phase)
    {
        if (phase.bossSpawnPoint != null)
            return phase.bossSpawnPoint;

        if (spawnPoints == null || spawnPoints.Length == 0)
            return null;

        return spawnPoints[spawnPoints.Length / 2];
    }

    private GameObject PickEnemyPrefab(DistancePhase phase)
    {
        bool spawnStrong = phase.strongEnemies != null &&
                           phase.strongEnemies.Length > 0 &&
                           Random.value <= phase.strongEnemyChance;

        GameObject[] source = spawnStrong ? phase.strongEnemies : phase.normalEnemies;

        if (source == null || source.Length == 0)
            return null;

        int index = Random.Range(0, source.Length);
        return source[index];
    }

    private int GetSpawnCountByDistance(DistancePhase phase, float playerDistance)
    {
        float localDistance = Mathf.Max(0f, playerDistance - phase.startDistance);
        float normalized = phase.distanceWindow <= 0f ? 1f : Mathf.Clamp01(localDistance / phase.distanceWindow);

        float curveValue = phase.spawnCountCurve != null ? phase.spawnCountCurve.Evaluate(normalized) : 1f;
        int bonus = Mathf.RoundToInt(curveValue * phase.extraSpawnCountByCurve);

        return Mathf.Max(0, phase.baseEnemySpawnCount + bonus);
    }

    private DistancePhase GetCurrentPhase(float playerDistance)
    {
        if (phases == null || phases.Count == 0)
            return null;

        DistancePhase result = null;

        for (int i = 0; i < phases.Count; i++)
        {
            if (playerDistance >= phases[i].startDistance)
                result = phases[i];
            else
                break;
        }

        return result;
    }

    private List<int> BuildAvailableSpawnIndices()
    {
        List<int> indices = new();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null)
                continue;

            indices.Add(i);
        }

        return indices;
    }

    // Fisher-Yates shuffle
    private void ShuffleIndices(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    private void ClearSpawnPoints()
    {
        foreach (var point in spawnPoints)
        {
            if (point == null)
                continue;

            SpawnPoint spawnPoint = point.GetComponent<SpawnPoint>();
            if (spawnPoint == null)
                continue;

            GameObject current = spawnPoint.GetObject();
            if (current != null)
            {
                ObjectPool.instance.Despawn(current);
                spawnPoint.SetObject(null);
            }
        }
    }

    private void RandomSpawn()
    {
        if (randomObjects == null || randomObjects.Length == 0 || spawnPoints == null || spawnPoints.Length == 0)
            return;

        for (int i = 0; i < randomSpawnCount; i++)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            int randomObjectIndex = Random.Range(0, randomObjects.Length);

            SpawnPoint spawnPoint = spawnPoints[randomSpawnIndex].GetComponent<SpawnPoint>();
            if (spawnPoint == null || spawnPoint.GetObject() != null)
                continue;

            GameObject obj = ObjectPool.instance.Spawn(
                randomObjects[randomObjectIndex].name,
                spawnPoints[randomSpawnIndex].position,
                Quaternion.identity,
                transform
            );

            spawnPoint.SetObject(obj);
        }
    }
}