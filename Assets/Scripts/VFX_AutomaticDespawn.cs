using UnityEngine;

public class VFX_AutomaticDespawn : MonoBehaviour
{
    public void DespawnObject()
    {
        ObjectPool.Instance.Despawn(gameObject);
    }
}
