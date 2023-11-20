using TMPro;
using UnityEngine;

public class WarehousePanelView : StorePanel
{
    [SerializeField] private string textFormat;
    [field: SerializeField] public WarehouseView WarehouseView { get; private set; }
    [field: SerializeField] public AudioButton CloseButton { get; private set; }
    [field: SerializeField] public AudioButton OrderButton { get; private set; }
    [field: SerializeField] public TMP_Text OrderCostText { get; set; }

    private void Start()
    {
        SetUnInteractable();
        WarehouseView.OnOrderValueChanged += UpdateOrderButtonState;
        OrderButton.Button.onClick.AddListener(() =>
        {
            WarehouseView.SetWarehouseOrder();
            WarehouseView.ResetValues();
        });
    }

    private void OnDestroy()
    {
        WarehouseView.OnOrderValueChanged -= UpdateOrderButtonState;
    }

    private void UpdateOrderButtonState(int orderCost)
    {
        OrderCostText.text = string.Format(textFormat, orderCost.ToString());
        if (orderCost <= 0 || PersistentDataManager.MoneyAmount < orderCost)
        {
            SetUnInteractable();
            return;
        }
        SetInteractable();
    }

    private void SetInteractable()
    {
        OrderButton.Button.interactable = true;
        OrderCostText.color = Color.green;
    }

    private void SetUnInteractable()
    {
        OrderButton.Button.interactable = false;
        OrderCostText.color = Color.red;
    } 
}