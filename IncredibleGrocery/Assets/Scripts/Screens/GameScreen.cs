using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [Header("SettingsView")]
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private AudioButton settingsScreenButton;
    
    [Header("PlayerMoneyView")]
    [SerializeField] private TMP_Text moneyText;

    [Header("MoneyPopupView")] 
    [SerializeField] private MoneyPopup moneyPopUpPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    
     private const string MoneyTextFormat = "$";
     private const string SpendTextFormat = "-$";
     private const string EarnTextFormat = "+$";

    private void Start()
    {
        settingsScreenButton.Button.onClick.AddListener(() => settingsScreen.SetActive(true));
        moneyText.text = MoneyTextFormat + PersistentDataManager.MoneyAmount;
        PlayerMoney.Instance.OnMoneyValueChanged += UpdateMoneyView;
    }

    private void OnDestroy() => 
        PlayerMoney.Instance.OnMoneyValueChanged -= UpdateMoneyView;

    private void UpdateMoneyView(int totalMoneyAmount, int currentMoneyAmount, bool isEarning)
    {
        moneyText.text = MoneyTextFormat + totalMoneyAmount;
        ShowMoneyPopup(currentMoneyAmount, isEarning);
    }

    private void ShowMoneyPopup(int moneyAmount, bool isEarning)
    {
        var moneyPopUp = Instantiate(moneyPopUpPrefab, startPoint.position, Quaternion.identity, spawnParent);
        string moneyPopupText  = (isEarning ? EarnTextFormat : SpendTextFormat) + moneyAmount;
        moneyPopUp.ShowPopup(startPoint, endPoint, moneyPopupText);
    }
}
