using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using Services.Interfaces;
using Services.Results;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService service, ILogger<TodoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("get-pending")]
        public async Task<IActionResult> GetPending()
        {
            IActionResult actionResult = null;
            string userCode = User.FindFirst(item => item.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var operationResult = await _service.GetPendingTodoItemsByUserId(userCode);
                if(operationResult.Code == TodoListOperationCodes.Retrieved) {
                    actionResult = Ok(operationResult);
                }
                else {
                    throw new InvalidOperationException("internal error");
                }
            }
            catch (Exception ex)
            {
                actionResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, ex.Message);
            }            

            return actionResult;
        }

        [HttpPost]
        [ActionName("mark-complete")]
        public async Task<IActionResult> MarkComplete([FromBody] Todo item)
        {
            IActionResult actionResult = null;
            string userCode = User.FindFirst(item => item.Type == ClaimTypes.NameIdentifier)?.Value;
            item.Completed = true;
            try
            {
                var operationResult = await _service.Update(item.Id, item);
                if (operationResult.Code == ErrorCodes.InternalError) {
                    throw new InvalidOperationException("internal error");
                }
                else {
                    actionResult = Ok(operationResult);
                }
            }
            catch (Exception ex)
            {
                actionResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, ex.Message);
            }
            return actionResult;
        }

        [HttpPost]
        [ActionName("mark-incomplete")]
        public async Task<IActionResult> MarkInComplete([FromBody] Todo item)
        {
            IActionResult actionResult = null;
            string userCode = User.FindFirst(item => item.Type == ClaimTypes.NameIdentifier)?.Value;
            item.Completed = false;
            try
            {
                var operationResult = await _service.Update(item.Id, item);
                if (operationResult.Code == ErrorCodes.InternalError) {
                    throw new InvalidOperationException("internal error");
                }
                else {
                    actionResult = Ok(operationResult);
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