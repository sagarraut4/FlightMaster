using FlightMaster.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightMaster.DataAccess.Repository
{
    public interface IAircraftRepository : IRepository<Aircraft>
    {
        Task<Aircraft> GetAircraftDetails(int Id);
        Task<IEnumerable<Aircraft>> GetMultipleAircraftDetails(IEnumerable<int> ids);
    }
}
