using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManagerView : MonoBehaviour
{
    [SerializeField] private OrderManager _orderManager;
    [SerializeField] private StoragePanel _storagePanel;

    [Header("Order view")]
    [SerializeField] private GameObject _orderCloud;  
    [SerializeField] private Transform _contentParent;
    [SerializeField] private ProductItemViewFactory _productItemFactory;

    [Header("View settings")]
    [SerializeField] private SoundsData _soundsData;
    [SerializeField] private float _timeToCloudDisappear;

    private GridLayoutGroup _orderLayout;

    private List<ProductItem> _orderList = new();

    private readonly List<ProductItemView> _orderItemsView;

    private void Start()
    {
        _orderLayout = _contentParent.GetComponent<GridLayoutGroup>();
        
        EventBus.OnOrderCreated += SetProductView;
    }

    private void OnDisable()
    {
        EventBus.OnOrderCreated -= SetProductView;
    }

    private IEnumerator ShowOrderCloud()
    {
        ViewManager.Instance.SetCloudView(_orderCloud, _timeToCloudDisappear, 
            _soundsData.BubbleAppearSound, _soundsData.BubbleDisappearSound);

        yield return new WaitForSeconds(_timeToCloudDisappear);

        EventBus.OnOrderViewCreated?.Invoke();
    }

    private void ResizeCells(int productCount)
    {
        ViewManager.Instance.Resize(productCount, _orderLayout);
    }

    private void Clear()
    {
        ViewManager.Instance.Clear(_contentParent, _orderItemsView);    
    }

    public void SetProductView(Order order)
    {
        Clear();

        _orderList = order.Products;

        ResizeCells(_orderList.Count);

        foreach (var product in _orderList)
        {
            _productItemFactory.GetProductView(product, _contentParent);
        }

        StartCoroutine(ShowOrderCloud());
    }
}