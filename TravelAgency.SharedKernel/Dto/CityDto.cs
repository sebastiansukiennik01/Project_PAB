﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.SharedKernel.Dto
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string? Name { get; set; }

    }
}
