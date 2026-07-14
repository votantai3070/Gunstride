using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    public void SetObject(GameObject obj = null) => this.obj = obj;
    public GameObject GetObject() => obj;
}
