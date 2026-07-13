using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float waitTimeStart = 3f;

    private bool isGameStarted = false;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (waitTimeStart > 0 && !isGameStarted)
            waitTimeStart -= Time.deltaTime;
        else if (!isGameOver && !isGameStarted)
            isGameStarted = true;
    }


    private void StartGame()
    {

    }

    public bool IsGameStarted() => isGameStarted;
    public bool IsGameOver() => isGameOver;
}
