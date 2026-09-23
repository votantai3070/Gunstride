using System;
using UnityEngine;

public class CoinManager : MonoBehaviour, ISaveable
{
    public static CoinManager Instance;

    public static event Action<int> OnCoinChanged;
    public int TakenCoins { get; private set; } = 0;

    public int totalCoins = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddCoin(int coin)
    {
        TakenCoins += coin;
        OnCoinChanged?.Invoke(TakenCoins);
    }


    public void AddTotalCoin(int amount)
    {
        totalCoins += amount;
        ResetTakenCoin();
    }

    private void ResetTakenCoin()
    {
        TakenCoins = 0;
    }

    public void RemoveCoin(int coin)
    {
        if (totalCoins >= coin)
        {
            totalCoins -= coin;
            OnCoinChanged?.Invoke(totalCoins);
        }
    }

    public bool CanSpendCoin(int amount) => totalCoins >= amount;


    public void LoadData(GameData data)
    {
        totalCoins = data.coins;

        Debug.Log($"Loaded coins: {totalCoins}");
    }

    public void SaveData(ref GameData data)
    {
        data.coins = totalCoins;

        Debug.Log($"Saved coins: {data.coins}");
    }
}
