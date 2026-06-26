using AutoMapper;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace IMDBAPI.Repository
{
    public class UserRepository : BaseRepository<UserDB>, IUserRepository
    {
        private readonly ConnectionString _connectionString;
        public UserRepository(IOptions<ConnectionString> options) : base(options.Value.IMDBDB)
        {
            _connectionString = options.Value;

        }

        public int CreateUser(UserDB user)
        {
            var query = @"Insert INTO Foundation.Users(Name,Password,Email)
             OUTPUT INSERTED.Id
               values (@Name,@Password,@Email)";
            return QueryPerson(query, user);
        }

        public UserDB GetUser(string email)
        {
            var query = @"Select * from Foundation.Users where email = @email";
            return Get(query, new {  email });
        }
    }
}
