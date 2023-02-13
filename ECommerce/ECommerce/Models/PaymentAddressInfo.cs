using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class PaymentAddressInfo
    {
        [StringLength(10)]
        public string FirstName { get; set; }
        [StringLength(10)]
        public string LastName { get; set; }
        [StringLength(10)]
        public string Address { get; set; }
        [StringLength(10)]
        public string City { get; set; }
        [StringLength(10)]
        public string PostCode { get; set; }
        public Country Country { get; set; }
        public string Region { get; set; }
    }
}