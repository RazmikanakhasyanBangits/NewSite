namespace Repository.Entity
{
    public class Status
    {
        public short Id { get; set; }
        public string ActiveStatus { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
