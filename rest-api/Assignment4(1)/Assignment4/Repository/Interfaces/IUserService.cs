using IMDBAPI.Models.RequestModels;

namespace IMDBAPI.Repository.Interfaces
{
    public interface IUserService
    {
        public string CreateUser(UserRequest user);
        public string LoginUser(string email, string password);
    }
}
