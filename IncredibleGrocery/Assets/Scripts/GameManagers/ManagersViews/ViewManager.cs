using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [field: SerializeField] public ProductItemViewFactory ProductItemFactory { get; private set; }
    private IEnumerator ShowCloud(GameObject cloud, float timeToWait)
    {
        cloud.SetActive(true);
        AudioManager.Instance.PlaySound(SoundType.BubbleAppearSound);
        yield return new WaitForSeconds(timeToWait);
        AudioManager.Instance.PlaySound(SoundType.BubbleDisappearSound);
        cloud.SetActive(false);
    }

    protected void SetCloudView(GameObject cloud, float timeToWait)
    {
        StartCoroutine(ShowCloud(cloud, timeToWait));
    }

    protected void ClearParent(Transform contentParent, List<ProductItemView> productItemViews)
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        productItemViews?.Clear();
    }
    
    protected void ClearProductItemsView(List<ProductItemView> productItemsView)
    {
        if (productItemsView == null) return;
        foreach (var productItem in productItemsView)
            productItem.Click -= OnProductViewClick;
        
        productItemsView.Clear();
    }
    
    protected  List<ProductItemView> GetProductsView(List<ProductItem> products, Transform contentParent, bool showAmountText)
    {
        var productsItemView = new List<ProductItemView>();
        foreach (var product in products)
        {
            product.currentAmount = product.baseAmount;
            var productItemView = ProductItemFactory.GetProductView(product, contentParent);
            productItemView.Click += OnProductViewClick;
            productItemView.SetState(ProductViewState.Unselected);
            productsItemView.Add(productItemView);
            if (!showAmountText)
                productItemView.AmountCounterText.gameObject.SetActive(false);
        }

        return productsItemView;
    }

    protected virtual void OnProductViewClick(ProductItemView productItemView)
    {
        productItemView.SwitchState();
    }
}