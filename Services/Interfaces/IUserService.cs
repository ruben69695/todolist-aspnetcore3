using System.Threading.Tasks;

using Services.Results;
using Models;

namespace Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<OperationResult> GetByCode(string code);
    }
}