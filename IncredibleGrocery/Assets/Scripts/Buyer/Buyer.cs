using System;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    public Action OnBuyerEnter;
    public Action OnBuyerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<OrderZoneTrigger>())
            OnBuyerEnter?.Invoke();
        if (other.GetComponent<ExitZoneTrigger>())
            OnBuyerExit?.Invoke();
    }
}