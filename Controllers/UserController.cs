using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;
using toDoAppBackend.Services;

namespace toDoAppBackend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService UserService;
        
        
        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpPost("register")]
        public RegisterResponse Register([FromBody]RegisterRequest loginRequest)
        {
            return UserService.Register(loginRequest);
        }
        
        [HttpPost("login")]
        public LoginResponse Login([FromBody]LoginRequest loginRequest)
        {
            return UserService.Login(loginRequest);
        }
        
        // Only experimental
//        [HttpGet("all")]
//        public List<User> GetAllUsers()
//        {
//            return UserService.GetAllUsers();
//        }
    }
}
