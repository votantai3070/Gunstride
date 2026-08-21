using UnityEngine;

public class Utils
{
    public void Flipped(bool canFlip, Transform target)
    {
        if (canFlip)
            target.Rotate(0, 180, 0);
        else
            target.Rotate(0, 0, 0);
    }

    public bool CanAttack(float lastTimeAttack, float duration)
    {
        return Time.time > lastTimeAttack + duration;
    }
}
