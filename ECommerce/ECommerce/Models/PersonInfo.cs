using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class PersonInfo
    {
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string Email { get; set; }
        [MaxLength(10)]
        public string Telephone { get; set; }
        [MaxLength(32)]
        public string Password { get; set; }
    }
}