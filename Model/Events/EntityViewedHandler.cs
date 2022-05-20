namespace Model.Events;

public class EntityViewedHandler : INotificationHandler<EntityViewed>
{
    private const string ProductEntityTypeId = "Product";
    private readonly IRepository<RecentlyViewedProduct> _recentlyViewedProductRepository;
    private readonly IWorkContext _workContext; 
    public EntityViewedHandler(IRepository<RecentlyViewedProduct> recentlyViewedProductRepository, IWorkContext workContext)
    {
        _recentlyViewedProductRepository = recentlyViewedProductRepository;
        _workContext = workContext;
    }
    public async Task Handle(EntityViewed notification, CancellationToken cancellationToken)
    {
        if (notification.EntityTypeId == ProductEntityTypeId)
        {
            var user = await _workContext.GetCurrentUser();
            var recentlyViewedProduct = await _recentlyViewedProductRepository.Query().FirstOrDefaultAsync(x => x.UserId == user.Id && x.ProductId == notification.EntityId);
            if (recentlyViewedProduct == null)
            {
                recentlyViewedProduct = new RecentlyViewedProduct
                {
                    UserId = user.Id,
                    ProductId = notification.EntityId,
                    LatestViewedOn = DateTimeOffset.Now
                };
                _recentlyViewedProductRepository.Add(recentlyViewedProduct);
            }
            recentlyViewedProduct.LatestViewedOn = DateTimeOffset.Now;
            await _recentlyViewedProductRepository.SaveChangesAsync();
        }
 
    }
}

