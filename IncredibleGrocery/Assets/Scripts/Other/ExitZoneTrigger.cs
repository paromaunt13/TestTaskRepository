using UnityEngine;

public class ExitZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Buyer>())
        {
            EventBus.OnBuyerExit?.Invoke();
        }
    }
}