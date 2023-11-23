using UnityEngine;

[CreateAssetMenu(fileName = "Storage Data Config")]
public class StorageDataConfig : ScriptableObject
{
    [field: SerializeField] public StorageData StorageData { get; private set; }
    [field: SerializeField] public StorageData WarehouseData { get; private set; }
}
