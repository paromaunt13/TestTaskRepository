using System;
using UnityEngine;

public class OrderZone : MonoBehaviour
{
    public Action<Buyer> OnBuyerEnter;
    public Action<Buyer> OnBuyerLeaved;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Buyer>(out var buyer))
            OnBuyerEnter?.Invoke(buyer);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Buyer>(out var buyer))
            OnBuyerLeaved?.Invoke(buyer);
    }
}