using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private FileDataHandler dataHandler;
    private GameData gameData;

    private readonly List<ISaveable> allSaveables = new();

    [SerializeField] private string fileName = "save.json";
    [SerializeField] private bool encryption = true;

    private bool hasLoadedFromFile;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        dataHandler = new FileDataHandler(
            Application.persistentDataPath,
            fileName,
            encryption
        );
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshSaveables();

        if (!hasLoadedFromFile)
        {
            LoadGameFromFile();
            hasLoadedFromFile = true;
        }
        else
        {
            // Scene mới: không đọc file lại.
            // Chỉ apply data đang có trong RAM.
            ApplyGameDataToSaveables();
        }
    }

    private void RefreshSaveables()
    {
        allSaveables.Clear();

        MonoBehaviour[] behaviours =
            FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour is ISaveable saveable)
                allSaveables.Add(saveable);
        }

        Debug.Log(
            $"[SaveManager] Found {allSaveables.Count} saveable objects."
        );
    }

    public void LoadGameFromFile()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is NULL.");
            return;
        }

        gameData = dataHandler.LoadData();

        if (gameData == null)
        {
            Debug.Log("No save data found. Creating new GameData.");
            gameData = new GameData();
        }

        ApplyGameDataToSaveables();

        Debug.Log(
            $"[SaveManager] Loaded. Coins = {gameData.coins}"
        );
    }

    private void ApplyGameDataToSaveables()
    {
        if (gameData == null)
            return;

        for (int i = 0; i < allSaveables.Count; i++)
        {
            if (allSaveables[i] is UnityEngine.Object unityObject &&
                unityObject == null)
            {
                continue;
            }

            allSaveables[i].LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is NULL.");
            return;
        }

        if (gameData == null)
            gameData = new GameData();

        RefreshSaveables();

        for (int i = 0; i < allSaveables.Count; i++)
        {
            if (allSaveables[i] is UnityEngine.Object unityObject &&
                unityObject == null)
            {
                continue;
            }

            allSaveables[i].SaveData(ref gameData);
        }

        dataHandler.SaveData(gameData);

        Debug.Log(
            $"[SaveManager] Saved. Coins = {gameData.coins}"
        );
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    [ContextMenu("Delete Save Data")]
    public void DeleteSaveData()
    {
        dataHandler.Delete();

        gameData = new GameData();

        RefreshSaveables();
        ApplyGameDataToSaveables();

        hasLoadedFromFile = true;
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }
}