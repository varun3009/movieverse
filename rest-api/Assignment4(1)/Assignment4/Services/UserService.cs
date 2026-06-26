using AutoMapper;
using IMDBAPI.Exceptions;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace IMDBAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        public UserService(IUserRepository userRepo, IMapper mapper, JwtService jwtService)
        {
            _userRepository = userRepo;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        public string CreateUser(UserRequest user)
        {
            if (user == null || user.Email is null || user.Password is null || user.ConfirmPassword is null || user.Password.IsNullOrEmpty() || user.Name.IsNullOrEmpty())
            {
                throw new InvalidInputException("Few Fields are empty");
            }
            var isuser = _userRepository.GetUser(user.Email);
            if(isuser is not null)
            {
                throw new InvalidInputException("User already Exist");
            }

            if(!user.Password.Equals(user.ConfirmPassword))
            {
                throw new InvalidInputException("password doensn't match");
            }
            var userdb = _mapper.Map<UserDB>(user);
            var userId = _userRepository.CreateUser(userdb);
            return _jwtService.GenerateToken(userId, userdb.Email, userdb.Name);
        }

        public string LoginUser(string email, string password) { 
        
            if(email == null || password == null)
            {
                throw new InvalidInputException("Invalid Creds");
            }

            var user = _userRepository.GetUser(email);
            if(user is null || !user.Password.Equals(password))
            {
                throw new InvalidInputException("Not valid password");
            }
            return _jwtService.GenerateToken(user.Id, user.Email, user.Name);
        }
    }
}
