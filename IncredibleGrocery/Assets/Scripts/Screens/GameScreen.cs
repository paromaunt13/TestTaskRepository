using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [Header("Settings view")]
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private AudioButton settingsScreenButton;
    
    [Header("PlayerMoneyView")]
    [SerializeField] private string textFormat;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        settingsScreenButton.Button.onClick.AddListener(() => settingsScreen.SetActive(true));
        
        UpdateMoneyView(PersistentDataManager.MoneyAmount);
        PlayerMoney.Instance.OnMoneyValueChanged += UpdateMoneyView;
    }

    private void OnDestroy()
    {
        PlayerMoney.Instance.OnMoneyValueChanged -= UpdateMoneyView;
    }

    private void UpdateMoneyView(int moneyAmount)
    {
        moneyText.text = string.Format(textFormat, moneyAmount);
    }
}
