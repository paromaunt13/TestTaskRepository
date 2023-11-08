using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    [SerializeField] private StorageData storageData;
    [SerializeField] private ProductItemViewFactory productItemFactory;
    [SerializeField] private Transform contentParent;
    [SerializeField] private SellManagerView sellManagerView;

    private readonly List<ProductItemView> _productItemsView = new();
    
    private int _selectsAmount;
    private int _currentSelects;

    private void Start()
    {
        OrderManager.OnOrderCreated += SetAvailableSelects;
        SellManager.OnOrderComplete += ResetViewValues;
        SetProductsView(storageData.Products);
    }

    private void OnDestroy()
    {
        OrderManager.OnOrderCreated -= SetAvailableSelects;
        SellManager.OnOrderComplete -= ResetViewValues;
    }

    private void ResetViewValues()
    {
        _currentSelects = 0;
        foreach (var item in _productItemsView)
        {
            item.SetState(ProductViewState.Unselected);
        }
    }

    private void SetAvailableSelects(Order order)
    {
        _selectsAmount = order.Products.Count;
    }

    private void OnProductViewClick(ProductItemView productItemView)
    {
        productItemView.SwitchState();

        if (productItemView.CurrentProductState == ProductViewState.Selected)
        {
            _currentSelects++;
            sellManagerView.AddProductSellView(productItemView);
        }
        else if (productItemView.CurrentProductState == ProductViewState.Unselected)
        {
            _currentSelects--;
            sellManagerView.RemoveProductSellView(productItemView);
        }

        sellManagerView.SetSellButtonState(_selectsAmount, _currentSelects);
    }

    private void Clear()
    {
        foreach (var item in _productItemsView)
        {
            //item.Click -= OnProductViewClick;
            Destroy(item.gameObject);
        }

        _productItemsView.Clear();
    }

    private void SetProductsView(List<ProductItem> products)
    {
        Clear();

        foreach (var product in products)
        {
            var productItemView = productItemFactory.GetProductView(product, contentParent);
            productItemView.Click += OnProductViewClick;
            productItemView.SetState(ProductViewState.Unselected);
            _productItemsView.Add(productItemView);
        }
    }
}