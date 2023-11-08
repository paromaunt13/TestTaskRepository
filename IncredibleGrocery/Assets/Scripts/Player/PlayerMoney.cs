using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    public  Action<int> OnMoneyValueChanged;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    public void AddMoney(int moneyAmount)
    {
        PersistentDataManager.MoneyAmount += moneyAmount;
        OnMoneyValueChanged?.Invoke(PersistentDataManager.MoneyAmount);
    }
}