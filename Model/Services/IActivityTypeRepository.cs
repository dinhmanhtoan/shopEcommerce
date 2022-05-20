namespace Model.Services;
public interface IActivityTypeRepository :IRepository<Activity>
{
    IQueryable<MostViewEntityDto> List();
}
