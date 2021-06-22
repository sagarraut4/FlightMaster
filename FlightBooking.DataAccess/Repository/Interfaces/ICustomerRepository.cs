using FlightBooking.DataAccess.Entities;
using System.Threading.Tasks;

namespace FlightBooking.DataAccess.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomer(int id);
    }
}
