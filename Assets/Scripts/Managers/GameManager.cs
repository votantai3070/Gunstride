using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Action<int> OnCoinChanged;

        [Header("Game Settings")]
        public float waitTimeStart = 3f;
        public int Coin { get; private set; } = 0;
        public float PlayerDistance { get; private set; } = 0f;

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
        public bool IsGameOver() => isGameOver;
    }
}
