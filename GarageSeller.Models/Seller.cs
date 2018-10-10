using System.ComponentModel.DataAnnotations;

namespace GarageSeller.Models
{
    public class Seller
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum first name length", MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum last name length", MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum garage address length", MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Maximum email length", MinimumLength = 6)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }
    }
}
