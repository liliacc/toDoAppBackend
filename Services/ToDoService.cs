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
        List<ToDo> GetAllTodos();
        ValidationResponse UpdateTodo(UpdateTodoRequest request);
        ValidationResponse DeleteTodo(DeleteTodoRequest request);
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

            response.Todos = new List<ToDo>();
            todos.ForEach(t => response.Todos.Add(new ToDo {Id = t.Id, Text = t.Text}));

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
        public List<ToDo> GetAllTodos()
        {
            return context.ToDos.ToList();
        }

        public ValidationResponse UpdateTodo(UpdateTodoRequest request) {
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

            var todo = context.ToDos.Find(request.Id);

            if (todo == null) {
                response.Error = "No such todo";
                return response;
            }

            context.Entry(todo)
                .Reference(it => it.User)
                .Load();

            if (todo.User.Token != request.Token || todo.User.Name != request.Username) {
                response.Error = "This todo belongs to another user";
                return response;
            }

            todo.Text = request.TodoText;
            context.Entry(todo).State = EntityState.Modified;
            context.SaveChanges();

            return response;
        }

        public ValidationResponse DeleteTodo(DeleteTodoRequest request) {
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

            var todo = context.ToDos.Find(request.Id);

            if (todo == null) {
                response.Error = "No such todo";
                return response;
            }

            context.Entry(todo)
                .Reference(it => it.User)
                .Load();

            if (todo.User.Token != request.Token || todo.User.Name != request.Username) {
                response.Error = "This todo belongs to another user";
                return response;
            }

            context.Entry(todo).State = EntityState.Deleted;
            context.SaveChanges();

            return response;
        }
    }
}
