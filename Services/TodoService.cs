using Data.Repositories.Interfaces;
using Services.Interfaces;
using Models;
using System.Threading.Tasks;
using Services.Results;
using System;
using System.Linq;

namespace Services
{
    public class TodoService : IService<Todo>, ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository repository)
        {
            _todoRepository = repository;
        }
        public async Task<OperationResult> Create(Todo item)
        {
            var operationResult = new OperationResult();
            try
            {
                if (!IsIdEmpty(item)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.IdViolation,
                        Description = "error creating a new todo item, todo item id must be empty for creation"
                    };
                } 
                else if (IsNameEmpty(item)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyName,
                        Description = "error creating a new todo item, you must provide a name to the todo item"
                    };
                } 
                else if (IsUserIdEmpty(item)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyUserIdentification,
                        Description = "error creating a new todo item, you must provide a user identification"
                    };
                } 
                else {
                    var itemCreated = await _todoRepository.Create(item);
                    if (itemCreated == null) {
                        operationResult.Error = new GenericError {
                            Code = ErrorCodes.Unknow,
                            Description = "unkown error creating the todo item"
                        };
                    } 
                    else {
                        operationResult.Code = UserOperationCodes.Created;
                        operationResult.Result = itemCreated;
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error creating the todo item",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> Get(string id)
        {
            var operationResult = new OperationResult();
            try
            {
                if (IsIdEmpty(id)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyUserIdentification,
                        Description = "error getting a todo item, you must provide the item id"
                    };
                }
                else {
                    var item = await _todoRepository.Get(id);
                    if (item == null) {
                        operationResult.Code = TodoListOperationCodes.NotFound;
                        operationResult.Result = null;
                    }
                    else {
                        operationResult.Result = TodoListOperationCodes.Found;
                        operationResult.Result = item;
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting a todo item by the id",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> GetAll()
        {
            var operationResult = new OperationResult();
            try
            {
                var list = await _todoRepository.GetAll();
                operationResult.Code = TodoListOperationCodes.Retrieved;
                operationResult.Result = list;
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting the complete todo list",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> GetTodoItemByUserId(string itemId, string userId)
        {
            var operationResult = new OperationResult();
            try
            {
                if (IsIdEmpty(itemId)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyId,
                        Description = "error getting a sepecific todo item for a user, you must also provide the todo item id"
                    };
                }
                else if (IsUserIdEmpty(userId)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyUserIdentification,
                        Description = "error getting a sepecific todo item for a user, you must provide the user id"
                    };
                }
                else {
                    var item = (await _todoRepository.Get(xitem => xitem.UserIdentifier == userId && xitem.Id == itemId)).FirstOrDefault();
                    if (item == null) {
                        operationResult.Code = TodoListOperationCodes.NotFound;
                        operationResult.Result = null;
                    }
                    else {
                        operationResult.Code = TodoListOperationCodes.Found;
                        operationResult.Result = item;
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting a specific todo item for a user",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> GetTodoListByUserId(string userId)
        {
            var operationResult = new OperationResult();
            try
            {
                if (IsUserIdEmpty(userId)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyUserIdentification,
                        Description = "error getting the todo list for a user, you must provide the user id"
                    };
                }
                else {
                    var list = await _todoRepository.Get(item => item.UserIdentifier == userId);
                    operationResult.Code = TodoListOperationCodes.Retrieved;
                    operationResult.Result = list;
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting the todo list for a user",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> Remove(Todo item)
        {
            var operationResult = new OperationResult();
            try
            {
                if (item == null) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.NullItem,
                        Description = "error removing a todo item, you must provide an item object"
                    };
                }
                else if (IsIdEmpty(item.Id)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyId,
                        Description = "error removing a todo item, you must provide the item id"
                    };
                }
                else {
                    await _todoRepository.Remove(item);
                    operationResult.Code = TodoListOperationCodes.Removed;
                    operationResult.Result = null;
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error removing a todo item",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> Remove(string id)
        {
            var operationResult = new OperationResult();
            try
            {
                if (IsIdEmpty(id)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyId,
                        Description = "error removing a todo item, you must provide the item id"
                    };
                }
                else {
                    await _todoRepository.Remove(id);
                    operationResult.Code = TodoListOperationCodes.Removed;
                    operationResult.Result = null;
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error removing a todo item",
                    Exception = ex
                };
            }
            return operationResult;
        }

        public async Task<OperationResult> Update(string id, Todo item)
        {
            var operationResult = new OperationResult();
            try
            {
                if (IsIdEmpty(id) || IsIdEmpty(item)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyId,
                        Description = "error updating a sepecific todo item, you must also provide the todo item id"
                    };
                }
                else if (IsNameEmpty(item)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyName,
                        Description = "error updating a specific todo item, you must provide a name to the todo item"
                    };
                }
                else if (IsUserIdEmpty(item)) {
                    operationResult.Error = new GenericError {
                        Code = TodoListOperationCodes.EmptyUserIdentification,
                        Description = "error updating a specific todo item, you must provide a user identification for the todo item"
                    };
                }
                else {
                    await _todoRepository.Update(id, item);
                    operationResult.Code = TodoListOperationCodes.Updated;
                    operationResult.Result = null;
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error updating a todo item",
                    Exception = ex
                };
            }
            return operationResult;
        }

        private bool IsIdEmpty(Todo item) {
            return IsIdEmpty(item.Id);
        }

        private bool IsNameEmpty(Todo item) {
            return IsNameEmpty(item.Name);
        }

        private bool IsUserIdEmpty(Todo item) {
            return IsUserIdEmpty(item.UserIdentifier);
        }

        private bool IsIdEmpty(string id) {
            return IsNullOrEmpty(id);
        }

        private bool IsNameEmpty(string name) {
            return IsNullOrEmpty(name);
        }

        private bool IsUserIdEmpty(string userId) {
            return IsNullOrEmpty(userId);
        }

        private bool IsNullOrEmpty(string item) {
            return string.IsNullOrEmpty(item);
        }
    }
}