using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage Content", menuName = "Storage/Storage Content")]
public class StorageData : ScriptableObject
{
    [SerializeField] private List<ProductItem> _products;

    public List<ProductItem> Products => _products;
}
