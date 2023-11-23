using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WarehouseView : ViewManager
{
    [SerializeField] private Transform contentParent;
    private StorageData _warehouseData;
    
    private int _orderCost;
    
    private  List<ProductItemView> _productItemsView;
    private  List<ProductItemView> _orderedProductItemsView;
    
    public static Action<List<ProductItemView>> OnWarehouseOrderCreated;
    public Action<int> OnOrderValueChanged;

    [Inject]
    private void Construct(StorageDataConfig storageDataConfig) =>
        _warehouseData = storageDataConfig.WarehouseData;
    private void Start()
    {
        _orderedProductItemsView = new List<ProductItemView>();
        ClearProductItemsView(_productItemsView);
        _productItemsView = GetProductsView(_warehouseData.Products, contentParent, false);
    }

    protected override void OnProductViewClick(ProductItemView productItemView)
    {
        base.OnProductViewClick(productItemView);

        switch (productItemView.CurrentProductState)
        {
            case ProductViewState.Selected:
                _orderedProductItemsView.Add(productItemView);
                break;
            case ProductViewState.Unselected:
            {
                _orderedProductItemsView.Remove(productItemView);
                break;
            }
        }
        _orderCost = _orderedProductItemsView.Count * productItemView.Product.price;
        OnOrderValueChanged?.Invoke(_orderCost);
    }

    public void SetWarehouseOrder()
    {
        PlayerMoney.Instance.SpendMoney(_orderCost);
        OnWarehouseOrderCreated?.Invoke(_orderedProductItemsView);
    }

    public void ResetValues()
    {
        _orderCost = 0;
        
        foreach (var item in _productItemsView)
            item.SetState(ProductViewState.Unselected);
        
        _orderedProductItemsView.Clear();
        OnOrderValueChanged?.Invoke(_orderCost);
    }
}