using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle - ", menuName = "Gunstrike Data/Obstacle Data/Obstacle")]
public class ObstacleDataSO : ScriptableObject
{
    public int damage;
    public float speed;
    public float duration;
}
