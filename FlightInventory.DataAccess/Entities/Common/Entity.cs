using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightInventory.DataAccess.Entities.Common
{
    public class Entity : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
