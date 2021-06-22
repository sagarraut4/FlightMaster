using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    [Table("Country")]
    public class Country : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }
    }
}
