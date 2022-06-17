using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewSite.Models;
using NewSite.Service.Interface;
using System.Diagnostics;

namespace NewSite.Controllers
{
    [Authorize]
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly IUserService userService;
        public LogInController(ILogger<LogInController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(GetUserRequestModel user)
        {
            var userInfo = await userService.GetUserInfoAsync(user);
            if (userInfo==null)
            {
                return RedirectToAction("SignInForm", "Registration");
            }
            return View("./UserPage",userInfo);
        }

        public  IActionResult UserPage()
        {
            return View();
        }

        [HttpGet("Notification")]
        public IActionResult Notification()
        {
            return BadRequest("Something Went Wrong");
        }

        [HttpGet("LogOut")]
        public  IActionResult LogOut()
        {
             userService.LogOut();
            return RedirectToAction("SignInForm", "Registration");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}