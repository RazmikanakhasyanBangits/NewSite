namespace Repository.Entity
{
    public class OldPasswords
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string CreationDate { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
