using Managers;
using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] private float[] laneY = { -3f, 0f, 3f };
    [SerializeField] private float laneChangeSpeed = 12f;

    private int currentLaneIndex = 1;
    private int targetLaneIndex = 1;

    public bool isChangingLane;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MoveToLane();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void ChangeLane(int direction)
    {
        targetLaneIndex = Mathf.Clamp(targetLaneIndex + direction, 0, laneY.Length - 1);
    }

    public void Movement()
    {
        if (!GameManager.Instance.IsGameStarted()) return;

        float directX = player.IsFlipped() ? -1 : 1;
        player.rb.linearVelocityX = directX * player.speed;
    }

    private void MoveToLane()
    {
        Vector3 pos = transform.position;
        float targetY = laneY[targetLaneIndex];

        pos.y = Mathf.MoveTowards(pos.y, targetY, laneChangeSpeed * Time.deltaTime);
        transform.position = pos;

        isChangingLane = Mathf.Abs(transform.position.y - targetY) > 0.01f;

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