using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StorageView : ViewManager
{
    [SerializeField] private Transform contentParent;
    private StorageData _storageData;
    private  List<ProductItemView> _productItemsView;
    
    private int _currentSelects;
    public int SelectsAmount { get; set; }
    public bool AbleToSelect { get; set; }
    
    public Action<int, int> OnSelectsValueChanged;
    public Action<ProductItemView> OnProductIItemViewAdded;
    public Action<ProductItemView> OnProductIItemViewRemoved;

    [Inject]
    private void Construct(StorageDataConfig storageDataConfig) =>
        _storageData = storageDataConfig.StorageData;
    
    private void Start()
    {
        AbleToSelect = false;
        _productItemsView = GetProductsView(_storageData.Products, contentParent, true);
        LoadProductsViewData();
        SellView.OnSellListSet += ResetViewValues;
        WarehouseView.OnWarehouseOrderCreated += UpdateProductsAmount;
    }

    private void OnDestroy()
    {
        SellView.OnSellListSet -= ResetViewValues;
        WarehouseView.OnWarehouseOrderCreated -= UpdateProductsAmount;
    }

    private void LoadProductsViewData()
    {
        if (PersistentDataManager.FirstLaunch) return;
        foreach (var productItemView in _productItemsView)
        {
            productItemView.Product.currentAmount = 
                PlayerPrefs.GetInt(productItemView.Product.productType.ToString(), productItemView.Product.currentAmount);;
            productItemView.UpdateCounterText(productItemView.Product.currentAmount);
            if (productItemView.Product.currentAmount == 0)
                productItemView.SetState(ProductViewState.OutOfStock);
        }
    }

    public void SaveProductsViewData()
    {
        foreach (var productItemView in _productItemsView)
        {
            PlayerPrefs.SetInt(productItemView.Product.productType.ToString(), productItemView.Product.currentAmount);
            PlayerPrefs.Save();
        }
    }

    public void ResetViewValues()
    {
        AbleToSelect = false;
        _currentSelects = 0;
        foreach (var productItemView in _productItemsView)
        {
            productItemView.SetState(ProductViewState.Unselected);
            productItemView.UpdateCounterText(productItemView.Product.currentAmount);
            if (productItemView.Product.currentAmount == 0)
                productItemView.SetState(ProductViewState.OutOfStock);
        }
        OnSelectsValueChanged?.Invoke(SelectsAmount, _currentSelects);
    }

    protected override void OnProductViewClick(ProductItemView productItemView)
    {
        if (!AbleToSelect) return;
        base.OnProductViewClick(productItemView);
        
        switch (productItemView.CurrentProductState)
        {
            case ProductViewState.Selected:
                _currentSelects++;
                OnProductIItemViewAdded?.Invoke(productItemView);
                break;
            case ProductViewState.Unselected:
                _currentSelects--;
                OnProductIItemViewRemoved?.Invoke(productItemView);
                break;
        }
        OnSelectsValueChanged?.Invoke(SelectsAmount, _currentSelects);
    }

    private void UpdateProductsAmount(List<ProductItemView> newProductItemsView)
    {
        foreach (var productItemView in _productItemsView)
        {
            var matchingProduct = newProductItemsView
                .Find(p => p.Product.productType == productItemView.Product.productType);

            if (matchingProduct == null) continue;
            
            productItemView.Product.currentAmount += matchingProduct.Product.baseAmount;
            productItemView.UpdateCounterText(productItemView.Product.currentAmount);
            if (productItemView.CurrentProductState == ProductViewState.OutOfStock)
                productItemView.SetState(ProductViewState.Unselected);
        }
        SaveProductsViewData();
    }
}