using UnityEngine;

[CreateAssetMenu(fileName = "Product Data", menuName = "Products/New Product Data")]
public class ProductData : ScriptableObject
{
    public ProductType ProductType;
    public int Price;
    public Sprite Icon;
}
