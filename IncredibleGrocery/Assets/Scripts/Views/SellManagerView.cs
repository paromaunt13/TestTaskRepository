using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellManagerView : ViewManager
{
    [SerializeField] private SellManager sellManager;

    [Header("UI view elements")] 
    [SerializeField] private Button sellButton;
    [SerializeField] private StoragePanel storagePanel;

    [Header("Sell view")] 
    [SerializeField] private GameObject sellCloud;
    [SerializeField] private Transform contentParent;

    [SerializeField] private ProductItemViewFactory productItemFactory;

    [Header("View animation settings")] 
    [SerializeField] private SoundsData soundsData;

    [SerializeField] private float timeBeforeCheck;
    [SerializeField] private float timeToCloudDisappear;
    [SerializeField] private float timeBetweenChecks;

    private GridLayoutGroup _orderLayout;

    private List<ProductItem> _productsToSell;
    private readonly List<ProductItemView> _productsToSellView = new();
    private List<ProductItemView> _productsViewToCheck = new();

    private void Start()
    {
        SellManager.OnOrderComplete += PlayMoneySound;

        sellButton.interactable = false;

        _orderLayout = contentParent.GetComponent<GridLayoutGroup>();

        sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        SellManager.OnOrderComplete -= PlayMoneySound;
        sellButton.onClick.RemoveAllListeners();
    }

    private void PlayMoneySound()
    {
        if (sellManager.CanReceiveMoney)
            AudioManager.Instance.PlaySound(soundsData.MoneyIncomeSound);
    }

    private void OnButtonClick()
    {
        storagePanel.MovePanel();
        sellButton.interactable = false;

        SetSellProducts();
    }

    public void AddProductSellView(ProductItemView productItemView)
    {
        _productsToSellView.Add(productItemView);
    }

    public void RemoveProductSellView(ProductItemView productItemView)
    {
        _productsToSellView.Remove(productItemView);
    }

    public void CheckSellButtonState(int selectsAmount, int currentSelects)
    {
        sellButton.interactable = currentSelects == selectsAmount;
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

        //ResizeCells(_productsToSell.Count, _orderLayout);

        _productsViewToCheck = new List<ProductItemView>();
        foreach (var product in _productsToSell)
        {
            var sellItemView = productItemFactory.GetProductView(product, contentParent);
            _productsViewToCheck.Add(sellItemView);
        }

        ShowSellCloud();

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

    private void ShowSellCloud()
    {
        SetCloudView(sellCloud, timeToCloudDisappear,
            soundsData.BubbleAppearSound, soundsData.BubbleDisappearSound);
    }
}