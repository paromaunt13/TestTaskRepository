using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage Content", menuName = "Storage/Storage Content")]
public class StorageData : ScriptableObject
{
    [field: SerializeField] public List<ProductItem> Products { get; private set; }
}