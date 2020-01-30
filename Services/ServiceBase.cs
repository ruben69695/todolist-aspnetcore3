using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Data.Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class ServiceBase<TModel> : IService<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _repository;

        public ServiceBase(IRepository<TModel> repository)
        {
            _repository = repository;
        }
        public async Task<TModel> Create(TModel item)
        {
            return await _repository.Create(item);
        }

        public async Task<TModel> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<TModel>> Get(Expression<Func<TModel, bool>> filter)
        {
            return await _repository.Get(filter);
        }

        public async Task<List<TModel>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Remove(TModel item)
        {
            await _repository.Remove(item);
        }

        public async Task Remove(string id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(string id, TModel item)
        {
            await _repository.Update(id, item);
        }
    }
}