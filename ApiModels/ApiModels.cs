using System.Collections.Generic;
using toDoAppBackend.Entities;

namespace toDoAppBackend.ApiModels
{
    public class ValidationResponse
    {
        public string Error { get; set; }
    }
    
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class RegisterResponse : ValidationResponse
    {
        public string Token { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginResponse : ValidationResponse
    {
        public string Token { get; set; }
    }
    
    public class AuthRequest
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }

    public class GetAllToDosRequest : AuthRequest
    {
    }
    
    public class GetAllToDosResponse : ValidationResponse
    {
        public List<ToDo> Todos { get; set; }
    }
    
    public class CreateTodoRequest : AuthRequest
    {
        public string TodoText { get; set; }
    }

    public class UpdateTodoRequest : AuthRequest
    {
        public int Id { get; set; }
        public string TodoText { get; set; }
    }

    public class DeleteTodoRequest : AuthRequest
    {
        public int Id { get; set; }
    }
}