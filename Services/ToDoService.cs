using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;

namespace toDoAppBackend.Services
{
    public interface IToDoService
    {
        GetAllToDosResponse GetAllToDosByUser(GetAllToDosRequest request);
        ValidationResponse CreateTodo(CreateTodoRequest request);
        List<User> GetAllUsers();
    }

    class ToDoService : IToDoService
    {
        private readonly ToDoDbContext context;

        public ToDoService(ToDoDbContext context)
        {
            this.context = context;
        }

        public GetAllToDosResponse GetAllToDosByUser(GetAllToDosRequest request)
        {
            GetAllToDosResponse response = new GetAllToDosResponse();
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Token))
            {
                response.Error = "Need to login";
                return response;
            }

            User user = context.Users.FirstOrDefault(u => u.Name == request.Username && u.Token == request.Token);
            if (user == null)
            {
                response.Error = "Need to login";
                return response;
            }

            var todos = context.ToDos
                .Include(p => p.User)
                .Where(p => p.User == user)
                .ToList();

            response.Todos = new List<string>();
            todos.ForEach(t => response.Todos.Add(t.Text));

            return response;
        }

        public ValidationResponse CreateTodo(CreateTodoRequest request)
        {
            ValidationResponse response = new ValidationResponse();
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Token))
            {
                response.Error = "Need to login";
                return response;
            }

            User user = context.Users.FirstOrDefault(u => u.Name == request.Username && u.Token == request.Token);
            if (user == null)
            {
                response.Error = "Need to login";
                return response;
            }

            var todo = new ToDo();
            todo.User = user;
            todo.Text = request.TodoText;
            context.Entry(todo).State = EntityState.Added;
            context.SaveChanges();

            return response;
        }

        // Only experimental
        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }
    }
}
