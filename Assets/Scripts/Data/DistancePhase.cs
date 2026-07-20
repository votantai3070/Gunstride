using System;
using UnityEngine;

[Serializable]
public class DistancePhase
{
    [Header("Phase")]
    public string phaseName = "Phase";
    public float startDistance = 0f;

    [Header("Enemy Groups")]
    public GameObject[] normalEnemies;
    public GameObject[] strongEnemies;

    [Range(0f, 1f)] public float strongEnemyChance = 0f;

    [Header("Spawn Count")]
    public int baseEnemySpawnCount = 1;
    public float distanceWindow = 500f;
    public int extraSpawnCountByCurve = 2;
    public AnimationCurve spawnCountCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Boss Phase")]
    public bool spawnBossOnly = false;
    public GameObject bossEnemy;
    public Transform bossSpawnPoint;
}
