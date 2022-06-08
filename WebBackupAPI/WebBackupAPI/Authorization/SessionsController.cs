using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBackupAPI.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private AuthenticationService auth = new AuthenticationService();

        [HttpPost]
        public JsonResult Login(string password)
        {
            try
            {
                return new JsonResult(this.auth.Authenticate(password));
            }
            catch
            {
                return new JsonResult("Invalid username or password") { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
