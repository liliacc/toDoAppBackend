using System;
using toDoAppBackend.ApiModels;
using toDoAppBackend.Entities;

namespace toDoAppBackend.Services
{
    public interface IToDoService
    {
        GetAllToDosResponse GetAllToDosByUser(GetAllToDosRequest request);
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
            
            throw new NotImplementedException();
        }
    }
}
