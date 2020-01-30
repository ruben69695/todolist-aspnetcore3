using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Services.Interfaces
{
    public interface IService<TModel>
    {
        Task<List<TModel>> GetAll();
        Task<TModel> Get(string id);
        Task<List<TModel>> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Create(TModel item);
        Task Update(string id, TModel item);
        Task Remove(TModel item);
        Task Remove(string id);
    }
}