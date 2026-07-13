using UnityEngine;

public class ChunkContentGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] obstacleObjects;
    [SerializeField] private GameObject[] enemyObjects;
    [SerializeField] private GameObject[] pickupObjects;

    [SerializeField] private GameObject[] randomObjects;
    [SerializeField] private int randomSpawnCount = 3;

    public void Generate()
    {
        RandomizeChildren();
    }

    public void Regenerate()
    {
        RandomizeChildren();
    }

    private void RandomizeChildren()
    {
        SetRandomGroup(obstacleObjects, 0.6f);
        SetRandomGroup(enemyObjects, 0.5f);
        SetRandomGroup(pickupObjects, 0.4f);
    }

    private void RandomSpawn()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        int randomObjectIndex = Random.Range(0, randomObjects.Length);
        for (int i = 0; i < randomSpawnCount; i++)
        {
            GameObject obj = Instantiate(randomObjects[randomObjectIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
        }
    }

    private void SetRandomGroup(GameObject[] objects, float chance)
    {
        if (objects == null) return;

        for (int i = 0; i < objects.Length; i++)
        {
            bool active = Random.value < chance;
            objects[i].SetActive(active);
        }
    }
}