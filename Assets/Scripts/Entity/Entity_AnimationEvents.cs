using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void AttackTrigger()
    {
        entity.isTrigger = true;
    }

    public void AttackPointTrigger()
    {
        entity.isAttack = true;
    }
}
