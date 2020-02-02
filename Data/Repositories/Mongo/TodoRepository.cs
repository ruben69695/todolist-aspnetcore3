using Data.Repositories.Interfaces;
using Models;

namespace Data.Repositories.Mongo
{
    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(MongoContext context) : base(context)
        { }
    }
}