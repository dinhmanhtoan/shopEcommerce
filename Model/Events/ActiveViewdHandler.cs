namespace Model.Events;
public class ActiveViewdHandler : INotificationHandler<EntityViewed>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IWorkContext _workContext;
    private const long EntityViewedActivityTypeId = 1;
    public ActiveViewdHandler(IRepository<Activity> activityRepository, IWorkContext workContext)
    {
        _activityRepository = activityRepository;
        _workContext = workContext;
    }
    public async Task Handle(EntityViewed notification, CancellationToken cancellationToken)
    {
        var user = await _workContext.GetCurrentUser();
        var activity = new Activity
        {
            ActivityTypeId = EntityViewedActivityTypeId,
            EntityId = notification.EntityId,
            EntityTypeId = notification.EntityTypeId,
            UserId = user.Id,
            CreatedOn = DateTimeOffset.Now
        };
        _activityRepository.Add(activity);
    }
}
