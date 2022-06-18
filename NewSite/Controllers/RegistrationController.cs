using Microsoft.AspNetCore.Mvc;
using NewSite.Models;
using NewSite.Service.Interface;

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
        [HttpGet("RegisterForm")]
        public async Task<IActionResult> RegisterForm()
        {
            return View("./RegisterForm");
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserRequestModel user, IFormFile file)
        {
            var a = await userService.AddUserAsync(user);
            if (!a)
            {
                return View("./RegisterForm");
            }

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
