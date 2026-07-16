using UnityEngine;

public class Utils
{
    public void FlipLeft(Transform target)
    {
        target.Rotate(0, 180, 0);
    }
}
