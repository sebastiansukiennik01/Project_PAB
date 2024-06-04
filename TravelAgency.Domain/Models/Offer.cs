using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        public int HotelId { get; set; }

        [Required]
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public int DepartureCityId { get; set; }

        [Required]
        [ForeignKey("DepartureCityId")]
        public virtual City? DepartureCity { get; set; }
    }
}
