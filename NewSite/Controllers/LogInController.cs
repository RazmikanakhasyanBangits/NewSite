using Helper_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using Shared.Models;
using Shared.Models.Enums;

namespace Repository.Controllers
{
    [Authorize]
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor accessor;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IAbstractCaching abstractCaching;


        public LogInController(ILogger<LogInController> logger, IUserService userService, 
            IHttpContextAccessor accessor, IServiceScopeFactory serviceScopeFactory, IAbstractCaching abstractCaching)
        {
            _logger = logger;
            this.userService = userService;
            this.accessor = accessor;
            this.serviceScopeFactory = serviceScopeFactory;
            this.abstractCaching = abstractCaching;
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
        [HttpGet("test")]
        public async Task Test()
        {
            await userService.Test1();
        }
        [AllowAnonymous]
        [HttpPost(nameof(VerifyAccount))]
        public async Task<IActionResult> VerifyAccount(VerifyAccountModel model)
        {
            await userService.VerifyAccountAsync(model);
            return RedirectToAction("SignInForm", "Registration");
        }
        public async Task<IActionResult> UserPage()
        {
            var user = await  abstractCaching.GetAsync<User>(CachKeys.UserKey);
            return View("./UserPage", user);
        }

        [HttpGet("Notification")]
        public IActionResult Notification() => BadRequest("Something Went Wrong");

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            userService.LogOut();
            return RedirectToAction("SignInForm", "Registration");
        }
        public IActionResult Privacy() => View();

        [AllowAnonymous]
        [HttpGet(nameof(VerificationForm))]
        public IActionResult VerificationForm() => View();
    }
}