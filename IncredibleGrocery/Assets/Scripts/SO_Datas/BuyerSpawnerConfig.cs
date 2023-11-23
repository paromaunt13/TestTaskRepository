using UnityEngine;

[CreateAssetMenu(fileName = "Buyer Spawner Config")]
public class BuyerSpawnerConfig : ScriptableObject
{
    [field: SerializeField] public Buyer BuyerPrefab { get; private set; }
    [field: SerializeField] public float MinTimeToNewBuyer { get; private set; }
    [field: SerializeField] public float MaxTimeToNewBuyer { get; private set; }
    [field: SerializeField] public int MaxBuyersAmount { get; private set; }
}
