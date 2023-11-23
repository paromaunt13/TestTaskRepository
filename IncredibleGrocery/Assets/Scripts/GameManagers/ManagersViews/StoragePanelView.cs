using UnityEngine;
using Zenject;

public class StoragePanelView : StorePanel
{
    [SerializeField] private StorageView storageView;
    [SerializeField] private AudioButton sellButton;
    [field: SerializeField] public AudioButton OpenWarehouseButton { get; private set; }
    
    private SellView _sellView;
    private OrderManager _orderManager;

    [Inject]
    private void Construct(OrderManager orderManager, SellManager sellManager)
    {
        _orderManager = orderManager;
        _sellView = sellManager.GetComponent<SellView>();
    }
       
    
    private void Start()
    {
        sellButton.Button.interactable = false;
        sellButton.Button.onClick.AddListener(() =>
        {
            sellButton.Button.interactable = false;
            _sellView.SetSellProducts();
            storageView.SaveProductsViewData();
        });
        
        _orderManager.OnOrderCreated += SetAvailableSelects;
        _orderManager.OnOrderCanceled += storageView.ResetViewValues;
        _orderManager.OnOrderCanceled += _sellView.ResetCurrentSellView;
        storageView.OnSelectsValueChanged += UpdateSellButtonState;
        storageView.OnProductIItemViewAdded += AddProductSellView;
        storageView.OnProductIItemViewRemoved += RemoveProductSellView;
    }

    private void OnDestroy()
    { 
        _orderManager.OnOrderCreated -= SetAvailableSelects;
        _orderManager.OnOrderCanceled -= storageView.ResetViewValues;
        _orderManager.OnOrderCanceled -= _sellView.ResetCurrentSellView;
        storageView.OnSelectsValueChanged -= UpdateSellButtonState;
        storageView.OnProductIItemViewAdded -= AddProductSellView;
        storageView.OnProductIItemViewRemoved -= RemoveProductSellView;
    }

    private void UpdateSellButtonState(int selectsAmount, int currentSelects) =>
        sellButton.Button.interactable = currentSelects == selectsAmount;

    private void SetAvailableSelects(Order order)
    {
        storageView.SelectsAmount = order.Products.Count;
        storageView.AbleToSelect = true;
    }
    
    private void AddProductSellView(ProductItemView productItemView) =>
        _sellView.ProductsToSellView.Add(productItemView);

    private void RemoveProductSellView(ProductItemView productItemView) =>
        _sellView.ProductsToSellView.Remove(productItemView);
}