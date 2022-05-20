namespace Model.Events;

public class UserSignedIn : INotification
{
    public long UserId { get; set; }
}

