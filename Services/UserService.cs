using System;
using System.Threading.Tasks;
using System.Linq;

using Services.Interfaces;
using Services.Results;
using Data.Repositories.Interfaces;
using Models;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        { 
            _userRepository = repository;
        }

        public async Task<OperationResult> Create(User item)
        {
            var operation = new OperationResult();
            try
            {
                // First check if exists
                var user = await _GetByCode(item.Code);
                if (user != null) {
                    operation.Code = UserOperationCodes.Found;
                }
                else {
                    // If not exists create the new user
                    user = await _userRepository.Create(item);
                    operation.Code = UserOperationCodes.Created;
                }
                operation.Result = user;
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error creating a user",
                    Exception = ex,
                };
            }
            return operation;
        }

        public async Task<OperationResult> Get(string id)
        {
            var operation = new OperationResult();
            try
            {
                // First retrieve it from database
                var user = await _userRepository.Get(id);
                if (user != null) {
                    operation.Code = UserOperationCodes.Found;
                }
                else {
                    operation.Code = UserOperationCodes.NotFound;
                }
                operation.Result = user;
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting a user by the internal id",
                    Exception = ex
                };
            }
            return operation;
        }

        public async Task<OperationResult> GetAll()
        {
            var operation = new OperationResult();
            try
            {
                var users = await _userRepository.GetAll();
                operation.Code = UserOperationCodes.Retrieved;
                operation.Result = users;
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting a user by the internal id",
                    Exception = ex
                };
            }
            return operation;
        }

        public async Task<OperationResult> GetByCode(string code)
        {
            var operation = new OperationResult();
            try
            {
                // First retrieve it from database
                var user = await _GetByCode(code);
                if (user != null) {
                    operation.Code = UserOperationCodes.Found;
                }
                else {
                    operation.Code = UserOperationCodes.NotFound;
                }
                operation.Result = user;
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error getting a user by the user code",
                    Exception = ex
                };
            }
            return operation;
        }

        public async Task<OperationResult> Remove(User item)
        {
            var operation = new OperationResult();
            try
            {
                await _userRepository.Remove(item);
                if (await _IsUserRemoved(item)) {
                    operation.Code = UserOperationCodes.Removed;
                }
                else {
                    operation.Error = new GenericError {
                        Code = ErrorCodes.Unknow,
                        Description = "unknow error, trying to delete the user",
                    };
                }
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error removing a user item",
                    Exception = ex
                };
            }
            return operation;
        }

        public async Task<OperationResult> Remove(string id)
        {
            var operation = new OperationResult();
            try
            {
                await _userRepository.Remove(id);
                if (await _IsUserRemoved(id)) {
                    operation.Code = UserOperationCodes.Removed;
                }
                else {
                    operation.Error = new GenericError {
                        Code = ErrorCodes.Unknow,
                        Description = "unkown error, trying to delete the user by id"
                    };
                }
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error removing a user by id",
                    Exception = ex
                };
            }
            return operation;
        }

        public async Task<OperationResult> Update(string id, User item)
        {
            var operation = new OperationResult();
            try
            {
                await _userRepository.Update(id, item);
                operation.Code = UserOperationCodes.Updated;
            }
            catch (Exception ex)
            {
                operation.Error = new GenericError {
                    Code = ErrorCodes.InternalError,
                    Description = "internal error, trying to update the user",
                    Exception = ex
                };
            }
            return operation;
        }

        private async Task<User> _GetByCode(string code) {
            var userList = await _userRepository.Get(user => user.Code == code);
            return userList.FirstOrDefault();
        }

        private async Task<bool> _ExistByCode(string code) {
            return await _GetByCode(code) != null;
        }

        private async Task<bool> _ExistByInternalId(string id) {
            return await _userRepository.Get(id) != null;
        }

        private async Task<bool> _IsUserRemoved(User item) {
            return !(await _ExistByInternalId(item.Id));
        }

        private async Task<bool> _IsUserRemoved(string id) {
            return !(await _ExistByInternalId(id));
        }
    }
}