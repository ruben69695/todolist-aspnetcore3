using Data.Repositories.Interfaces;
using Models;

namespace Data.Repositories.Mongo
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MongoContext context) : base(context)
        { }
    }
}