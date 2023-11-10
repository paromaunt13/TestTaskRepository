using System;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public Action<Buyer> OnBuyerExit;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Buyer>(out var buyer))
            OnBuyerExit?.Invoke(buyer);
    }
}