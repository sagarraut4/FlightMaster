using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    [Table("Airport")]
    public class Airport : Entity
    {
        [Column("city_id")]
        public int CityId { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("type")]
        public string Type { get; set; }
    }
}
