using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;
using toDoAppBackend.Services.ToDoService;

namespace toDoAppBackend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IToDoService toDoService;
        
        
        public UserController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [HttpPost("register")]
        public RegisterResponse Register([FromBody]RegisterRequest loginRequest)
        {
            return toDoService.Register(loginRequest);
        }
        
        [HttpPost("login")]
        public LoginResponse Login([FromBody]LoginRequest loginRequest)
        {
            return toDoService.Login(loginRequest);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
