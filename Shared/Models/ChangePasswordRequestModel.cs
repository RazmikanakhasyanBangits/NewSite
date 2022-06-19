namespace Shared.Models
{
    public class ChangePasswordRequestModel
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPasword { get; set; }
    }
}
