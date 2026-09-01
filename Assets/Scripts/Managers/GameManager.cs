using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Action<int> OnCoinChanged;

        [Header("Game Settings")]
        [SerializeField] float waitTimeStart = 3f;
        public int Coin { get; private set; } = 0;
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
            //SaveManager.instance.SaveGame();
            StartCoroutine(ChangeSceneCo(sceneName));
        }

        private IEnumerator ChangeSceneCo(string sceneName)
        {
            UI_FadeScreen fadeScreen = FindFadeScreenUI();

            fadeScreen.FadeOut(); // transparent -> black

            yield return fadeScreen.fadeEffectCo;

            SceneManager.LoadScene(sceneName);

            //dataLoaded = false; // data loaded becomes true when you load game from save manager
            yield return null;

            //while (dataLoaded == false)
            //    yield return null;

            fadeScreen = FindFadeScreenUI();
            fadeScreen.FadeIn(); // black -> transparent

            //if (player == null)
            //    yield break;
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
            Coin += coin;
            OnCoinChanged?.Invoke(Coin);
        }
        public void RemoveCoin(int coin)
        {
            Coin -= coin;
            OnCoinChanged?.Invoke(Coin);
        }

        public bool IsGameStarted() => isGameStarted;

        //private bool 
    }
}
