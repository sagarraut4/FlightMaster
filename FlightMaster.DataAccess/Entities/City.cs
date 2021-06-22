using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    [Table("City")]
    public class City : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("country_id")]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [Column("timezone")]
        public DateTimeOffset? TimeZone { get; set; }

        public virtual Country Country { get; set; }
    }
}
