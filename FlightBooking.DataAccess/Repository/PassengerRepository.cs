using FlightBooking.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightBooking.DataAccess.Repository
{
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(FlightBookingDbContext context) : base(context)
        {

        }

        public async Task<Passenger> GetPassenger(int id)
        {
            return await Query.SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
