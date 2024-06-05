using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Domain.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
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
