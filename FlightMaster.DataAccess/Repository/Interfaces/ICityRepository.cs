using FlightMaster.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightMaster.DataAccess.Repository
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetCityDetails(int Id);
        Task<IEnumerable<City>> GetMultipleCitiesDetails(IEnumerable<int> ids);
    }
}
