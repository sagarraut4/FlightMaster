using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    [Table("CarrierType")]
    public class CarrierType : Entity
    {
        [Column("type")]
        public string Type { get; set; }

        [Column("range_nmi")]
        public string RangeNMI { get; set; }

        [Column("economy_seats")]
        public int EconomySeats { get; set; }

        [Column("premium_economy_seats")]
        public int BusinessSeats { get; set; }

        [Column("business_seats")]
        public int PremiumEconomySeats { get; set; }

        [Column("first_class_seats")]
        public int FirstClassSeats { get; set; }
    }
}
