using System.Collections.Generic;
using toDoAppBackend.Entities;

namespace toDoAppBackend.Services.ToDoService
{
    public interface IUserService
    {
        IEnumerable<User> getAllUsers();
        User getUser(int dealId, string username);
    }

    class UserService : IUserService
    {
        private readonly ToDoDbContext context;
        
        public UserService(ToDoDbContext context)
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
    }
}
