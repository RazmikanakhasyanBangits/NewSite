namespace Repository.Entity
{
    public class Friend
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public string Photo { get; set; }
        
        public long UserId { get; set; }
        public User User  { get; set; }
    }
}
