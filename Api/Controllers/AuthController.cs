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
        public IActionResult LoginGoogle(string redirectPath = "/") {
            var authProperties = new AuthenticationProperties() {
                RedirectUri = redirectPath,
                IsPersistent = true 
            };

            return Challenge(authProperties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult LoginGitHub(string redirectPath = "/") {
            var authProperties = new AuthenticationProperties() { 
                RedirectUri = redirectPath,
                IsPersistent = true 
            };

            return Challenge(authProperties, "GitHub");
        }

        [HttpGet]
        public IActionResult LoginMicrosoft(string redirectPath = "/") {
            var authProperties = new AuthenticationProperties() { 
                RedirectUri = redirectPath,
                IsPersistent = true 
            };

            return Challenge(authProperties, MicrosoftAccountDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Authorize]
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