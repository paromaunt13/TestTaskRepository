using System;
using System.Collections;
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
        if (PlayerPrefs.HasKey(MoneyKey))
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
        MoneyAmount = PlayerPrefs.GetInt(MoneyKey, 0);
        EventBus.OnMoneyValueChanged?.Invoke(MoneyAmount);
    }

    private void SaveMoneyData()
    {
        PlayerPrefs.SetInt(MoneyKey, MoneyAmount);
        PlayerPrefs.Save();
    }

    public void AddMoney(int moneyAmount)
    {              
        MoneyAmount += moneyAmount;
        EventBus.OnMoneyValueChanged?.Invoke(MoneyAmount);
        SaveMoneyData();
    }
}