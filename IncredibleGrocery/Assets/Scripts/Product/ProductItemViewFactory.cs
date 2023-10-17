using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product Item View Factory", menuName = "Factory/Product Item View Factory")]
public class ProductItemViewFactory : ScriptableObject
{
    [SerializeField] private ProductItemView _productPrefab;

    public ProductItemView GetProductView(ProductItem product, Transform parent)
    {
        ProductItemView productView;

        productView = Instantiate(_productPrefab, parent);

        productView.SetProductView(product);

        return productView;
    }
}
