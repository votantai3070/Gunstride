using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public HealthBarUI HealthBarUI { get; private set; }

    private void Awake()
    {
        HealthBarUI = GetComponentInChildren<HealthBarUI>();
    }
}
