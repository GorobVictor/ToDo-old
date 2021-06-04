using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.UserDto
{
    [AutoMap(typeof(User))]
    public class UserAuth
    {
        public UserAuth(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
