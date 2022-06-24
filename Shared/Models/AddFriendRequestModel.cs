namespace Shared.Models
{
    public class AddFriendRequestModel
    {
        public long Id { get; set; }
        public long FromId { get; set; }

        public long UserId { get; set; }
    }
}
