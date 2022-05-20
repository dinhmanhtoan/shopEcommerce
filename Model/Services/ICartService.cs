namespace Model.Services;

public interface ICartService
{
    Task<Result> AddToCart(long customerId, long productId, int quantity);

    Task<Result> AddToCart(long customerId, long createdById, long productId, int quantity);
    Task<Cart> GetActiveCart(long customerId);

    Task<Cart> GetActiveCart(long customerId, long createdById);

    Task<CartVm> GetActiveCartDetails(long customerId);

    Task<CartVm> GetActiveCartDetails(long customerId, long createdById);
    Task<CouponValidationResult> ApplyCoupon(long cartId, string couponCode);
    Task UnlockCart(Cart cart);
    Task MigrateCart(long id, long userId);
}

