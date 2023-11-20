using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderView : ViewManager
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private float timeToCloudDisappear;

    private GameObject _orderCloud;
    private Transform _contentParent;
    
    private List<ProductItem> _orderList = new();
    private readonly List<ProductItemView> _orderItemsView = new();

    private void Start()
    {
        orderManager.OnBuyerViewDataCreated += SetViewData;
        orderManager.OnOrderCreated += SetOrderView;
    }
    
    private void OnDestroy()
    {
        orderManager.OnBuyerViewDataCreated -= SetViewData;
        orderManager.OnOrderCreated -= SetOrderView;
    }
    
    private void SetViewData(GameObject orderCloud, Transform contentParent)
    {
        _orderCloud = orderCloud;
        _contentParent = contentParent;
    }
    
    private void SetOrderView(Order order)
    {
        StopAllCoroutines();
        ClearParent(_contentParent, _orderItemsView);
    
        _orderList = order.Products;
        foreach (var orderItemView in _orderList.Select(product => ProductItemFactory.GetProductView(product, _contentParent)))
        {
            orderItemView.AmountCounterText.gameObject.SetActive(false);
            _orderItemsView.Add(orderItemView);
        }

        StartCoroutine(ShowOrderCloud());
    }
    
    private IEnumerator ShowOrderCloud()
    {
        SetCloudView(_orderCloud, timeToCloudDisappear);
        yield return new WaitForSeconds(timeToCloudDisappear);
        ClearParent(_contentParent, _orderItemsView);;
    }
}