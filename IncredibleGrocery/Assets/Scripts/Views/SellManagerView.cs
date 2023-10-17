using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellManagerView : MonoBehaviour
{
    [SerializeField] private SellManager _sellManager;

    [Header("UI view elements")]
    [SerializeField] private Button _sellButton;
    [SerializeField] private StoragePanel _storagePanel;

    [Header("Sell view")]
    [SerializeField] private GameObject _sellCloud;
    [SerializeField] private Transform _contentParent;

    [SerializeField] private ProductItemViewFactory _productItemFactory;

    [Header("View animation settings")]
    [SerializeField] private SoundsData _soundsData;
    [SerializeField] private float _timeBeforeCheck;
    [SerializeField] private float _timeToCloudDisappear;
    [SerializeField] private float _timeBetweenChecks;

    private GridLayoutGroup _orderLayout;

    private List<ProductItem> _productsToSell;
    private readonly List<ProductItemView> _productsToSellView = new();
    private List<ProductItemView> _productsViewToCheck = new();

    private void Start()
    {
        EventBus.OnOrderComplete += PlayMoneySound;

        _sellButton.interactable = false;

        _orderLayout = _contentParent.GetComponent<GridLayoutGroup>();

        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        EventBus.OnOrderComplete -= PlayMoneySound;
        _sellButton.onClick.RemoveAllListeners();
    }

    private void PlayMoneySound()
    {
        if (_sellManager.CanReceiveMoney)
        {
            AudioManager.Instance.PlaySound(_soundsData.MoneyIncomeSound);
        }       
    }

    private void OnButtonClick()
    {
        _storagePanel.HidePanel();
        _sellButton.interactable = false;

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
        if (currentSelects == selectsAmount)
        {
            _sellButton.interactable = true;
        }
        else
        {
            _sellButton.interactable = false;
        }
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
        Clear();

        ResizeCells(_productsToSell.Count);

        _productsViewToCheck = new List<ProductItemView>();
        foreach (var product in _productsToSell)
        {
            var sellItemView = _productItemFactory.GetProductView(product, _contentParent);
            _productsViewToCheck.Add(sellItemView);
        }

        ShowSellCloud();

        yield return new WaitForSeconds(_timeBeforeCheck);

        StartCoroutine(CheckProducts(_productsViewToCheck));
    }

    private IEnumerator CheckProducts(List<ProductItemView> productItemsView)
    {
        int correctProductAmount = 0;

        foreach (var productView in productItemsView)
        {
            if (_sellManager.CheckProduct(productView.Product))
            {
                correctProductAmount++;
                productView.SetState(ProductViewState.Correct);
            }
            else
            {
                productView.SetState(ProductViewState.Wrong);
            }
            yield return new WaitForSeconds(_timeBetweenChecks);
        }
        
        _sellManager.SetOrderCost(correctProductAmount);
    }

    private void ShowSellCloud()
    {
        ViewManager.Instance.SetCloudView(_sellCloud, _timeToCloudDisappear,
            _soundsData.BubbleAppearSound, _soundsData.BubbleDisappearSound);
    }

    private void ResizeCells(int productCount)
    {
        ViewManager.Instance.Resize(productCount, _orderLayout);
    }

    private void Clear()
    {
        ViewManager.Instance.Clear(_contentParent, _productsToSellView);
    }
}