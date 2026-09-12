using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] private int coinAmount = 1;

    public void DropCoin(Vector3 position)
    {
        GameObject coin = ObjectPool.Instance.Spawn(coinPrefab.name, position, Quaternion.identity);

        if (coin.TryGetComponent<Coin_Item>(out var coinItem))
            coinItem.SetupCoin(coinAmount);
    }
}
