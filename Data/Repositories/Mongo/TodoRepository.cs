using Data.Repositories.Interfaces;
using Model;

namespace Data.Repositories.Mongo
{
    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(MongoContext context) : base(context)
        { }
    }
}