using System;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    [SerializeField] private int costMultiplier;

    private List<ProductItem> _sellList = new();

    private Order _order = new();

    private int _productPrice;

    public static Action OnOrderComplete;
    
    public static bool CorrectOrder { get; private set; }
    
    private void Start()
    {
        OrderManager.OnOrderCreated += SetSellList;
    }

    private void OnDestroy()
    {
        OrderManager.OnOrderCreated -= SetSellList;
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
        var correctOrder = correctProductAmount == _order.Products.Count;
        
        var canReceiveMoney = correctProductAmount != 0;
        if (canReceiveMoney)
        {
            var totalOrderCost = _productPrice * correctProductAmount * (correctOrder ? costMultiplier : 1);
            PlayerMoney.Instance.AddMoney(totalOrderCost);
            AudioManager.Instance.PlaySound(SoundType.MoneyReceiveSound);
        }

        OnOrderComplete?.Invoke();
    }
}