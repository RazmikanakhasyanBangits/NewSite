namespace Repository.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public UserDetails Details { get; set; }

        public IEnumerable<Friend> Friends { get; set; }
        public IEnumerable<FriendRequests> FriendRequests { get; set; }

        public IEnumerable<OldPasswords> OldPasswords { get; set; }

        public short StatusId { get; set; }
        public Status Status { get; set; }

        public short RoleId { get; set; }
        public Role Role { get; set; }
    }
}
