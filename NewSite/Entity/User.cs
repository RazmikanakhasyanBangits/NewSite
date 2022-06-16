namespace NewSite.Entity
{
    public class User
    {
        public long? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserDetails Detail { get; set; }
        public short RoleId { get; set; }
        public Role Role { get; set; }
    }
}
