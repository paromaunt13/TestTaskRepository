using TMPro;
using UnityEngine;

public class PlayerMoneyView : MonoBehaviour
{
    [SerializeField] private string _textFormat;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private TMP_Text _moneyText;

    private void Start()
    {
        UpdateMoneyView(_playerMoney.MoneyAmount);

        EventBus.OnMoneyValueChanged += UpdateMoneyView;
    }

    private void OnDisable()
    {
        EventBus.OnMoneyValueChanged -= UpdateMoneyView;
    }
    private void UpdateMoneyView(int moneyAmount)
    {
        _moneyText.text = string.Format(_textFormat, moneyAmount);
    }
}