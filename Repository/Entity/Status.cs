namespace Repository.Entity
{
    public class Status
    {
        public short Id { get; set; }
        public string ActiveStatus { get; set; }

        public User User { get; set; }
    }
}
