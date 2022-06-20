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
        private readonly IServiceScopeFactory serviceScopeFactory;


        public LogInController(ILogger<LogInController> logger, IUserService userService, IHttpContextAccessor accessor, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            this.userService = userService;
            this.accessor = accessor;
            this.serviceScopeFactory = serviceScopeFactory;
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
        [HttpPost(nameof(VerifyAccount))]
        public async Task<IActionResult> VerifyAccount(VerifyAccountModel model)
        {
            await userService.VerifyAccountAsync(model);
            return RedirectToAction("SignInForm", "Registration");
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
        [AllowAnonymous]
        [HttpGet(nameof(VerificationForm))]
        public IActionResult VerificationForm()
        {
            return View();
        }
    }
}