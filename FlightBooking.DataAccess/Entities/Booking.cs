using FlightBooking.DataAccess.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.DataAccess.Entities
{
    [Table("Booking")]
    public class Booking : Entity
    {
        [Column("flight_id")]
        public int FlightId { get; set; }

        [Column("customer_id")]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Column("seats")]
        public string Seats { get; set; }

        [Column("seats_type")]
        public string seats_type { get; set; }

        [Column("fare")]
        public decimal Fare { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("pnr")]
        public string PNR { get; set; }

        [Column("Passenger_id")]
        [ForeignKey("Passenger")]
        public int PassengerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Passenger Passenger { get; set; }
    }
}
