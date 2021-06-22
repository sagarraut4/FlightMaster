using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMaster.DataAccess.Entities
{
    public class AuditEntity : Entity
    {
        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_timestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [Column("modified_by")]
        public int ModifiedBy { get; set; }

        [Column("modified_timestamp")]
        public DateTime ModifiedTimestamp { get; set; }
    }
}
