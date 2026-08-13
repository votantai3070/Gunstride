using System.Collections.Generic;
using UnityEngine;

public class VFX_AutomationEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> effectGo;
    [SerializeField] private float effectDuration = 1f;

    public void SetupEffectGo(List<GameObject> effectGo, float effectDuration)
    {
        this.effectGo = effectGo;
        this.effectDuration = effectDuration;
    }

    public void CreateEffect(Transform target)
    {
        foreach (var go in effectGo)
        {
            GameObject effect = ObjectPool.Instance.Spawn(go.name, target.position, Quaternion.identity, target);

            if (effect.TryGetComponent<VFX_AutomationEffectItem>(out var effectItem))
            {
                effectItem.Play(effectDuration);
            }
        }
    }
}