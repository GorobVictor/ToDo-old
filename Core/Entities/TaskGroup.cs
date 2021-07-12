using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("task_group")]
    public class TaskGroup : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("favorites")]
        public bool Favorite { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<Tasks> Tasks { get; set; }
    }
}
