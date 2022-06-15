using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Helper_s.Impl;
using NewSite.Helper_s.Interface;
using NewSite.Models;
using NewSite.Service.Interface;
using System.Diagnostics;
using System.Security.Claims;

namespace NewSite.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IConfiguration config;
        private readonly IHttpContextAccessor accessor;
        public LogInController(ILogger<LogInController> logger, IUserService userService, ITokenService tokenService,
            IConfiguration config, IHttpContextAccessor accessor)
        {
            _logger = logger;
            this.userService = userService;
            this.tokenService = tokenService;
            this.config = config;
            this.accessor = accessor;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddUser(User user)
        {
            var userId = accessor.HttpContext?
                                .GetClaimValueFromToken(ClaimTypes.Email).ToString();
            accessor.HttpContext.Session.GetString("Token");
            await userService.AddUserAsync(user);


            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(User user)
        {
            string generateToken = null;
            var userInfo = await userService.GetUserInfoAsync(user);
            if (userInfo != null)
            {
                generateToken = tokenService.BuildToken(config["Jwt:Key"].ToString(), config["Jwt:Issuer"].ToString(), userInfo);
                if (generateToken != null)
                {
                    accessor.HttpContext.Session.SetString("Token", generateToken);
                }
                return RedirectToAction("AddUser","LogIn");
            }
            else
            {
                return Unauthorized();
            }
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