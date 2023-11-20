using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    public  Action<int, int, bool> OnMoneyValueChanged;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    public void EarnMoney(int moneyAmount)
    {
        PersistentDataManager.MoneyAmount += moneyAmount;
        AudioManager.Instance.PlaySound(SoundType.MoneyEarnSound);
        OnMoneyValueChanged?.Invoke(PersistentDataManager.MoneyAmount, moneyAmount, true);
    }
    
    public void SpendMoney(int moneyAmount)
    {
        PersistentDataManager.MoneyAmount -= moneyAmount;
        AudioManager.Instance.PlaySound(SoundType.MoneySpentSound);
        OnMoneyValueChanged?.Invoke(PersistentDataManager.MoneyAmount, moneyAmount, false);
    }
}