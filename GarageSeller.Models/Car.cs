using System;
using System.ComponentModel.DataAnnotations;

namespace GarageSeller.Models
{
    public class Car
    {
        public int ID { get; set; }
        public int TransmissionID { get; set; }
        public int MotorID { get; set; }

        public int GarageID { get; set; }
        public int SellerID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum string length error", MinimumLength = 3)]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public DateTime SoldDateUtc { get; set; }
        public string Comment { get; set; }

        public Garage Garage { get; set; }
        public Seller Seller { get; set; }


    }
}
