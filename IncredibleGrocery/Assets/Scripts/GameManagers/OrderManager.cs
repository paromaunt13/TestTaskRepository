using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private StorageData storageContent;

    [SerializeField] private int minProductAmount;
    [SerializeField] private int maxProductAmount;

    public static Action<Order> OnOrderCreated;
    
    private List<ProductItem> _orderProductList;
    private Buyer _buyer;

    private void Start()
    {
        _buyer = FindObjectOfType<Buyer>();
        _buyer.OnBuyerEnter += SetOrder;
    }

    private void OnDestroy()
    {
        _buyer.OnBuyerEnter -= SetOrder;
    }

    private void SetOrder()
    {
        var productList = GetProductList();
        //order.Products = productList;
        var order = new Order()
        {
            Products = productList
        };

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