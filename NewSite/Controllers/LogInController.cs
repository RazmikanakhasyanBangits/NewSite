using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using Shared.Models;
using System.Text.Json;

namespace Repository.Controllers
{
    [Authorize]
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor accessor;
        public LogInController(ILogger<LogInController> logger, IUserService userService, IHttpContextAccessor accessor)
        {
            _logger = logger;
            this.userService = userService;
            this.accessor = accessor;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(GetUserRequestModel user)
        {
            var userInfo = await userService.GetUserInfoAsync(user);
            if (userInfo == null)
            {
                return RedirectToAction("SignInForm", "Registration");
            }
            return View("./UserPage", userInfo);
        }

        [AllowAnonymous]
        [HttpPost("VerifyAccount")]
        public async Task VerifyAccount(VerifyAccountModel model)
        {
            await userService.VerifyAccountAsync(model);
        }
        public IActionResult UserPage()
        {
            var user = JsonSerializer.Deserialize<User>(accessor.HttpContext.Session.GetString("User"));
            return View("./UserPage", user);
        }

        [HttpGet("Notification")]
        public IActionResult Notification()
        {
            return BadRequest("Something Went Wrong");
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            userService.LogOut();
            return RedirectToAction("SignInForm", "Registration");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}