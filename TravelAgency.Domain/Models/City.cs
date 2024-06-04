using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public int CountryId { get; set; }

        [Required]
        [ForeignKey("CountryId")]
        public Country? Country { get; set; }

        public City() { }
        public City(string? name, Country? country)
        {
            Name = name;
            Country = country;
        }
    }
}
