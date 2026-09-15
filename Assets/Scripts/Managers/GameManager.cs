using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour, ISaveable
    {
        public static GameManager Instance { get; private set; }
        public Action<int> OnCoinChanged;

        private bool dataLoaded;

        [Header("Game Settings")]
        [SerializeField] float waitTimeStart = 3f;

        // Coin đã nhặt trong level hiện tại (chưa save)
        public int TakenCoins { get; private set; } = 0;

        // Tổng coin đã save từ các lần chơi trước
        public int TotalCoins { get; private set; } = 0;

        public float PlayerDistance { get; private set; } = 0f;

        private bool isGameStarted = false;
        [SerializeField] private float waitTimer;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (waitTimer > 0 && !isGameStarted)
                waitTimer -= Time.deltaTime;

            if (isGameStarted) return;
            if (waitTimer <= 0 && !isGameStarted)
                isGameStarted = true;
        }

        public void ResetValue()
        {
            isGameStarted = false;
            waitTimer = waitTimeStart;
            Time.timeScale = 1f;
        }

        public void ChangeScene(string sceneName)
        {
            SaveManager.instance.SaveGame();
            StartCoroutine(ChangeSceneCo(sceneName));
        }

        private IEnumerator ChangeSceneCo(string sceneName)
        {
            UI_FadeScreen fadeScreen = FindFadeScreenUI();

            fadeScreen.FadeOut();

            yield return fadeScreen.fadeEffectCo;

            SceneManager.LoadScene(sceneName);

            dataLoaded = false;
            yield return null;

            while (dataLoaded == false)
                yield return null;

            fadeScreen = FindFadeScreenUI();
            fadeScreen.FadeIn();
        }

        private UI_FadeScreen FindFadeScreenUI()
        {
            if (UI.Instance != null)
                return UI.Instance.FadeUI;
            else
                return FindFirstObjectByType<UI_FadeScreen>();
        }

        public void UpdateDistance(float distance)
        {
            PlayerDistance = distance;
            UI.Instance.IngameUI.UpdateDistance(distance);
        }

        public void AddCoin(int coin)
        {
            TakenCoins += coin;
            OnCoinChanged?.Invoke(TakenCoins);
        }

        public void RemoveCoin(int coin)
        {
            if (TotalCoins >= coin)
            {
                TotalCoins -= coin;
                OnCoinChanged?.Invoke(TotalCoins);
                SaveManager.instance.SaveGame();
            }
        }

        public bool CanSpendCoin(int amount)
        {
            return TotalCoins >= amount;
        }

        public bool IsGameStarted() => isGameStarted;

        public void LoadData(GameData data)
        {
            TotalCoins = data.coins;
            TakenCoins = 0; // Reset coin chưa save khi load game mới
            dataLoaded = true;

            Debug.Log($"Loaded coins: {TotalCoins}");
        }

        public void SaveData(ref GameData data)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "MainMenu")
                return;

            // Cộng coin đã nhặt trong level hiện tại vào tổng
            TotalCoins += TakenCoins;
            TakenCoins = 0;

            data.coins = TotalCoins;
            dataLoaded = false;

            Debug.Log($"Saved coins: {data.coins}");
        }
    }
}