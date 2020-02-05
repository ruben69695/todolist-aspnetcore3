using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.Interfaces;
using Models;
using Services.Results;

namespace todoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody]User user) {
            IActionResult actionResult = null;
            try
            {
                var operation = await _userService.Create(user);
                if (operation.HasError) {
                    actionResult = BuildProblemResult(operation);
                    _logger.LogError(operation.Error.ToString());
                }
                else {
                    if (operation.Code == UserOperationCodes.Created) {
                        actionResult = Created("", operation.Result);
                    }
                    else if (operation.Code == UserOperationCodes.Found) {
                        actionResult = Ok(operation.Result);
                    }
                    else {
                        throw new ArgumentOutOfRangeException(nameof(operation.Code), operation.Code, "User code operation not supported");
                    }
                }
            }
            catch (Exception ex)
            {
                actionResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, ex.Message);
            }
            return actionResult;   
        }

        private IActionResult BuildProblemResult(OperationResult opResult) {
            if (opResult == null) {
                throw new ArgumentNullException(nameof(opResult), 
                    "the operation result parameter passed to the function can't be null");
            }
            return Problem(
                detail: opResult.Error.Description, 
                instance: string.Empty, 
                statusCode: StatusCodes.Status500InternalServerError, 
                title: opResult.Error.Code.ToString(),
                type: null
            );
        }        

    }
}