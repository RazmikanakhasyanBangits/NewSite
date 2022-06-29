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
        private readonly ITokenService tokenService;

        public UserController(IUserService userService, IFriendRequestService friendRequestService, ITokenService tokenService)
        {
            _userService = userService;
            _friendRequestService = friendRequestService;
            this.tokenService = tokenService;
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
        public async Task SendFriendRequest([FromBody] int userId)
        {
            await _friendRequestService.SendFriendRequestAsync(userId);
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

        [AllowAnonymous]
        [HttpGet(nameof(GetUserToken))]
        public Dictionary<string,string> GetUserToken()
        {
            return tokenService.GetToken();
        }

        [HttpPost(nameof(SeachUsers))]
        public async Task<IEnumerable<UserSearchResultModel>> SeachUsers([FromBody] FindeUserModel user)
        {
            var result = await _userService.SearchUser(user);
            return result;
        }
    }
}
