namespace Model.Events;

public class UserSignedInHandler : INotificationHandler<UserSignedIn>
{
    private readonly IWorkContext _workContext;
    private readonly ICartService _cartService;

    public UserSignedInHandler(IWorkContext workContext, ICartService cartService)
    {
        _workContext = workContext;
        _cartService = cartService;
    }

    public async Task Handle(UserSignedIn user, CancellationToken cancellationToken)
    {
        var guestUser = await _workContext.GetCurrentUser();
        await _cartService.MigrateCart(guestUser.Id, user.UserId);
    }

}

