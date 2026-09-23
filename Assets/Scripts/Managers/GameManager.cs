using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour, ISaveable
    {
        public static GameManager Instance;

        private bool dataLoaded;

        [Header("Game Settings")]
        [SerializeField] float waitTimeStart = 3f;

        public float PlayerDistance { get; private set; } = 0f;
        public int EnemiesDefeated { get; private set; }

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
            ResetValue();
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

        public void AddEnemies()
        {
            EnemiesDefeated += 1;
        }

        public bool IsGameStarted() => isGameStarted;

        public void LoadData(GameData data)
        {
            dataLoaded = true;
        }

        public void SaveData(ref GameData data)
        {
            dataLoaded = false;
        }
    }
}