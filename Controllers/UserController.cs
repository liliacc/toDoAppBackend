using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return toDoService.getAllUsers();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
