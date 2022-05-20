namespace Model.Events;
public class AfterOrderCreated : INotification
{
    public AfterOrderCreated(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
