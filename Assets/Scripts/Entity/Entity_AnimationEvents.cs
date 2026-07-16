using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    void AttackTrigger()
    {
        entity.isTrigger = true;
    }

    void AttackPointTrigger()
    {
        entity.isAttack = true;
    }
}
