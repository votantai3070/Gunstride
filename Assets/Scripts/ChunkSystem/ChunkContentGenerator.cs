using System.Collections.Generic;
using UnityEngine;

public class ChunkContentGenerator : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Distance Phases")]
    [SerializeField] private List<DistancePhase> phases = new();

    [Header("Debug")]
    [SerializeField] private bool showDebugLog;

    public void Generate(float playerDistance)
    {
        ClearSpawnPoints();
        SpawnByDistance(playerDistance);
    }

    public void Regenerate(float playerDistance)
    {
        ClearSpawnPoints();
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
        List<int> availableIndices = BuildAvailableSpawnIndices();
        ShuffleIndices(availableIndices);

        int enemyCount = Mathf.Min(GetSpawnCountByDistance(phase, playerDistance), availableIndices.Count);
        int phaseObstacleCount = Mathf.Max(0, phase.obstacleSpawnCount);
        int phasePickupCount = Mathf.Max(0, phase.pickupSpawnCount);

        SpawnWeightedGroup(availableIndices, phase.pickupObjects, phasePickupCount);
        SpawnEnemyGroup(availableIndices, enemyCount, phase);
        SpawnWeightedGroup(availableIndices, phase.obstacleObjects, phaseObstacleCount);
    }

    private void SpawnEnemyGroup(List<int> availableIndices, int spawnCount, DistancePhase phase)
    {
        int spawned = 0;

        for (int i = availableIndices.Count - 1; i >= 0 && spawned < spawnCount; i--)
        {
            int pointIndex = availableIndices[i];
            SpawnPoint spawnPoint = spawnPoints[pointIndex].GetComponent<SpawnPoint>();

            if (spawnPoint == null || spawnPoint.GetObject() != null)
                continue;

            GameObject enemyPrefab = PickEnemyPrefab(phase);
            if (enemyPrefab == null)
                continue;

            GameObject enemy = ObjectPool.Instance.Spawn(
                enemyPrefab.name,
                spawnPoints[pointIndex].position,
                Quaternion.identity,
                transform
            );

            spawnPoint.SetObject(enemy);
            availableIndices.RemoveAt(i);
            spawned++;
        }
    }

    private void SpawnWeightedGroup(List<int> availableIndices, List<WeightedPrefab> prefabs, int spawnCount)
    {
        if (prefabs == null || prefabs.Count == 0 || spawnCount <= 0)
            return;

        int spawned = 0;

        for (int i = availableIndices.Count - 1; i >= 0 && spawned < spawnCount; i--)
        {
            int pointIndex = availableIndices[i];
            SpawnPoint spawnPoint = spawnPoints[pointIndex].GetComponent<SpawnPoint>();

            if (spawnPoint == null || spawnPoint.GetObject() != null)
                continue;

            GameObject prefab = WeightedRandomUtility.PickPrefab(prefabs);
            if (prefab == null)
                continue;

            GameObject obj = ObjectPool.Instance.Spawn(
                prefab.name,
                spawnPoints[pointIndex].position,
                Quaternion.identity,
                transform
            );

            spawnPoint.SetObject(obj);
            availableIndices.RemoveAt(i);
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
        if (spawnPoint == null || spawnPoint.GetObject() != null)
            return;

        GameObject boss = ObjectPool.Instance.Spawn(
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
        bool spawnStrong = phase.strongEnemies != null
            && phase.strongEnemies.Count > 0
            && Random.value <= phase.strongEnemyChance;

        List<WeightedPrefab> source = spawnStrong ? phase.strongEnemies : phase.normalEnemies;
        return WeightedRandomUtility.PickPrefab(source);
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
                ObjectPool.Instance.Despawn(current);
                spawnPoint.SetObject(null);
            }
        }
    }
}