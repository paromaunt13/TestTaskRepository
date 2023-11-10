using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderZoneManager : MonoBehaviour
{
    [SerializeField] private OrderZone orderZone;
    [SerializeField] private StorageData storageContent;

    [SerializeField] private int minProductAmount;
    [SerializeField] private int maxProductAmount;

    private BuyerView _buyerView;
    
    private List<ProductItem> _orderProductList;

    public static Action<Order> OnOrderCreated;
    public  Action<GameObject, Transform> OnBuyerDataCreated;
    
    private void Start()
    {
        orderZone.OnBuyerEnter += SetBuyerData;
    }
    
    private void OnDestroy()
    {
        orderZone.OnBuyerEnter -= SetBuyerData;
    }

    private void SetBuyerData(Buyer buyer)
    {
        _buyerView = buyer.GetComponent<BuyerView>();
        
        var productList = GetProductList();
        var order = buyer.MakeOrder();
        order.Products.AddRange(productList);
        
        OnBuyerDataCreated?.Invoke(_buyerView.BuyerCloud, _buyerView.ParentContent);
        OnOrderCreated?.Invoke(order);
    }

    private IEnumerable<ProductItem> GetProductList()
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