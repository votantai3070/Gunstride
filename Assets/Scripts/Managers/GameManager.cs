using System;
using UnityEngine;

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
