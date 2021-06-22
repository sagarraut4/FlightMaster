using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public interface IEventHelper
    {
        Task Publish(EventDetails eventDetails);
    }
}
