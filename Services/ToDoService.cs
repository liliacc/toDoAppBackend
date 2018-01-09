using System;
using System.Collections.Generic;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace toDoAppBackend.Services.ToDoService
{
    public interface IToDoService
    {
        IEnumerable<User> getAllUsers();
        User getUser(int dealId, string username);
        RegisterResponse Register(RegisterRequest loginRequest);
        LoginResponse Login(LoginRequest loginRequest);
    }

    class ToDoService : IToDoService
    {
        private readonly ToDoDbContext context;
        
        public ToDoService(ToDoDbContext context)
        {
            this.context = context;
        }

        
        public IEnumerable<User> getAllUsers()
        {
            return context.Users;
        }

        public User getUser(int dealId, string username)
        {
            throw new System.NotImplementedException();
        }

        public RegisterResponse Register(RegisterRequest loginRequest)
        {
            RegisterResponse response = new RegisterResponse();
            if (String.IsNullOrEmpty(loginRequest.Username))
            {
                response.Error = "Username is empty";
                return response;
            }
            if (String.IsNullOrEmpty(loginRequest.Password))
            {
                response.Error = "Password is empty";
                return response;
            }
            User user;
            if (context.Users.Any())
            {
                user = context.Users.FirstOrDefault(p => p.Name.ToLower() == loginRequest.Username.ToLower());
                if (user != null)
                {
                    response.Error = "User already exists";
                    return response;
                }
                
            }

            user = new User();
            user.Name = loginRequest.Username;
            user.Password = loginRequest.Password;
            user.Token = Guid.NewGuid().ToString();
            context.Entry(user).State = EntityState.Added;
            context.SaveChanges();
            response.Token = user.Token;
            return response;

        }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
