using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Dto.UserDto
{
    public class GetTokenResult
    {
        public GetTokenResult()
        {
        }

        public GetTokenResult(string token, User user)
        {
            this.Token = token;
            this.User = user;
        }

        public string Token { get; set; }

        public User User { get; set; }
    }
}
