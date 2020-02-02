using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;
using Models;
using Microsoft.Extensions.Logging;

namespace todoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody]User user) {
            IActionResult actionResult = null;
            try
            {
                var operation = await _userService.Create(user);
                if (operation.HasError) {
                    actionResult = Problem(
                        operation.Error.Description, 
                        string.Empty, 
                        StatusCodes.Status500InternalServerError, 
                        operation.Error.Code.ToString(),
                        null
                    );
                    _logger.LogError(operation.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                actionResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, ex.Message);
            }
            return actionResult;   
        }
    }
}