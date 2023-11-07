using TMPro;
using UnityEngine;

public class PlayerMoneyView : MonoBehaviour
{
    [SerializeField] private string textFormat;
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        UpdateMoneyView(playerMoney.MoneyAmount);
        PlayerMoney.OnMoneyValueChanged += UpdateMoneyView;
    }
    
    private void OnDestroy()
    {
        PlayerMoney.OnMoneyValueChanged -= UpdateMoneyView;
    }

    private void UpdateMoneyView(int moneyAmount)
    {
        moneyText.text = string.Format(textFormat, moneyAmount);
    }
}