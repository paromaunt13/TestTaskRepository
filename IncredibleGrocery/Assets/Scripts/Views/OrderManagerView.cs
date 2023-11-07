using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManagerView : ViewManager
{
    [Header("Order view")] 
    [SerializeField] private GameObject orderCloud;

    [SerializeField] private Transform contentParent;
    [SerializeField] private ProductItemViewFactory productItemFactory;

    [Header("View settings")] 
    [SerializeField] private SoundsData soundsData;

    [SerializeField] private float timeToCloudDisappear;

    private GridLayoutGroup _orderLayout;

    private List<ProductItem> _orderList = new();

    private readonly List<ProductItemView> _orderItemsView;

    public static Action OnOrderViewCreated;

    private void Start()
    {
        _orderLayout = contentParent.GetComponent<GridLayoutGroup>();

        OrderManager.OnOrderCreated += SetProductView;
    }

    private void OnDestroy()
    {
        OrderManager.OnOrderCreated -= SetProductView;
    }

    private IEnumerator ShowOrderCloud()
    {
        SetCloudView(orderCloud, timeToCloudDisappear,
            soundsData.BubbleAppearSound, soundsData.BubbleDisappearSound);

        yield return new WaitForSeconds(timeToCloudDisappear);
        
        Clear(contentParent, _orderItemsView);;

        OnOrderViewCreated?.Invoke();
    }
    
    private void SetProductView(Order order)
    {
        Clear(contentParent, _orderItemsView);

        _orderList = order.Products;
    
        //ResizeCells(_orderList.Count, _orderLayout);

        foreach (var product in _orderList)
        {
            productItemFactory.GetProductView(product, contentParent);
        }

        StartCoroutine(ShowOrderCloud());
    }
}