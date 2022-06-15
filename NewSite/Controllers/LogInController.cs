using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Helper_s.Interface;
using NewSite.Models;
using NewSite.Service.Interface;
using System.Diagnostics;
using System.Security.Claims;

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
                return Unauthorized();
            }
            return Ok();
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