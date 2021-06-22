using FlightBooking.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.DataAccess.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(FlightBookingDbContext context) : base(context)
        {

        }
        public async Task<Booking> GetBookingDetailsById(int id)
        {
            return await Query.Include(pr => pr.Passenger).Include(cr => cr.Customer).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<string> GetLastPNRNO()
        {
            var booking = await Query.Include(pr => pr.Passenger).Include(cr => cr.Customer).ToListAsync();
            if (booking != null && booking.Count > 0)
            {
                int pnrNo = Int32.Parse(booking.LastOrDefault().PNR);
                return (pnrNo + 1).ToString().PadLeft(6, '0');
            }
            else
            {
                return "000001";
            }
        }

        public async Task<Booking> GetBookingDetailsByPNR(string pnr)
        {
            return await Query.Include(pr => pr.Passenger).Include(cr => cr.Customer).SingleOrDefaultAsync(e => e.PNR == pnr);
        }

        public async Task<bool> BookFlight(Booking booking)
        {
            try
            {
                var pnr = await GetLastPNRNO();
                booking.PNR = pnr;
                await InsertAsync(booking);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
