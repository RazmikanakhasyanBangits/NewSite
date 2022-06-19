using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.Models;

namespace NewSite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet(nameof(ChangePasswordView))]
        public IActionResult ChangePasswordView()
        {
            return View("./ChangePassword");
        }

        [HttpPost("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordRequestModel model)
        {
            await _userService.ChangePasswordAsync(model);

            return RedirectToAction("UserPage", "LogIn");
        }
    }
}
