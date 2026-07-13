using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    [SerializeField] private float[] laneY = { -3f, 0f, 3f };
    [SerializeField] private float laneChangeSpeed = 12f;

    private int currentLaneIndex = 1;
    private int targetLaneIndex = 1;

    private void Update()
    {
        MoveToLane();
    }

    public void ChangeLane(int direction)
    {
        targetLaneIndex = Mathf.Clamp(targetLaneIndex + direction, 0, laneY.Length - 1);
    }

    private void MoveToLane()
    {
        Vector3 pos = transform.position;
        float targetY = laneY[targetLaneIndex];

        pos.y = Mathf.MoveTowards(pos.y, targetY, laneChangeSpeed * Time.deltaTime);
        transform.position = pos;

        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            pos.y = targetY;
            transform.position = pos;
            currentLaneIndex = targetLaneIndex;
        }
    }

    public int GetCurrentLane()
    {
        return currentLaneIndex;
    }
}