using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProductItemView : MonoBehaviour, IPointerClickHandler
{
    [Header("View settings")] 
    [SerializeField] private Image productItemImage;
    [SerializeField] private Image stateImage;
    [SerializeField] private Sprite selectedImage;
    [SerializeField] private Sprite correctImage;
    [SerializeField] private Sprite wrongImage;
    [SerializeField] private string counterTextFormat;
    [field: SerializeField] public TMP_Text AmountCounterText { get; set; }

    [Header("Transparency settings")]
    [SerializeField] private float inStockTransparency;
    [SerializeField] private float outOfStockTransparency;

    private Image _productImage;
    private Color _transparency;
    private bool _ableToSelect;

    public ProductViewState CurrentProductState { get; private set; }
    public ProductItem Product { get; private set; }
    public event Action<ProductItemView> Click;

    private void UpdateStateView(ProductViewState productState)
    {
        stateImage.gameObject.SetActive(true);

        switch (productState)
        {
            case ProductViewState.Selected:
                _ableToSelect = true;
                stateImage.sprite = selectedImage;
                break;
            case ProductViewState.Unselected:
                _ableToSelect = true;
                _transparency.a = inStockTransparency;
                stateImage.gameObject.SetActive(false);
                break;
            case ProductViewState.Correct:
                stateImage.sprite = correctImage;
                break;
            case ProductViewState.Wrong:
                stateImage.sprite = wrongImage;
                break;
            case ProductViewState.OutOfStock:
                _transparency.a = outOfStockTransparency;
                _ableToSelect = false;
                stateImage.gameObject.SetActive(false);
                break;
        }
        _productImage.color = _transparency;
    }
    
    public void SetProductView(ProductItem product)
    {
        _productImage = productItemImage;
        _productImage.sprite = product.icon;

        Product = product;
        UpdateCounterText(Product.currentAmount);
        stateImage.gameObject.SetActive(false);

        CurrentProductState = ProductViewState.Unselected;

        _transparency = _productImage.color;
    }

    public void UpdateCounterText(int currentAmount) =>
        AmountCounterText.text = string.Format(counterTextFormat, currentAmount.ToString());

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_ableToSelect) return;
        AudioManager.Instance.PlaySound(SoundType.ProductSelectSound);
        Click?.Invoke(this);
    }

    public void SwitchState()
    {
        CurrentProductState = CurrentProductState switch
        {
            ProductViewState.Unselected => ProductViewState.Selected,
            ProductViewState.Selected => ProductViewState.Unselected,
            _ => CurrentProductState
        };
        UpdateStateView(CurrentProductState);
    }

    public void SetState(ProductViewState productState)
    {
        CurrentProductState = productState;
        UpdateStateView(CurrentProductState);
    }
}