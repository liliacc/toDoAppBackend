using Microsoft.AspNetCore.Mvc;
using toDoAppBackend.ApiModels;
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
        
        [HttpPost("get-all")]
        public GetAllToDosResponse GetAll([FromBody]GetAllToDosRequest request)
        {
            return toDoService.GetAllToDosByUser(request);
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
