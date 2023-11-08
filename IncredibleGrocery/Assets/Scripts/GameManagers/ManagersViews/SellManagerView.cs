using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellManagerView : ViewManager
{
    [SerializeField] private SellManager sellManager;

    [Header("UI view elements")] 
    [SerializeField] private AudioButton sellButton;
    [SerializeField] private StoragePanel storagePanel;

    [Header("Sell view")] 
    [SerializeField] private GameObject sellCloud;
    [SerializeField] private Transform contentParent;
    [SerializeField] private ProductItemViewFactory productItemFactory;

    [Header("View animation settings")]
    [SerializeField] private float timeBeforeCheck;
    [SerializeField] private float timeToCloudDisappear;
    [SerializeField] private float timeBetweenChecks;

    private List<ProductItem> _productsToSell;
    private readonly List<ProductItemView> _productsToSellView = new();
    private List<ProductItemView> _productsViewToCheck = new();

    private void Start()
    {
        sellButton.Button.interactable = false;
        sellButton.Button.onClick.AddListener(() =>
        {
            storagePanel.MovePanel();
            sellButton.Button.interactable = false;
            SetSellProducts();
        });
    }

    public void AddProductSellView(ProductItemView productItemView)
    {
        _productsToSellView.Add(productItemView);
    }

    public void RemoveProductSellView(ProductItemView productItemView)
    {
        _productsToSellView.Remove(productItemView);
    }

    public void SetSellButtonState(int selectsAmount, int currentSelects)
    {
        sellButton.Button.interactable = currentSelects == selectsAmount;
    }

    private void SetSellProducts()
    {
        _productsToSell?.Clear();

        _productsToSell = new List<ProductItem>();
        foreach (var productView in _productsToSellView)
        {
            _productsToSell.Add(productView.Product);
        }
        StartCoroutine(SetSellProductsView());
    }

    private IEnumerator SetSellProductsView()
    {
        Clear(contentParent, _productsToSellView);

        _productsViewToCheck = new List<ProductItemView>();
        foreach (var product in _productsToSell)
        {
            var sellItemView = productItemFactory.GetProductView(product, contentParent);
            _productsViewToCheck.Add(sellItemView);
        }

        SetCloudView(sellCloud, timeToCloudDisappear);
        yield return new WaitForSeconds(timeBeforeCheck);
        StartCoroutine(CheckProducts(_productsViewToCheck));
    }

    private IEnumerator CheckProducts(List<ProductItemView> productItemsView)
    {
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

        sellManager.SetOrderCost(correctProductAmount);
    }
}