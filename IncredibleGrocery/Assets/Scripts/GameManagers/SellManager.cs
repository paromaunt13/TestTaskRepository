using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SellManager : MonoBehaviour
{
    [SerializeField] private int costMultiplier;
    private OrderManager _orderManager;
    
    private List<ProductItem> _sellList = new();

    private Order _order = new();

    private int _totalOrderCost;

    public Action<bool> OnOrderComplete;

    [Inject]
    private void Construct(OrderManager orderManager) =>
        _orderManager = orderManager;
    private void Start() => 
        _orderManager.OnOrderCreated += SetSellList;
    private void OnDestroy() => 
        _orderManager.OnOrderCreated -= SetSellList;

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
            PlayerMoney.Instance.EarnMoney(_totalOrderCost);
        }

        OnOrderComplete?.Invoke(correctOrder);
    }
}