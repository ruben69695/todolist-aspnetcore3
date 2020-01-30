using Services.Interfaces;
using Data.Repositories.Interfaces;
using Model;


namespace Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        { }
    }
}