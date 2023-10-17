using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private StorageData _storageContent;

    [SerializeField] private int _minProductAmount;
    [SerializeField] private int _maxProductAmount;

    private List<ProductItem> _orderProductList;

    private void Start()
    {
        EventBus.OnBuyerEnter += SetOrder;
    }

    private void OnDisable()
    {
        EventBus.OnBuyerEnter -= SetOrder;
    }

    private List<ProductItem> GetProductList()
    {
        int productCount = Random.Range(_minProductAmount, _maxProductAmount + 1);

        _orderProductList = new List<ProductItem>();

        while (_orderProductList.Count < productCount)
        {
            int randomIndex = Random.Range(0, _storageContent.Products.Count);
            var randomProduct = _storageContent.Products[randomIndex];

            if (_orderProductList.Contains(randomProduct))
            {
                continue;
            }
            else
            {
                _orderProductList.Add(randomProduct);
            }
        }

        return _orderProductList;
    }

    public void SetOrder()
    {
        var productList = GetProductList();
        var order = new Order()
        {
            Products = productList
        };

        EventBus.OnOrderCreated?.Invoke(order);
    }
}