using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.DataAccess.Entities.Common
{
    public class Entity : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
