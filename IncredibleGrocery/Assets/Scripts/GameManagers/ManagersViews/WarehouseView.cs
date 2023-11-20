using System;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseView : ViewManager
{
    [SerializeField] private StorageData storageData;
    [SerializeField] private Transform contentParent;

    private int _currentSelects;
    private int _orderCost;
    
    private  List<ProductItemView> _productItemsView;
    private  List<ProductItemView> _orderedProductItemsView;
    
    public static Action<List<ProductItemView>> OnWarehouseOrderCreated;
    public Action<int> OnOrderValueChanged;

    private void Start()
    {
        _orderedProductItemsView = new List<ProductItemView>();
        ClearProductItemsView(_productItemsView);
        _productItemsView = GetProductsView(storageData.Products, contentParent, false);
    }

    protected override void OnProductViewClick(ProductItemView productItemView)
    {
        base.OnProductViewClick(productItemView);

        switch (productItemView.CurrentProductState)
        {
            case ProductViewState.Selected:
                _currentSelects++;
                _orderedProductItemsView.Add(productItemView);
                break;
            case ProductViewState.Unselected:
            {
                _currentSelects--;
                _orderedProductItemsView.Remove(productItemView);
                break;
            }
        }
        _orderCost = _currentSelects * productItemView.Product.price;
        OnOrderValueChanged?.Invoke(_orderCost);
    }

    public void SetWarehouseOrder()
    {
        PlayerMoney.Instance.SpendMoney(_orderCost);
        OnWarehouseOrderCreated?.Invoke(_orderedProductItemsView);
    }

    public void ResetValues()
    {
        _currentSelects = 0;
        _orderCost = 0;
        
        foreach (var item in _productItemsView)
            item.SetState(ProductViewState.Unselected);
        
        _orderedProductItemsView.Clear();
        OnOrderValueChanged?.Invoke(_orderCost);
    }
}