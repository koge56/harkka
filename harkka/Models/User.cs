using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace harkka.models

{
    public class User
    {
        public long Id { get; set; }
        [MinLength(3)]
        [MaxLength(30)]
        public string username { get; set; }
        [MaxLength(30)]
        public string password { get; set; }
        [MaxLength(30)]
        [EmailAddress]
        public string? email { get; set; }
        public byte[]? Salt { get; set; }
        [MaxLength(30)]
        public string? firstName { get; set; }
        [MaxLength(30)]
        public string? lastName { get; set; }
        [MaxLength(30)]
        public DateTime? joindate { get; set; }
        public DateTime? lastlogin { get; set; }
        public bool deleted { get; set; }
    }
    public class UserDTO
    {
        [MaxLength(30)]
        public string username { get; set; }
        [MaxLength(30)]
        [EmailAddress]
        public string? email { get; set; }
        [MaxLength(30)]
        public string? firstName { get; set; }
        [MaxLength(30)]
        public string? lastName { get; set; }
        public DateTime? joindate { get; set; }
        public DateTime? lastlogin { get; set; }

        public bool deleted {  get; set; }


    }
}