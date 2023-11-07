using UnityEngine;

[CreateAssetMenu(fileName = "Product Item View Factory", menuName = "Factory/Product Item View Factory")]
public class ProductItemViewFactory : ScriptableObject
{
    [SerializeField] private ProductItemView productPrefab;

    public ProductItemView GetProductView(ProductItem product, Transform parent)
    {
        var productView = Instantiate(productPrefab, parent);

        productView.SetProductView(product);

        return productView;
    }
}