using FlightMaster.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMaster.DataAccess.Repository
{
    public class AircraftRepository : Repository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(FlightMasterDbContext context) : base(context)
        {

        }

        public async Task<Aircraft> GetAircraftDetails(int Id)
        {
            return await Query.Include(ar => ar.Airline).Include(c => c.CarrierType).SingleOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<IEnumerable<Aircraft>> GetMultipleAircraftDetails(IEnumerable<int> ids)
        {
            return await Query.Include(ar => ar.Airline).Include(c => c.CarrierType).Where(e => ids.Contains(e.Id)).ToListAsync();
        }
    }
}
