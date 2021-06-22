using FlightBooking.DataAccess.Entities;
using System.Threading.Tasks;

namespace FlightBooking.DataAccess.Repository
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Task<Passenger> GetPassenger(int id);
    }
}
