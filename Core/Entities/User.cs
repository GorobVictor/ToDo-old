using AutoMapper;
using Core.Dto.UserDto;
using Core.Entities.Base;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get => $"{FirstName} {LastName}"; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("photo")]
        public string Photo { get; set; }

        [Column("role")]
        public UserRole Role { get; set; }

        public List<TaskGroup> TaskGroups { get; set; }

        public List<Tasks> Tasks { get; set; }
    }
}
