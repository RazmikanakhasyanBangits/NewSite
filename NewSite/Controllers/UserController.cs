using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using Shared.Models;

namespace NewSite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendRequestService _friendRequestService;

        public UserController(IUserService userService, IFriendRequestService friendRequestService)
        {
            _userService = userService;
            _friendRequestService = friendRequestService;
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

        [HttpPost(nameof(SendFriendRequest))]
        public async Task SendFriendRequest(string email)
        {
            await _friendRequestService.SendFriendRequestAsync(email);
        }

        [HttpPost(nameof(RejectFriendRequest))]
        public async Task RejectFriendRequest(long id)
        {
            await _friendRequestService.RejectFriendRequest(id);
        }

        [HttpPost(nameof(AcceptFriendRequest))]
        public async Task<ErrorModel> AcceptFriendRequest(AddFriendRequestModel model)
        {
            return await _friendRequestService.AcceptFriendRequest(model);
        }
    }
}
