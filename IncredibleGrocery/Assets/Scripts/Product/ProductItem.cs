using System;
using UnityEngine;

[Serializable]
public class ProductItem
{
    public int price;
    public Sprite icon;
    public ProductType productType;
    public int baseAmount;
    public int currentAmount;
}