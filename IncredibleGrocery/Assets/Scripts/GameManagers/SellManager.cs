using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    [SerializeField] private int _costMultiplier;

    private List<ProductItem> _orderList = new();

    private Order _order = new();

    private int _totalOrderCost;
    private int _productPrice;

    public static bool CorrectOrder { get; private set; }
    public bool CanReceiveMoney { get; private set; }

    private void Start()
    {
        ResetValues();

        EventBus.OnOrderCreated += SetOrderList;
    }

    private void OnDisable()
    {
        EventBus.OnOrderCreated -= SetOrderList;
    }

    private void ResetValues()
    {
        _totalOrderCost = 0;
    }

    private void SetOrderList(Order order)
    {
        _order = order;
        _orderList = order.Products;
    }

    public bool CheckProduct(ProductItem product)
    {
        _productPrice = product.Price;
        if (_orderList.Contains(product))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetOrderCost(int correctProductAmount)
    {
        ResetValues();

        if (correctProductAmount == _order.Products.Count)
        {
            CanReceiveMoney = true;
            CorrectOrder = true;

            _totalOrderCost = _productPrice * correctProductAmount * _costMultiplier;
        }
        else if (correctProductAmount == 0)
        {
            CanReceiveMoney = false;
            CorrectOrder = false;
        }
        else
        {
            CanReceiveMoney = true;
            CorrectOrder = false;

            _totalOrderCost = _productPrice * correctProductAmount;
        }

        PlayerMoney.Instance.AddMoney(_totalOrderCost);

        EventBus.OnOrderComplete?.Invoke();
    }
}