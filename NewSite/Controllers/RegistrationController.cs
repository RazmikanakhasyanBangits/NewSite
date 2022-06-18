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

        public async Task<IActionResult> RegisterForm()
        {
            return View();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserRequestModel user)
        {
            await userService.AddUserAsync(user);


            return Ok();
        }
    }
}
