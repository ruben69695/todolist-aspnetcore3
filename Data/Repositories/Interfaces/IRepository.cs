using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<List<TModel>> GetAll();
        Task<List<TModel>> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Create(TModel item);
        Task Update(string id, TModel item);
        Task Remove(TModel item);
        Task Remove(string id);
    }
}