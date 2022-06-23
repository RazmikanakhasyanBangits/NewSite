namespace Repository.Entity
{
    public class FriendRequests
    {
        public long Id { get; set; }
        public long FromId { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
