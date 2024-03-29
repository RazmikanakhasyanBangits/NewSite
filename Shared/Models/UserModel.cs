﻿namespace Shared.Models
{
    public class UserModel
    {
        public long? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserDetailsModel Details { get; set; }

        public short RoleId { get; set; }
    }
}
