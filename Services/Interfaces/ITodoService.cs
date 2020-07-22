using System.Threading.Tasks;
using Models;
using Services.Results;

namespace Services.Interfaces
{
    public interface ITodoService : IService<Todo>
    { 
        Task<OperationResult> GetTodoItemByUserId(string itemId, string userId);
        Task<OperationResult> GetTodoListByUserId(string userId);
        Task<OperationResult> GetPendingTodoItemsByUserId(string userId);
    }
}