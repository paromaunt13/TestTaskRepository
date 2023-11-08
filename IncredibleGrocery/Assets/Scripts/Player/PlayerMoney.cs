using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;
    
    public int MoneyAmount { get; private set; }

    public static Action<int> OnMoneyValueChanged;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        
        Instance = this;

        LoadMoneyData();
    }

    private void LoadMoneyData()
    {
        MoneyAmount = PersistentDataManager.MoneyAmount;
        OnMoneyValueChanged?.Invoke(MoneyAmount);
    }

    public void AddMoney(int moneyAmount)
    {
        MoneyAmount += moneyAmount;
        OnMoneyValueChanged?.Invoke(MoneyAmount);
        PersistentDataManager.MoneyAmount = MoneyAmount;
    }
}