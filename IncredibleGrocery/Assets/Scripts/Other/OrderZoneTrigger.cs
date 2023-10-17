using UnityEngine;

public class OrderZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Buyer>())
        {
            EventBus.OnBuyerEnter?.Invoke();
        }
    }
}