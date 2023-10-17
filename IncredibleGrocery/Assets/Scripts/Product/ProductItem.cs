using UnityEngine;

[CreateAssetMenu(fileName = "Product Item", menuName = "Products/Product Item")]
public class ProductItem : ScriptableObject
{   
    [SerializeField] private ProductData _productData;
    public int Price => _productData.Price;
    public Sprite Icon => _productData.Icon;
    public ProductType ProductType => _productData.ProductType;
}
