using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Base
{
    public class BaseEntity : Entity, ICreatedEntity, IUpdatedEntity
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("created_by")]
        public long CreatedBy { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }
    }
}
