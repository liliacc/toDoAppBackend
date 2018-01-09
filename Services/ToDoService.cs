using System;
using System.Collections.Generic;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace toDoAppBackend.Services
{
    public interface IToDoService
    {

    }

    class ToDoService : IToDoService
    {
        private readonly ToDoDbContext context;

        public ToDoService(ToDoDbContext context)
        {
            this.context = context;
        }
    }
}
