namespace Shop.Components;

public class WishListBadgeViewComponent : ViewComponent
{
    private readonly IRepository<WishList> _wishListRepository;
    private readonly IWorkContext _workContext;
    public WishListBadgeViewComponent(IRepository<WishList> wishListRepository, IWorkContext workContext)
    {
        _wishListRepository = wishListRepository;
        _workContext = workContext;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _workContext.GetCurrentUser();
        var wishlish = await _wishListRepository.Query().Include(x => x.Items).FirstOrDefaultAsync(x => x.UserId == user.Id);
        var count = wishlish != null ? wishlish.Items.Count() : 0;
        return View(count);
    }
}

