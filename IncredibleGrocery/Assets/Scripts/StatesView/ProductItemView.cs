using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProductItemView : MonoBehaviour, IPointerClickHandler
{
    [Header("View settings")]
    [SerializeField] private Image _stateImage;

    [SerializeField] private Sprite _selectedImage;
    [SerializeField] private Sprite _wrongImage;

    [SerializeField] private SoundsData _soundsData;

    [Header("Transparency settings")]
    [SerializeField] private float _unselectedTransparency;
    [SerializeField] private float _selectedTransparency;

    private Image _productImage;

    private Color _transparency;

    private ProductViewState _productState;
    public ProductViewState CurrentProductState => _productState;

    public ProductItem Product { get; private set; }

    public event Action<ProductItemView> Click;

    private void UpdateStateView(ProductViewState productState)
    {
        _stateImage.gameObject.SetActive(true);

        switch (productState)
        {
            case ProductViewState.Selected:
                _transparency.a = _selectedTransparency;
                _stateImage.sprite = _selectedImage;
                break;
            case ProductViewState.Unselected:
                _transparency.a = _unselectedTransparency;
                _stateImage.gameObject.SetActive(false);
                break;
            case ProductViewState.Correct:
                _transparency.a = _selectedTransparency;
                _stateImage.sprite = _selectedImage;
                break;
            case ProductViewState.Wrong:
                _transparency.a = _selectedTransparency;               
                _stateImage.sprite = _wrongImage;             
                break;
        }

        _productImage.color = _transparency;        
    }

    private void Select()
    {
        _productState = ProductViewState.Selected;
        UpdateStateView(_productState);
    }

    private void Unselect()
    {
        _productState = ProductViewState.Unselected;
        UpdateStateView(_productState);
    }

    public void SetProductView(ProductItem product)
    {
        _productImage = GetComponent<Image>();
        _productImage.sprite = product.Icon;

        Product = product;

        _stateImage.gameObject.SetActive(false);

        _productState = ProductViewState.Unselected;

        _transparency = _productImage.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(_soundsData.ProductSelectSound);
        Click?.Invoke(this);
    }

    public void SwitchState()
    {
        if (_productState == ProductViewState.Unselected)
        {
            Select();
        }
        else if (_productState == ProductViewState.Selected)
        {
            Unselect();
        }
    }

    public void SetState(ProductViewState productState)
    {
        _productState = productState;
        UpdateStateView(_productState);
    }
}