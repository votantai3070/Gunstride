using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour, ISaveable
{
    public static AchievementManager Instance { get; private set; }

    [Header("Data")]
    [SerializeField] private Achievement_ListDataSO achievementListDataSO;

    public event Action<AchievementDataSO> OnAchievementCompleted;
    public event Action<AchievementDataSO> OnAchievementClaimed;
    public event Action OnAchievementProgressChanged;

    private readonly Dictionary<string, AchievementDataSO> achievementById = new();

    // Chỉ giữ progress của achievement PerRun.
    // Không save data này vào GameData.
    private readonly Dictionary<string, int> runProgress = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildAchievementLookup();
    }

    private void BuildAchievementLookup()
    {
        achievementById.Clear();

        if (achievementListDataSO == null ||
            achievementListDataSO.achievementList == null)
        {
            Debug.LogError(
                "[AchievementManager] Achievement list is null."
            );

            return;
        }

        foreach (AchievementDataSO achievement
                 in achievementListDataSO.achievementList)
        {
            if (achievement == null)
                continue;

            if (string.IsNullOrWhiteSpace(achievement.AchievementID))
            {
                Debug.LogError(
                    $"[AchievementManager] '{achievement.name}' " +
                    "has no Achievement ID."
                );

                continue;
            }

            if (achievementById.ContainsKey(achievement.AchievementID))
            {
                Debug.LogError(
                    "[AchievementManager] Duplicate Achievement ID: " +
                    achievement.AchievementID
                );

                continue;
            }

            achievementById.Add(
                achievement.AchievementID,
                achievement
            );
        }

        Debug.Log(
            $"[AchievementManager] Loaded " +
            $"{achievementById.Count} achievement definitions."
        );
    }

    #region Save / Load

    public void LoadData(GameData data)
    {
        if (data == null)
        {
            Debug.LogError("[AchievementManager] GameData is null.");
            return;
        }

        data.achievementProgress ??=
            new SerializableDictionary<string, int>();

        data.achievementCompleted ??=
            new SerializableDictionary<string, bool>();

        data.achievementRewardClaimed ??=
            new SerializableDictionary<string, bool>();

        // Progress PerRun không được load từ file.
        // Khi mở game/run mới, nó phải bắt đầu lại từ 0.
        runProgress.Clear();

        Debug.Log(
            $"[AchievementManager] Loaded save data | " +
            $"Progress: {data.achievementProgress.Count} | " +
            $"Completed: {data.achievementCompleted.Count} | " +
            $"Claimed: {data.achievementRewardClaimed.Count}"
        );

        OnAchievementProgressChanged?.Invoke();
    }

    public void SaveData(ref GameData data)
    {
        if (data == null)
        {
            data = new GameData();
        }

        // Vì AchievementManager update trực tiếp GameData trong:
        // SetProgress(), CompleteAchievement(), ClaimReward(),
        // nên không cần copy dictionary vào đây.
        // Chỉ đảm bảo các dictionary luôn tồn tại trước khi file được ghi.

        data.achievementProgress ??=
            new SerializableDictionary<string, int>();

        data.achievementCompleted ??=
            new SerializableDictionary<string, bool>();

        data.achievementRewardClaimed ??=
            new SerializableDictionary<string, bool>();

        Debug.Log(
            $"[AchievementManager] SaveData called | " +
            $"Progress: {data.achievementProgress.Count} | " +
            $"Completed: {data.achievementCompleted.Count} | " +
            $"Claimed: {data.achievementRewardClaimed.Count}"
        );
    }

    #endregion

    #region Read Achievement State

    public int GetProgress(AchievementDataSO achievement)
    {
        if (!IsValidAchievement(achievement))
            return 0;

        // Nếu achievement complete thì UI luôn hiển thị full progress.
        if (IsCompleted(achievement))
            return achievement.targetValue;

        string id = achievement.AchievementID;

        if (achievement.achievementScope == AchievementScope.PerRun)
        {
            return runProgress.TryGetValue(
                id,
                out int currentRunProgress
            )
                ? currentRunProgress
                : 0;
        }

        GameData data = SaveManager.instance.GetGameData();

        if (data.achievementProgress == null)
            return 0;

        return data.achievementProgress.TryGetValue(
            id,
            out int totalProgress
        )
            ? totalProgress
            : 0;
    }

    public bool IsCompleted(AchievementDataSO achievement)
    {
        if (!IsValidAchievement(achievement))
            return false;

        GameData data = SaveManager.instance.GetGameData();

        if (data == null || data.achievementCompleted == null)
            return false;

        return data.achievementCompleted.TryGetValue(
            achievement.AchievementID,
            out bool completed
        ) && completed;
    }

    public bool IsRewardClaimed(AchievementDataSO achievement)
    {
        if (!IsValidAchievement(achievement))
            return false;

        GameData data = SaveManager.instance.GetGameData();

        if (data == null || data.achievementRewardClaimed == null)
            return false;

        return data.achievementRewardClaimed.TryGetValue(
            achievement.AchievementID,
            out bool claimed
        ) && claimed;
    }

    public AchievementState GetState(AchievementDataSO achievement)
    {
        if (achievement == null)
            return AchievementState.NotStarted;

        if (IsRewardClaimed(achievement))
            return AchievementState.Claimed;

        if (IsCompleted(achievement))
            return AchievementState.Completed;

        return GetProgress(achievement) <= 0
            ? AchievementState.NotStarted
            : AchievementState.InProgress;
    }

    #endregion

    #region Update Progress

    public void AddProgress(
        AchievementDataSO achievement,
        int amount = 1)
    {
        if (!IsValidAchievement(achievement) || amount <= 0)
            return;

        if (IsCompleted(achievement))
            return;

        int currentProgress = GetProgress(achievement);

        SetProgress(
            achievement,
            currentProgress + amount
        );
    }

    public void SetProgress(
        AchievementDataSO achievement,
        int value)
    {
        if (!IsValidAchievement(achievement))
            return;

        if (IsCompleted(achievement))
            return;

        int currentProgress = GetProgress(achievement);

        int newProgress = Mathf.Clamp(
            value,
            0,
            achievement.targetValue
        );

        // Không thay đổi thì không cần refresh UI.
        if (newProgress == currentProgress)
            return;

        string id = achievement.AchievementID;

        if (achievement.achievementScope == AchievementScope.PerRun)
        {
            runProgress[id] = newProgress;
        }
        else
        {
            GameData data = SaveManager.instance.GetGameData();

            data.achievementProgress[id] = newProgress;
        }

        if (newProgress >= achievement.targetValue)
        {
            CompleteAchievement(achievement);
        }

        // Gọi duy nhất ở đây khi progress/state thay đổi.
        OnAchievementProgressChanged?.Invoke();
    }

    private void CompleteAchievement(AchievementDataSO achievement)
    {
        if (IsCompleted(achievement))
            return;

        GameData data = SaveManager.instance.GetGameData();

        data.achievementCompleted[achievement.AchievementID] = true;

        Debug.Log(
            $"[AchievementManager] Completed: " +
            achievement.achievementName
        );

        // Dùng cho popup / sound / VFX achievement unlock.
        OnAchievementCompleted?.Invoke(achievement);
    }

    public bool ClaimReward(AchievementDataSO achievement)
    {
        if (!IsValidAchievement(achievement))
            return false;

        if (!IsCompleted(achievement))
        {
            Debug.LogWarning(
                $"[AchievementManager] Cannot claim incomplete: " +
                achievement.achievementName
            );

            return false;
        }

        if (IsRewardClaimed(achievement))
        {
            Debug.LogWarning(
                $"[AchievementManager] Reward already claimed: " +
                achievement.achievementName
            );

            return false;
        }

        GameData data = SaveManager.instance.GetGameData();

        data.coins += achievement.coinReward;

        data.achievementRewardClaimed[achievement.AchievementID] = true;

        Debug.Log(
            $"[AchievementManager] Reward claimed: " +
            $"{achievement.achievementName} | " +
            $"+{achievement.coinReward} coins"
        );

        // Dùng cho coin animation, SFX reward và refresh Coin UI.
        OnAchievementClaimed?.Invoke(achievement);

        // State đổi Completed -> Claimed.
        OnAchievementProgressChanged?.Invoke();

        // Claim reward là thay đổi quan trọng, save ngay.
        SaveManager.instance.SaveGame();

        return true;
    }

    #endregion

    #region Gameplay Reports

    /// <summary>
    /// Dùng cho event xảy ra một lần:
    /// enemy chết, nhặt coin, phá obstacle, mua weapon...
    /// Hàm này cập nhật cả Total và PerRun nếu có achievement cùng type.
    /// </summary>
    public void ReportEvent(
        AchievementType achievementType,
        int amount = 1)
    {
        if (amount <= 0)
            return;

        if (achievementListDataSO == null ||
            achievementListDataSO.achievementList == null)
        {
            return;
        }

        foreach (AchievementDataSO achievement
                 in achievementListDataSO.achievementList)
        {
            if (achievement == null)
                continue;

            if (achievement.achievementType != achievementType)
                continue;

            AddProgress(achievement, amount);
        }
    }

    /// <summary>
    /// Dùng cho giá trị hiện tại của run:
    /// distance, thời gian sống, kill hiện tại trong run...
    /// Không dùng để cộng dồn.
    /// </summary>
    public void ReportRunValue(
        AchievementType achievementType,
        int currentValue)
    {
        if (achievementListDataSO == null ||
            achievementListDataSO.achievementList == null)
        {
            return;
        }

        foreach (AchievementDataSO achievement
                 in achievementListDataSO.achievementList)
        {
            if (achievement == null)
                continue;

            if (achievement.achievementType != achievementType)
                continue;

            if (achievement.achievementScope != AchievementScope.PerRun)
                continue;

            SetProgress(achievement, currentValue);
        }
    }

    #endregion

    #region Run Lifecycle

    public void StartNewRun()
    {
        // Dọn progress PerRun cũ nếu restart/new run.
        runProgress.Clear();

        OnAchievementProgressChanged?.Invoke();

        Debug.Log("[AchievementManager] New run started.");
    }

    public void EndRun()
    {
        if (achievementListDataSO == null ||
            achievementListDataSO.achievementList == null)
        {
            return;
        }

        foreach (AchievementDataSO achievement
                 in achievementListDataSO.achievementList)
        {
            if (achievement == null)
                continue;

            if (achievement.achievementScope != AchievementScope.PerRun)
                continue;

            // Achievement PerRun đã complete có completed flag
            // trong GameData nên không mất sau khi clear runProgress.
            if (IsCompleted(achievement))
                continue;

            runProgress.Remove(achievement.AchievementID);
        }

        OnAchievementProgressChanged?.Invoke();

        // Không SaveGame ở đây.
        // Caller, ví dụ GameManager.EndGame(), sẽ save một lần.
        Debug.Log("[AchievementManager] Per-run progress reset.");
    }

    #endregion

    #region Validation

    private bool IsValidAchievement(AchievementDataSO achievement)
    {
        if (achievement == null)
            return false;

        if (string.IsNullOrWhiteSpace(achievement.AchievementID))
        {
            Debug.LogWarning(
                $"[AchievementManager] Achievement '{achievement.name}' " +
                "has an empty ID."
            );

            return false;
        }

        if (SaveManager.instance == null)
        {
            Debug.LogError(
                "[AchievementManager] SaveManager instance is missing."
            );

            return false;
        }

        if (SaveManager.instance.GetGameData() == null)
        {
            Debug.LogWarning(
                "[AchievementManager] GameData is not loaded yet."
            );

            return false;
        }

        return true;
    }

    #endregion
}