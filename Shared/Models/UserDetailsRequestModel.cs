namespace Shared.Models
{
    public class UserDetailsRequestModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthData { get; set; }
        public short Age { get; set; }
        public string Photo { get; set; }
        public bool Gender { get; set; }
    }
}
