using Data.Repositories.Interfaces;
using Services.Interfaces;
using Model;


namespace Services
{
    public class TodoService : ServiceBase<Todo>, ITodoService
    {
        public TodoService(ITodoRepository repository) : base(repository)
        { }
    }
}