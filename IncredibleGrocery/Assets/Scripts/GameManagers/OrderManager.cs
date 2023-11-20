using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private SellManager sellManager;
    [SerializeField] private OrderZone orderZone;
    [SerializeField] private StorageData storageContent;

    [SerializeField] private int minProductAmount;
    [SerializeField] private int maxProductAmount;

    public BuyerView BuyerView { get; set; }

    private List<ProductItem> _orderProductList;

    public Action<Order> OnOrderCreated;
    public Action<GameObject, Transform> OnBuyerViewDataCreated;

    private void Start()
    {
        sellManager.OnOrderComplete += CompleteOrder;
        orderZone.OnBuyerEnter += SetOrderViewData;
    }

    private void OnDestroy()
    {
        sellManager.OnOrderComplete -= CompleteOrder;
        orderZone.OnBuyerEnter -= SetOrderViewData;
    }

    private void CompleteOrder(bool correctOrder) =>
        BuyerView.SetReaction(correctOrder);

    private void SetOrderViewData(Buyer buyer)
    {
        BuyerView = buyer.GetComponent<BuyerView>();
        
        var productList = GetProductList();
        var order = new Order()
        {
            Products = productList
        };

        OnBuyerViewDataCreated?.Invoke(BuyerView.BuyerCloud, BuyerView.ParentContent);
        OnOrderCreated?.Invoke(order);
    }
    
    private List<ProductItem> GetProductList()
    {
        var productCount = Random.Range(minProductAmount, maxProductAmount + 1);

        _orderProductList = new List<ProductItem>();

        while (_orderProductList.Count < productCount)
        {
            var randomIndex = Random.Range(0, storageContent.Products.Count);
            var randomProduct = storageContent.Products[randomIndex];

            if (_orderProductList.Contains(randomProduct))
                continue;

            _orderProductList.Add(randomProduct);
        }

        return _orderProductList;
    }
}