using AutoMapper;
using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.UserDto
{
    [AutoMap(typeof(User))]
    public class UserSignOut
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
    }
}
