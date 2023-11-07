using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProductItemView : MonoBehaviour, IPointerClickHandler
{
    [Header("View settings")] 
    [SerializeField] private Image stateImage;

    [SerializeField] private Sprite selectedImage;
    [SerializeField] private Sprite wrongImage;

    [SerializeField] private SoundsData soundsData;

    [Header("Transparency settings")] 
    [SerializeField] private float unselectedTransparency;
    [SerializeField] private float selectedTransparency;

    private Image _productImage;

    private Color _transparency;

    public ProductViewState CurrentProductState { get; private set; }

    public ProductItem Product { get; private set; }

    public event Action<ProductItemView> Click;

    private void UpdateStateView(ProductViewState productState)
    {
        stateImage.gameObject.SetActive(true);

        switch (productState)
        {
            case ProductViewState.Selected:
                _transparency.a = selectedTransparency;
                stateImage.sprite = selectedImage;
                break;
            case ProductViewState.Unselected:
                _transparency.a = unselectedTransparency;
                stateImage.gameObject.SetActive(false);
                break;
            case ProductViewState.Correct:
                _transparency.a = selectedTransparency;
                stateImage.sprite = selectedImage;
                break;
            case ProductViewState.Wrong:
                _transparency.a = selectedTransparency;
                stateImage.sprite = wrongImage;
                break;
        }

        _productImage.color = _transparency;
    }
    
    public void SetProductView(ProductItem product)
    {
        _productImage = GetComponent<Image>();
        _productImage.sprite = product.icon;

        Product = product;

        stateImage.gameObject.SetActive(false);

        CurrentProductState = ProductViewState.Unselected;

        _transparency = _productImage.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(soundsData.ProductSelectSound);
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