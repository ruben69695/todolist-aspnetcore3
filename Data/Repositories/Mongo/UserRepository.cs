using Data.Repositories.Interfaces;
using Model;

namespace Data.Repositories.Mongo
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MongoContext context) : base(context)
        { }
    }
}