using Microsoft.AspNetCore.Mvc;

namespace NewSite.Models
{
    public class AddUserRequestModel 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
