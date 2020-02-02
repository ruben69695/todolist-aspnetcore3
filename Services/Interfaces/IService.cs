using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Services.Results;

namespace Services.Interfaces
{
    public interface IService<TModel>
    {
        Task<OperationResult> GetAll();
        Task<OperationResult> Get(string id);
        Task<OperationResult> Create(TModel item);
        Task<OperationResult> Update(string id, TModel item);
        Task<OperationResult> Remove(TModel item);
        Task<OperationResult> Remove(string id);
    }
}