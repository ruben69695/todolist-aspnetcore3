using Data.Repositories.Interfaces;
using Services.Interfaces;
using Models;
using System.Threading.Tasks;
using Services.Results;

namespace Services
{
    public class TodoService : IService<Todo>, ITodoService
    {
        public Task<OperationResult> Create(Todo item)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult> Remove(Todo item)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult> Remove(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult> Update(string id, Todo item)
        {
            throw new System.NotImplementedException();
        }
    }
}