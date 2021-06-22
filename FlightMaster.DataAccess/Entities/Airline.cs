using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    [Table("Airline")]
    public class Airline : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("address1")]
        public string Address1 { get; set; }

        [Column("address2")]
        public string Address2 { get; set; }

        [Column("address3")]
        public string Address3 { get; set; }
    }
}
