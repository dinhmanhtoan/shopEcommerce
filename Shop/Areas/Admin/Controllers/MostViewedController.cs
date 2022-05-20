namespace Shop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
[Route("[controller]/[action]")]
public class MostViewedController : Controller
{
    private readonly IActivityTypeRepository _activityTypeRepository;
    public MostViewedController(IActivityTypeRepository activityTypeRepository)
    {
        _activityTypeRepository = activityTypeRepository;
    }
    [HttpGet("most-viewed-entities/{entityTypeId}")]
    public async Task<IList<MostViewEntityDto>> GetMostViewedEntities(string entityTypeId)
    {
        return await _activityTypeRepository.List().Where(x => x.EntityTypeId == entityTypeId).Take(10).ToListAsync();
    }
}

