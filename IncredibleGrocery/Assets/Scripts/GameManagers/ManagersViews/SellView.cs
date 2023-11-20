using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellView : ViewManager
{
    [SerializeField] private SellManager sellManager;
    [SerializeField] private OrderManager orderManager;

    [Header("Sell view")] 
    [SerializeField] private GameObject sellCloud;
    [SerializeField] private Transform contentParent;

    [Header("View animation settings")]
    [SerializeField] private float timeBeforeCheck;
    [SerializeField] private float timeToCloudDisappear;
    [SerializeField] private float timeBetweenChecks;

    private List<ProductItem> _productsToSell;
    private List<ProductItemView> _productsViewToCheck = new();
    public List<ProductItemView> ProductsToSellView { get; set; }
    
    public static Action OnSellListSet;

    private void Start()
    {
        ProductsToSellView = new List<ProductItemView>();
    }

    public void SetSellProducts()
    {
        _productsToSell?.Clear();

        _productsToSell = new List<ProductItem>();
        foreach (var productView in ProductsToSellView)
        {
            _productsToSell.Add(productView.Product);
        }
        SetSellProductsView();
        OnSellListSet?.Invoke();
    }

    private void SetSellProductsView()
    {
        ClearParent(contentParent, ProductsToSellView);

        _productsViewToCheck = new List<ProductItemView>();
        foreach (var product in _productsToSell)
        {
            var sellItemView = ProductItemFactory.GetProductView(product, contentParent);
            sellItemView.AmountCounterText.gameObject.SetActive(false);
            _productsViewToCheck.Add(sellItemView);
            product.currentAmount--;
        }

        if (orderManager.BuyerView.IsLeaveUnsatisfied)  return;
        orderManager.BuyerView.IsWaitingForOrderCheck = true;
        SetCloudView(sellCloud, timeToCloudDisappear);
        StartCoroutine(CheckProducts(_productsViewToCheck));
    }

    private IEnumerator CheckProducts(List<ProductItemView> productItemsView)
    {
        yield return new WaitForSeconds(timeBeforeCheck);
        var correctProductAmount = 0;

        foreach (var productView in productItemsView)
        {
            if (sellManager.CheckProduct(productView.Product))
            {
                correctProductAmount++;
                productView.SetState(ProductViewState.Correct);
            }
            else
                productView.SetState(ProductViewState.Wrong);

            yield return new WaitForSeconds(timeBetweenChecks);
        }
        sellManager.SetOrderFinalCost(correctProductAmount);
    }
}