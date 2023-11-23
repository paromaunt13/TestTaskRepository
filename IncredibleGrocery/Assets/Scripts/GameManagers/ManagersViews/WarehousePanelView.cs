using TMPro;
using UnityEngine;

public class WarehousePanelView : StorePanel
{
    [SerializeField] private WarehouseView warehouseView;
    [SerializeField] private AudioButton orderButton;
    [SerializeField] private TMP_Text orderCostText;
    [field: SerializeField] public AudioButton CloseButton { get; private set; }

    private void Start()
    {
        SetInteractable(false);
        warehouseView.OnOrderValueChanged += UpdateOrderButtonState;
        orderButton.Button.onClick.AddListener(() =>
        {
            warehouseView.SetWarehouseOrder();
            warehouseView.ResetValues();
        });
    }

    private void OnDestroy() =>
        warehouseView.OnOrderValueChanged -= UpdateOrderButtonState;
    
    private void UpdateOrderButtonState(int orderCost)
    {
        orderCostText.text = "$" + orderCost;
        SetInteractable(!(orderCost <= 0 || PersistentDataManager.MoneyAmount < orderCost)); 
    }

    private void SetInteractable(bool isInteractable)
    {
        orderButton.Button.interactable = isInteractable;
        orderCostText.color = isInteractable? Color.green : Color.red;
    }
}