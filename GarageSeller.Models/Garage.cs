using System.ComponentModel.DataAnnotations;

    namespace GarageSeller.Models
{
    public class Garage
    {
        // Required is OK for entity, however since this class is not exposed on api side, the phone wont be taken into account
        public int ID { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Maximum garage name length", MinimumLength = 5)]
        public string GarageName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum garage address length", MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        // DataType attributes do NOT provide validation. Only for formatting such as view
        public string Email { get; set; }
    }
}
