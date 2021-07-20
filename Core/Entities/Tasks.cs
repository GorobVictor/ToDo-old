using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("tasks")]
    public class Tasks : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("cosing_time")]
        public DateTime? ClosingTime { get; set; }

        [Column("favorites")]
        public bool Favorite { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("task_group_id")]
        public long? TaskGroupId { get; set; }

        [Column("lead_time")]
        public DateTime? LeadTime { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("TaskGroupId")]
        public TaskGroup TaskGroup { get; set; }

        public override string ToString() => this.Name;

        [NotMapped]
        public double BalanceTime
        {
            get
            {
                if (!LeadTime.HasValue)
                    return 0;

                return (this.LeadTime - DateTime.Now).Value.TotalMilliseconds;
            }
            set { }
        }

        [NotMapped]
        public double AllTime
        {
            get
            {
                if (!LeadTime.HasValue)
                    return 0;

                return (this.CreatedAt - DateTime.Now).TotalMilliseconds;
            }
            set { }
        }
    }
}
