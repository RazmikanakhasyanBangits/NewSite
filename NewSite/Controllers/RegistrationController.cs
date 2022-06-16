using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Models;
using NewSite.Service.Impl;
using NewSite.Service.Interface;
using System.Security.Claims;

namespace NewSite.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IUserService userService;
        public RegistrationController(IHttpContextAccessor accessor, IUserService userService)
        {
            this.accessor = accessor;
            this.userService = userService;
        }

        public async Task<IActionResult> SignInForm()
        {
            return View();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserRequestModel user)
        {
           await userService.AddUserAsync(user);

            return View("./RegisterForm2");
        }

        [HttpPost("AddUserDetails")]
        public async Task<IActionResult> AddUserDetail(UserDetailsRequestModel userDetails)
        {
            await userService.AddUserDetailsAsync(userDetails);

            return View("./SignInForm");
        }
    }
}
