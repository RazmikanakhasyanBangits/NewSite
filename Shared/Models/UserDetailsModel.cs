﻿namespace Shared.Models
{
    public class UserDetailsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthData { get; set; }
        public short Age { get; set; }
        public string Photo { get; set; }
        public bool Gender { get; set; }

        public long UserId { get; set; }
    }
}
