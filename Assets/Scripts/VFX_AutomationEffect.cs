using UnityEngine;

public class VFX_AutomationEffect : MonoBehaviour
{
    [SerializeField] private GameObject effectGo;
    [SerializeField] private float effectDuration = 1f;

    public void SetupEffectGo(GameObject effectGo, float effectDuration)
    {
        this.effectGo = effectGo;
        this.effectDuration = effectDuration;
    }

    public void CreateEffect(Transform target)
    {
        GameObject effect = ObjectPool.Instance.Spawn(effectGo.name, target.position, Quaternion.identity, target);

        if (effect.TryGetComponent<VFX_AutomationEffectItem>(out var effectItem))
        {
            effectItem.Play(target, effectDuration);
        }
    }
}