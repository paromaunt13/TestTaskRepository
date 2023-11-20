using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [Header("SettingsView")]
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private AudioButton settingsScreenButton;
    
    [Header("PlayerMoneyView")]
    [SerializeField] private string textFormat;
    [SerializeField] private TMP_Text moneyText;

    [Header("MoneyPopupView")] 
    [SerializeField] private MoneyPopup moneyPopUpPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private string spendTextFormat;
    [SerializeField] private string earnTextFormat;

    private void Start()
    {
        settingsScreenButton.Button.onClick.AddListener(() => settingsScreen.SetActive(true));
        moneyText.text = string.Format(textFormat, PersistentDataManager.MoneyAmount);
        PlayerMoney.Instance.OnMoneyValueChanged += UpdateMoneyView;
    }

    private void OnDestroy()
    {
        PlayerMoney.Instance.OnMoneyValueChanged -= UpdateMoneyView;
    }

    private void UpdateMoneyView(int totalMoneyAmount, int currentMoneyAmount, bool isEarning)
    {
        moneyText.text = string.Format(textFormat, totalMoneyAmount);
        ShowMoneyPopup(currentMoneyAmount, isEarning);
    }

    private void ShowMoneyPopup(int moneyAmount, bool isEarning)
    {
        var moneyPopUp = Instantiate(moneyPopUpPrefab, startPosition.position, Quaternion.identity, spawnParent);
        
        moneyPopUp.MoneyText.text = isEarning
            ? string.Format(earnTextFormat, moneyAmount)
            : string.Format(spendTextFormat, moneyAmount);
        
        moneyPopUp.EndPosition = endPosition;
        moneyPopUp.StartPosition = startPosition;
    }
}
