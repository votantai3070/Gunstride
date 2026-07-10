using UnityEngine;

public class ChunkContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleObjects;
    [SerializeField] private GameObject[] enemyObjects;
    [SerializeField] private GameObject[] pickupObjects;

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