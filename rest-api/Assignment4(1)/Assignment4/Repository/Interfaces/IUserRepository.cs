using IMDBAPI.Models.DBModels;

namespace IMDBAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        int CreateUser(UserDB user);

        UserDB GetUser(string email);
    }
}
