using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "admin")]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly shopContext _context;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Vender> _venderRepository;
    private readonly IRepository<CustomerGroup> _customerGroupRepository;

    public UserController(UserManager<User> userManager,   SignInManager<User> signInManager, shopContext context,
        IRepository<User> userRepository, IRepository<Vender> venderRepository, IRepository<CustomerGroup> customerGroupRepository
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _userRepository = userRepository;
        _venderRepository = venderRepository;
        _customerGroupRepository = customerGroupRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var user = _context.Users.Include(x=>x.Roles).ThenInclude(x=>x.Role).Where(x=> !x.IsDeleted).ToList();
        return View(user);
    }
    [HttpPost]
    public IActionResult query()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var demo = Request.Form["order[0][column]"].FirstOrDefault();
        var searchValue = Request.Form["search[name]"].FirstOrDefault();
        var searchEmail = Request.Form["columns[1][search][value]"].FirstOrDefault();
        var searchFullname = Request.Form["columns[2][search][value]"].FirstOrDefault();
        //var searchRoles = Request.Form["columns[3][search][value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;
        var User = _userRepository.Query().Include(x => x.Roles).ThenInclude(x => x.Role).Where(x => !x.IsDeleted).AsQueryable();
        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            User = User.OrderBy(sortColumn + " " + sortColumnDirection);             
        }
        if (!string.IsNullOrEmpty(searchValue))
        {
            User = User.Where(m => m.FullName.Contains(searchValue));

        }
        if (!string.IsNullOrEmpty(searchEmail))
        {
            User = User.Where(m => m.Email.Contains(searchEmail));

        }
        if (!string.IsNullOrEmpty(searchFullname))
        {
            User = User.Where(m => m.FullName.Contains(searchFullname));

        }
        recordsTotal = User.Count();
        var data = User.Skip(skip).Take(pageSize).ToList();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        return Ok(jsonData);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var result = await _context.Roles.ToListAsync();
        var cutomerGroup = await _customerGroupRepository.Query().ToListAsync();
        var ListVender = await _venderRepository.Query().ToListAsync();
        var UserForm = new UserForm()
        {
            RoleIds = result.Select(x => x.Id).ToList(),
            roles = result,
            VendorItems = ListVender.Select(x=> new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList(),
            CustomerGroups = cutomerGroup
        };
        return View(UserForm);
    }
    [HttpPost]
    public async Task<IActionResult> Create(UserForm model)
    {
        if (ModelState.IsValid)
        {
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                VendorId = model.VendorId
            };
            foreach (var roleId in model.RoleIds)
            {
                var userRole = new UserRole
                {
                    RoleId = roleId
                };
                user.Roles.Add(userRole);
                userRole.User = user;
            }
            foreach (var customerGroupId in model.CustomerGroupId)
            {
                var CustomerGroupUser = new CustomerGroupUser
                {
                    CustomerGroupId = customerGroupId
                };
                user.CustomerGroups.Add(CustomerGroupUser);
                CustomerGroupUser.User = user;
            }

            var result = await _userManager.CreateAsync(user);
              
            if (result.Succeeded)
            {
                if (model.RoleIds.Count == 0)
                {
                    await _userManager.AddToRoleAsync(user, "customer");
                }
                return Accepted();
            }
            AddErrors(result);
        }

        return BadRequest(ModelState);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Edit(long Id)
    {
        if (ModelState.IsValid)
        {
            var result = await _context.Users.Include(x => x.Roles).ThenInclude(x=> x.Role)
                                .Include(x=> x.CustomerGroups).ThenInclude(x=> x.CustomerGroup)
                                .Where(x=> !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == Id);
            var roles = await _context.Roles.ToListAsync();
            var cutomerGroup = await _customerGroupRepository.Query().ToListAsync();
            var ListVender = await _venderRepository.Query().ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            var UserForm = new UserForm()
            {
                FullName = result.FullName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                RoleIds = result.Roles.Select(x => x.Role.Id).ToList(),
                roles = roles,
                CustomerGroupId = result.CustomerGroups.Select(x => x.CustomerGroup.Id).ToList(),
                CustomerGroups = cutomerGroup,
                VendorId = result.VendorId,
                VendorItems = ListVender.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = result.VendorId == x.Id ? true : false
                }).ToList(),

            };
            return View(UserForm);
        }
        return BadRequest(ModelState);
    }
    [HttpPost("{Id}")]
    public async Task<IActionResult> Edit(long Id,UserForm model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.Include(x=> x.Roles).Where(x=> !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return NotFound();
            }
            user.FullName = model.FullName;
            user.UserName = model.Email;
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.VendorId = model.VendorId;
            AddOrDeleteRoles(model, user);
            AddOrDeleteCustomerGroup(model, user);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Accepted();
            }

            AddErrors(result);
        }

        return BadRequest(ModelState);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long Id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
        if (user == null)
        {
            return NotFound();
        }
        user.IsDeleted = true;
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTime.Now.AddYears(200);
        await _context.SaveChangesAsync();
        return Accepted(); ;
    }
    private void AddOrDeleteRoles(UserForm model, User user)
    {
        foreach (var roleId in model.RoleIds)
        {
            if (user.Roles.Any(x => x.RoleId == roleId))
            {
                continue;
            }
            var userRole = new UserRole
            {
                RoleId = roleId,
                User = user
            };
            user.Roles.Add(userRole);
        }
        var deletedUserRoles =
            user.Roles.Where(userRole => !model.RoleIds.Contains(userRole.RoleId))
                .ToList();

        foreach (var deletedUserRole in deletedUserRoles)
        {
            deletedUserRole.User = null;
            user.Roles.Remove(deletedUserRole);
        }
    }
    private void AddOrDeleteCustomerGroup(UserForm userForm, User user)
    {
        foreach (var customerGroupId in userForm.CustomerGroupId)
        {
            if (user.CustomerGroups.Any(x=> x.CustomerGroupId == customerGroupId))
            {
                continue;
            };
            var customerGroupUser = new CustomerGroupUser
            {
                CustomerGroupId = customerGroupId,
                User = user
            };
            user.CustomerGroups.Add(customerGroupUser);
        }
        var deletedUserCustomerGroups =
                user.CustomerGroups.Where(userRole => !userForm.CustomerGroupId.Contains(userRole.CustomerGroupId))
        .ToList();
        foreach (var deletedUserCustomerGroup in deletedUserCustomerGroups)
        {
            deletedUserCustomerGroup.User = null;
            user.CustomerGroups.Remove(deletedUserCustomerGroup);
        }
    }
    private void AddErrors(IdentityResult result)
    {

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
