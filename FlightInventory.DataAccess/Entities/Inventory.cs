using FlightInventory.DataAccess.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightInventory.DataAccess.Entities
{
    [Table("Inventory")]
    public class Inventory : Entity
    {
        [Column("flight_id")]
        public int FlightId { get; set; }

        [Column("economy_seats")]
        public int EconomySeats { get; set; }

        [Column("premium_economy_seats")]
        public int PremiumEconomySeats { get; set; }

        [Column("business_seats")]
        public int BusinessSeats { get; set; }

        [Column("first_class_seats")]
        public int FirstClassSeats { get; set; }
    }
}
