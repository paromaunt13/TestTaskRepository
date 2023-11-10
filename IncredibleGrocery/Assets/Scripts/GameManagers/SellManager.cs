using System;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    [SerializeField] private int costMultiplier;

    private List<ProductItem> _sellList = new();

    private Order _order = new();

    private int _totalOrderCost;

    public static Action<bool> OnOrderComplete;
    
    private void Start()
    {
        OrderZoneManager.OnOrderCreated += SetSellList;
    }

    private void OnDestroy()
    {
        OrderZoneManager.OnOrderCreated -= SetSellList;
    }

    private void SetSellList(Order order)
    {
        _totalOrderCost = 0;
        _order = order;
        _sellList = order.Products;
    }

    public bool CheckProduct(ProductItem product)
    {
        var hasProduct = _sellList.Contains(product);
        if (hasProduct) 
            _totalOrderCost += product.price;
        return hasProduct;
    }

    public void SetOrderFinalCost(int correctProductAmount)
    {
        var correctOrder = correctProductAmount == _order.Products.Count;
        
        var canReceiveMoney = correctProductAmount != 0;
        
        if (canReceiveMoney)
        {
            _totalOrderCost *=   correctOrder ? costMultiplier : 1;
            PlayerMoney.Instance.AddMoney(_totalOrderCost);
        }

        OnOrderComplete?.Invoke(correctOrder);
    }
}