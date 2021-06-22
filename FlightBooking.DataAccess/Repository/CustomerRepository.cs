using FlightBooking.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightBooking.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(FlightBookingDbContext context) : base(context)
        {

        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await Query.SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
