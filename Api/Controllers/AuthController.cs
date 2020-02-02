using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace todoApi.Controllers {

    [Route("api/[controller]/[action]")]
    public class AuthController : Controller {
        
        public AuthController() {
            
        }
        
        [HttpGet]
        [ActionName("login-google")]
        public IActionResult LoginGoogle(string redirectPath = "/") {
            var authProperties = new AuthenticationProperties() {
                RedirectUri = redirectPath,
                IsPersistent = true 
            };

            return Challenge(authProperties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [ActionName("login-github")]
        public IActionResult LoginGitHub(string redirectPath = "/") {
            var authProperties = new AuthenticationProperties() { 
                RedirectUri = redirectPath,
                IsPersistent = true 
            };

            return Challenge(authProperties, "GitHub");
        }

        [HttpGet]
        [ActionName("login-microsoft")]
        public IActionResult LoginMicrosoft(string redirectPath = "/") {
            var authProperties = new AuthenticationProperties() { 
                RedirectUri = redirectPath,
                IsPersistent = true 
            };

            return Challenge(authProperties, MicrosoftAccountDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Authorize]
        [ActionName("get-user-information")]
        public JsonResult GetUserInformation() {
            object data = null;

            if (User.Identity.IsAuthenticated) {
                data = new {
                    identifier = User.FindFirst(item => item.Type == ClaimTypes.NameIdentifier)?.Value,
                    name = User.FindFirst(item => item.Type == ClaimTypes.Name)?.Value
                };
            }

            return new JsonResult(data);
        }
    }

}