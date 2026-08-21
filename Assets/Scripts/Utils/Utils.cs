using UnityEngine;

public class Utils
{
    public void Flipped(bool canFlip, Transform target)
    {
        target.localRotation = Quaternion.Euler(0f, canFlip ? 180f : 0f, 0f);
    }

    public bool CanAttack(float lastTimeAttack, float duration)
    {
        return Time.time > lastTimeAttack + duration;
    }
}
