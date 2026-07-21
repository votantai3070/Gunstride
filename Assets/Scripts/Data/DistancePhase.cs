using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DistancePhase
{
    [Header("Phase")]
    public string phaseName = "Phase";
    public float startDistance = 0f;

    [Header("Enemy Groups")]
    public int baseEnemySpawnCount = 1;
    public float distanceWindow = 500f;
    public int extraSpawnCountByCurve = 2;
    [Space]
    public AnimationCurve spawnCountCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public List<WeightedPrefab> normalEnemies = new();
    public List<WeightedPrefab> strongEnemies = new();
    [Range(0f, 1f)] public float strongEnemyChance = 0f;

    [Header("Pickup Item Groups")]
    public int pickupSpawnCount = 1;
    [Space]
    public List<WeightedPrefab> pickupObjects = new();

    [Header("Obstacle Groups")]
    public int obstacleSpawnCount = 1;
    [Space]
    public List<WeightedPrefab> obstacleObjects = new();

    [Header("Boss Phase")]
    public bool spawnBossOnly = false;
    public GameObject bossEnemy;
    public Transform bossSpawnPoint;
}