using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Contracts;
using TravelAgency.Domain.Models;

namespace TravelAgency.Infrastructure.Repositories
{
    public class OfferRepository : Repository<Offer>
    {
        private readonly DatabaseContext _travelAgencyDbContext;

        public OfferRepository(DatabaseContext context)
            : base(context)
        {
            _travelAgencyDbContext = context;
        }
    }
}
