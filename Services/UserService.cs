using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;

namespace toDoAppBackend.Services
{
    public interface IUserService
    {
        RegisterResponse Register(RegisterRequest loginRequest);
        LoginResponse Login(LoginRequest loginRequest);
    }

    class UserService : IUserService
    {
        private readonly ToDoDbContext context;
        
        public UserService(ToDoDbContext context)
        {
            this.context = context;
        }

        
        public RegisterResponse Register(RegisterRequest registerRequest)
        {
            RegisterResponse response = new RegisterResponse();
            if (string.IsNullOrWhiteSpace(registerRequest.Username))
            {
                response.Error = "Username is empty";
                return response;
            }
            if (string.IsNullOrWhiteSpace(registerRequest.Password))
            {
                response.Error = "Password is empty";
                return response;
            }
            User user;
            if (context.Users.Any())
            {
                user = context.Users.FirstOrDefault(p => p.Name.ToLower() == registerRequest.Username.ToLower());
                if (user != null)
                {
                    response.Error = "User already exists";
                    return response;
                }
            }

            user = new User();
            user.Name = registerRequest.Username;
            user.Password = registerRequest.Password;
            user.Token = Guid.NewGuid().ToString();
            context.Entry(user).State = EntityState.Added;
            context.SaveChanges();
            response.Token = user.Token;
            return response;

        }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();
            if (string.IsNullOrWhiteSpace(loginRequest.Username))
            {
                response.Error = "Username is empty";
                return response;
            }
            if (string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                response.Error = "Password is empty";
                return response;
            }

            if (!context.Users.Any())
            {
                response.Error = "Such user doesn't exist";
                return response;
            }
            
            User user = context.Users.FirstOrDefault(p => p.Name.ToLower() == loginRequest.Username.ToLower());
            if (user == null)
            {
                response.Error = "Such user doesn't exist";
                return response;
            }

            if (user.Password != loginRequest.Password)
            {
                response.Error = "Wrong password";
                return response;
            }
            
            user.Token = Guid.NewGuid().ToString();
            
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            response.Token = user.Token;
            return response;
        }
    }
}
