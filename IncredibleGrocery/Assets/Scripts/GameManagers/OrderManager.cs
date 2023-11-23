using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderZone orderZone;
    [SerializeField] private int minProductAmount;
    [SerializeField] private int maxProductAmount;

    private SellManager _sellManager;
    private StorageData _storageData;
    public BuyerView BuyerView { get; set; }

    private List<ProductItem> _orderProductList;

    public Action<Order> OnOrderCreated;
    public Action OnOrderCanceled;
    public Action<GameObject, Transform> OnBuyerViewDataCreated;

    [Inject]
    private void Construct(StorageDataConfig storageDataConfig, SellManager sellManager)
    {
        _storageData = storageDataConfig.StorageData;
        _sellManager = sellManager;
    }
    
    private void Start()
    {
        _sellManager.OnOrderComplete += CompleteOrder;
        orderZone.OnBuyerEnter += SetOrderViewData;
    }

    private void OnDestroy()
    {
        _sellManager.OnOrderComplete -= CompleteOrder;
        orderZone.OnBuyerEnter -= SetOrderViewData;
        
        if (BuyerView is not null)
            BuyerView.OnBuyerLeaved -= CancelOrder;
    }

    private void CompleteOrder(bool correctOrder) =>
        BuyerView.SetReaction(correctOrder);

    private void SetOrderViewData(Buyer buyer)
    {
        BuyerView = buyer.GetComponent<BuyerView>();
        BuyerView.OnBuyerLeaved += CancelOrder;

        OnBuyerViewDataCreated?.Invoke(BuyerView.BuyerCloud, BuyerView.ParentContent);
        OnOrderCreated?.Invoke(new Order { Products = GetProductList() });
    }

    private List<ProductItem> GetProductList()
    {
        var productCount = Random.Range(minProductAmount, maxProductAmount + 1);

        _orderProductList = new List<ProductItem>();

        while (_orderProductList.Count < productCount)
        {
            var randomIndex = Random.Range(0, _storageData.Products.Count);
            var randomProduct = _storageData.Products[randomIndex];

            if (_orderProductList.Contains(randomProduct))
                continue;

            _orderProductList.Add(randomProduct);
        }

        return _orderProductList;
    }

    private void CancelOrder()
    {
        OnOrderCanceled?.Invoke();
    }
}