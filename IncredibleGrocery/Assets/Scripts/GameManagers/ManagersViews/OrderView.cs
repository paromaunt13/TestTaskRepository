using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderView : ViewManager
{
    [SerializeField] private OrderZoneManager orderZoneManager;
    [SerializeField] private ProductItemViewFactory productItemFactory;
    [SerializeField] private float timeToCloudDisappear;

    private List<ProductItem> _orderList = new();
    private readonly List<ProductItemView> _orderItemsView;

    public static Action OnOrderViewCreated;

    private GameObject _orderCloud;
    private Transform _contentParent;

    private void Start()
    {
        OrderZoneManager.OnOrderCreated += SetOrderView;
        orderZoneManager.OnBuyerDataCreated += GetViewData;
    }

    private void GetViewData(GameObject orderCloud, Transform contentParent)
    {
        _orderCloud = orderCloud;
        _contentParent = contentParent;
    }

    private void OnDestroy()
    {
        OrderZoneManager.OnOrderCreated -= SetOrderView;
        orderZoneManager.OnBuyerDataCreated += GetViewData;
    }

    private IEnumerator ShowOrderCloud()
    {
        //var orderCloud = orderZoneManager.BuyerView.BuyerCloud;
        SetCloudView(_orderCloud, timeToCloudDisappear);

        yield return new WaitForSeconds(timeToCloudDisappear);
        
        Clear(_contentParent, _orderItemsView);;

        OnOrderViewCreated?.Invoke();
    }

    private void SetOrderView(Order order)
    {
        //var contentParent =  orderZoneManager.BuyerView.ParentContent;
        Clear(_contentParent, _orderItemsView);
    
        _orderList = order.Products;
        foreach (var product in _orderList)
        {
            productItemFactory.GetProductView(product, _contentParent);
        }

        StartCoroutine(ShowOrderCloud());
    }
}