using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class OrderView : ViewManager
{
    [SerializeField] private float timeToCloudDisappear;

    private OrderManager _orderManager;
    private GameObject _orderCloud;
    private Transform _contentParent;

    private List<ProductItem> _orderList = new();
    private readonly List<ProductItemView> _orderItemsView = new();

    [Inject]
    private void Construct(OrderManager orderManager) =>
        _orderManager = orderManager;
    
    private void Start()
    {
        _orderManager.OnBuyerViewDataCreated += SetViewData;
        _orderManager.OnOrderCreated += SetOrderView;
    }
    
    private void OnDestroy()
    {
        _orderManager.OnBuyerViewDataCreated -= SetViewData;
        _orderManager.OnOrderCreated -= SetOrderView;
    }
    
    private void SetViewData(GameObject orderCloud, Transform contentParent)
    {
        _orderCloud = orderCloud;
        _contentParent = contentParent;
    }
    
    private void SetOrderView(Order order)
    {
        ClearParent(_contentParent, _orderItemsView);
    
        _orderList = order.Products;
        foreach (var orderItemView in _orderList
                     .Select(product => ProductItemViewFactory.GetProductView(product, _contentParent)))
        {
            orderItemView.AmountCounterText.gameObject.SetActive(false);
            _orderItemsView.Add(orderItemView);
        }

        SetCloudView(_orderCloud, timeToCloudDisappear);
    }
}