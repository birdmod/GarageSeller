using System.ComponentModel.DataAnnotations;

namespace GarageSeller.Models
{
    public class MotorType
    {
        public int ID { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Maximum string length error", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
