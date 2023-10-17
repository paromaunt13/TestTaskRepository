using System;

public class EventBus
{
    public static Action OnBuyerEnter;
    public static Action<Order> OnOrderCreated;
    public static Action OnOrderViewCreated;
    public static Action OnOrderComplete;
    public static Action<int> OnMoneyValueChanged;
    public static Action OnBuyerExit;
}
