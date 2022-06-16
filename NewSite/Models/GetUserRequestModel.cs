using Microsoft.AspNetCore.Mvc;

namespace NewSite.Models
{
    public class GetUserRequestModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }

}
