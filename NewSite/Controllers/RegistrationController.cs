﻿using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.Models;

namespace Repository.Controllers
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
        [HttpGet("RegisterForm")]
        public async Task<IActionResult> RegisterForm()
        {
            return View("./RegisterForm");
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserRequestModel user)
        {
             await userService.AddUserAsync(user);

            return View("./RegisterForm2");
        }

        [HttpPost("AddUserDetails")]
        public async Task<IActionResult> AddUserDetail(UserDetailsRequestModel userDetails, IFormFile file)
        {
            await userService.AddUserDetailsAsync(userDetails);

            return View("./SignInForm");
        }
    }
}
