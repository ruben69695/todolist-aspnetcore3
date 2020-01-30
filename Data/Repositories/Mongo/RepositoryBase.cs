using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDB.Driver;

using Model;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Mongo
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : KeyBase
    {
        private readonly MongoContext _context;

        public RepositoryBase(MongoContext context)
        {
            _context = context;
        }

        public async Task<TModel> Create(TModel item)
        {
            await _context.Set<TModel>().InsertOneAsync(item);
            return item;
        }

        public async Task<List<TModel>> Get(Expression<Func<TModel, bool>> filter)
        {
            var data = await _context.Set<TModel>().FindAsync(filter);
            return await data.ToListAsync();
        }

        public async Task<TModel> Get(string id)
        {
            var data = await _context.Set<TModel>().FindAsync(element => element.Id == id);
            return await data.FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetAll()
        {
            var data = await _context.Set<TModel>().FindAsync(element => true);
            return await data.ToListAsync();
        }

        public async Task Remove(TModel item)
            => await _context.Set<TModel>().DeleteOneAsync(element => element.Id == item.Id);

        public async Task Remove(string id)
            => await _context.Set<TModel>().DeleteOneAsync(element => element.Id == id);

        public async Task Update(string id, TModel item)
            => await _context.Set<TModel>().ReplaceOneAsync(element => element.Id == id, item);
    }
}