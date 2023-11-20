using System;
using UnityEngine;

public class StoragePanelView : StorePanel
{
    [SerializeField] private OrderManager orderManager;
    [field: SerializeField] public StorageView StorageView { get; private set; }
    [field: SerializeField] public SellView SellView { get; private set; }
    [field: SerializeField] public AudioButton OpenWarehouseButton { get; private set; }
    [field: SerializeField] public AudioButton SellButton { get; private set; }

    private void Start()
    {
        SellButton.Button.interactable = false;
        SellButton.Button.onClick.AddListener(() =>
        {
            SellButton.Button.interactable = false;
            SellView.SetSellProducts();
            StorageView.SaveProductsViewData();
        });
        
        orderManager.OnOrderCreated += SetAvailableSelects;
        StorageView.OnSelectsValueChanged += UpdateSellButtonState;
        StorageView.OnProductIItemViewAdded += AddProductSellView;
        StorageView.OnProductIItemViewRemoved += RemoveProductSellView;
    }

    private void OnDestroy()
    { 
        orderManager.OnOrderCreated -= SetAvailableSelects;
        StorageView.OnSelectsValueChanged -= UpdateSellButtonState;
        StorageView.OnProductIItemViewAdded -= AddProductSellView;
        StorageView.OnProductIItemViewRemoved -= RemoveProductSellView;
    }

    private void UpdateSellButtonState(int selectsAmount, int currentSelects) =>
        SellButton.Button.interactable = currentSelects == selectsAmount;

    private void SetAvailableSelects(Order order)
    {
        StorageView.SelectsAmount = order.Products.Count;
        StorageView.AbleToSelect = true;
    }
    
    private void AddProductSellView(ProductItemView productItemView) =>
        SellView.ProductsToSellView.Add(productItemView);

    private void RemoveProductSellView(ProductItemView productItemView) =>
        SellView.ProductsToSellView.Remove(productItemView);
}