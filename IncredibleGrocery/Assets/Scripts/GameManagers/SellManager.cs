using System;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    [SerializeField] private int costMultiplier;

    private List<ProductItem> _sellList = new();

    private Order _order = new();

    private int _totalOrderCost;
    private int _productPrice;

    public static Action OnOrderComplete;
    
    public static bool CorrectOrder { get; private set; }
    public bool CanReceiveMoney { get; private set; }

    private void Start()
    {
        OrderManager.OnOrderCreated += SetSellList;
        ResetValues();
    }

    private void OnDestroy()
    {
        OrderManager.OnOrderCreated -= SetSellList;
    }

    private void ResetValues()
    {
        _totalOrderCost = 0;
    }

    private void SetSellList(Order order)
    {
        _order = order;
        _sellList = order.Products;
    }

    public bool CheckProduct(ProductItem product)
    {
        _productPrice = product.price;
        return _sellList.Contains(product);
    }

    public void SetOrderCost(int correctProductAmount)
    {
        ResetValues();
        
        if (correctProductAmount != _order.Products.Count)
        {
            if (correctProductAmount == 0)
                CanReceiveMoney = false;
            else
            {
                CanReceiveMoney = true;
                _totalOrderCost = _productPrice * correctProductAmount;
            }
            CorrectOrder = false;
        }
        else
        {
            CanReceiveMoney = true;
            CorrectOrder = true;

            _totalOrderCost = _productPrice * correctProductAmount * costMultiplier;
        }

        PlayerMoney.Instance.AddMoney(_totalOrderCost);

        OnOrderComplete?.Invoke();
    }
}