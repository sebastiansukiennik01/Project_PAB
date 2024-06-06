using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set;}

        [Range(1, 5, ErrorMessage = "Value out of range")]
        public int Rate { get; set; }

        public int CityId { get; set; }

        [Required]
        [ForeignKey("CityId")]
        public City? City { get; set; }

    }
}
