using System;
using System.Collections.Generic;

namespace DatabaseFirst.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Namesurname { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? Createddate { get; set; }
        public string? Telnr1 { get; set; }
        public string? Telnr2 { get; set; }
    }
}
