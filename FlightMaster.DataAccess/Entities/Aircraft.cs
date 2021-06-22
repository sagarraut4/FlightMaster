using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    [Table("Aircraft")]
    public class Aircraft : Entity
    {
        [Column("code")]
        public string Code { get; set; }

        [Column("airline_id")]
        [ForeignKey("Airline")]
        public int AirlineId { get; set; }

        [Column("carrier_id")]
        [ForeignKey("CarrierType")]
        public int CarrierId { get; set; }

        public virtual Airline Airline { get; set; }

        public virtual CarrierType CarrierType { get; set; }
    }
}
