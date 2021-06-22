using FlightMaster.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMaster.DataAccess.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(FlightMasterDbContext context) : base(context)
        {

        }

        public async Task<City> GetCityDetails(int Id)
        {
            return await Query.Include(c => c.Country).SingleOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<IEnumerable<City>> GetMultipleCitiesDetails(IEnumerable<int> ids)
        {
            return await Query.Include(c => c.Country).Where(e => ids.Contains(e.Id)).ToListAsync();
        }
    }
}
