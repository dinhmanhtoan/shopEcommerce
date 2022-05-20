namespace Model.Services;

public interface IWorkContext
{
    Task<User> GetCurrentUser();
}

