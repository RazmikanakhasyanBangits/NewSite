using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.Models;

namespace NewSite.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IUserService userService;

        public EmailController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("SendConde")]
        public async Task SendRestorePassword([FromQuery]ForgotPasswordModel model)
        {
            await userService.ForgotPassword(model);
        }
    }
}
