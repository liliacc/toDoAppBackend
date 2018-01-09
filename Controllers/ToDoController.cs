using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using toDoAppBackend.Entities;
using toDoAppBackend.Services;

namespace toDoAppBackend.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly IToDoService toDoService;
        
        
        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }
        
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return null;
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
