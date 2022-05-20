namespace Shop.Components;

public class DefaultShippingAddressViewComponent : ViewComponent
{
    private readonly IRepository<UserAddress> _userAddressRepository;
    private readonly IWorkContext _workContext;

    public DefaultShippingAddressViewComponent(IRepository<UserAddress> userAddressRepository, IWorkContext workContext)
    {
        _userAddressRepository = userAddressRepository;
        _workContext = workContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var curentUser = await _workContext.GetCurrentUser();

        var model = new DefaultAddressViewComponentVm();

        if (curentUser.DefaultShippingAddressId.HasValue)
        {
            model.Address = await _userAddressRepository.Query()
                .Where(x => x.Id == curentUser.DefaultShippingAddressId.Value)
                .Select(x => new UserAddressListItem
                {
                    UserAddressId = x.Id,
                    ContactName = x.Address.ContactName,
                    Phone = x.Address.Phone,
                    AddressLine1 = x.Address.AddressLine1,
                    AddressLine2 = x.Address.AddressLine2,
                    DistrictName = x.Address.District.Name,
                    StateOrProvinceName = x.Address.StateOrProvince.Name,
                    CountryName = x.Address.Country.Name
                })
                .FirstOrDefaultAsync();
        }

        return View(model);
    }
}

