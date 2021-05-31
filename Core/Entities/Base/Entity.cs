using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Base
{
    public class Entity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
