using FlightBooking.DataAccess.Entities;
using System.Threading.Tasks;

namespace FlightBooking.DataAccess.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetBookingDetailsById(int id);
        Task<Booking> GetBookingDetailsByPNR(string pnr);
        Task<bool> BookFlight(Booking booking);
    }
}
