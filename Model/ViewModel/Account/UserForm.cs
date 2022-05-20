namespace Model.ViewModel.Account;
public class UserForm
{
    public long Id { get; set; }

    [Required(ErrorMessage = "The {0} field is required.")]
    public string FullName { get; set; }

    public long? VendorId { get; set; }
 
    [Required(ErrorMessage = "The {0} field is required.")]
    [EmailAddress]
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public IList<long> RoleIds { get; set; } = new List<long>();
    public IList<Role> roles { get; set; } = new List<Role>();

    public IList<SelectListItem> VendorItems = new List<SelectListItem>();
    public IList<long> CustomerGroupId { get; set; } = new List<long>();

    public IList<CustomerGroup> CustomerGroups = new List<CustomerGroup>();
}

