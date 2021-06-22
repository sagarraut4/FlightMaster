using FlightBooking.DataAccess.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.DataAccess.Entities
{
    [Table("Customer")]
    public class Customer : Entity
    {
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
    }
}
