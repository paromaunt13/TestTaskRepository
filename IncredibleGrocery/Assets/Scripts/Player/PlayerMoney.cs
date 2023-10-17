using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    private const string MoneyKey = "PlayerMoney";
    public int MoneyAmount { get; set; }

    private void Awake()
    {
        Instance = this;

        CheckForMoney();      
    }

    private void CheckForMoney()
    {
        if (PersistentDataManager.CheckKey(MoneyKey))
        {
            LoadMoneyData();
        }
        else
        {
            MoneyAmount = 0;
            EventBus.OnMoneyValueChanged?.Invoke(MoneyAmount);
        }
    }

    private void LoadMoneyData()
    {
        MoneyAmount = PersistentDataManager.GetInt(MoneyKey);
        EventBus.OnMoneyValueChanged?.Invoke(MoneyAmount);
    }

    private void SaveMoneyData()
    {
        PersistentDataManager.SetInt(MoneyKey, MoneyAmount);
    }

    public void AddMoney(int moneyAmount)
    {              
        MoneyAmount += moneyAmount;
        EventBus.OnMoneyValueChanged?.Invoke(MoneyAmount);
        SaveMoneyData();
    }
}