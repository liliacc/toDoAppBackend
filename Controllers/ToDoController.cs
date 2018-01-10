﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("create")]
        public ValidationResponse CreateTodo([FromBody]CreateTodoRequest request)
        {
            return toDoService.CreateTodo(request);
        }
    }
}
