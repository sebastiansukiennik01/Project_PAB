using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.SharedKernel.Dto
{
    public class UpdateCityDto
    {
        public int CityId { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }
    }
}
