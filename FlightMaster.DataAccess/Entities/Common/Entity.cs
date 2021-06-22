using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    public class Entity : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
