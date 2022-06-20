using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.Models;

namespace NewSite.Controllers
{
    public class EmailController : Controller
    {
        private readonly IUserService userService;

        public EmailController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("SendCode")]
        public async Task SendRestorePassword([FromQuery]ForgotPasswordModel model)
        {
            await userService.ForgotPassword(model);
        }
    }
}
