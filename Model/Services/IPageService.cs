namespace Model.Services;
public interface IPageService
{
    Task Create(Pages page);
    Task Update(Pages page);
    Task Delete(Pages page);

}
