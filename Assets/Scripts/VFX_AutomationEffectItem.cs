using System.Collections;
using UnityEngine;

public class VFX_AutomationEffectItem : MonoBehaviour
{
    private Animator anim;
    private Coroutine despawnCoroutine;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Play(Transform target, float duration)
    {
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }

        if (despawnCoroutine != null)
            StopCoroutine(despawnCoroutine);

        despawnCoroutine = StartCoroutine(DespawnRoutine(duration));
    }

    private IEnumerator DespawnRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        ObjectPool.Instance.Despawn(gameObject);
        despawnCoroutine = null;
    }

    private void OnDisable()
    {
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }
    }
}