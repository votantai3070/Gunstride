using UnityEngine;

public class VFX_AutomationEffect : MonoBehaviour
{
    [SerializeField] private GameObject effectGo;
    [SerializeField] private float effectDuration = 1f;

    public void CreateEffect(Transform target)
    {
        GameObject effect = ObjectPool.instance.Spawn(
            effectGo.name,
            target.position,
            Quaternion.identity,
            target
        );

        Debug.Log("effect: " + effect);

        if (effect.TryGetComponent<VFX_AutomationEffectItem>(out var effectItem))
        {
            effectItem.Play(target, effectDuration);
        }
    }
}