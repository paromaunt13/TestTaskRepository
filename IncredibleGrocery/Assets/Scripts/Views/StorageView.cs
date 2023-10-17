using System.Collections.Generic;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    [SerializeField] private StorageData _storageData;
    [SerializeField] private ProductItemViewFactory _productItemFactory;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private SellManagerView _sellManagerView;

    private List<ProductItemView> _productItemsView = new();

    private int _selectsAmount;
    private int _currentSelects;

    private void Start()
    {
        EventBus.OnOrderCreated += SetAvailableSelects;
        EventBus.OnOrderComplete += ResetViewValues;

        SetProductsView(_storageData.Products);
    }

    private void OnDisable()
    {
        EventBus.OnOrderCreated -= SetAvailableSelects;
        EventBus.OnOrderComplete -= ResetViewValues;
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
            _sellManagerView.AddProductSellView(productItemView);
        }
        else if (productItemView.CurrentProductState == ProductViewState.Unselected)
        {
            _currentSelects--;            
            _sellManagerView.RemoveProductSellView(productItemView);           
        }

        _sellManagerView.CheckSellButtonState(_selectsAmount, _currentSelects);
    }

    private void Clear()
    {
        foreach (var item in _productItemsView)
        {          
            item.Click -= OnProductViewClick;
            Destroy(item.gameObject);
        }

        _productItemsView.Clear();
    }

    public void SetProductsView(List<ProductItem> products)
    {
        Clear();

        foreach (var product in products)
        {
            var productItemView = _productItemFactory.GetProductView(product,_contentParent);
            productItemView.Click += OnProductViewClick;
            productItemView.SetState(ProductViewState.Unselected);
            _productItemsView.Add(productItemView);
        }
    }   
}