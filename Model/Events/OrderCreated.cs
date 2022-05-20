namespace Model.Events;
public class OrderCreated : INotification
{
    public OrderCreated(Order order)
    {
        Order = order;
    }

    public Order Order { get; }
}
