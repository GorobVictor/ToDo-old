using Core.Dto.UserDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Service
{
    public interface IUserService
    {
        User GetUserByLoginAndPassword(UserAuth user, bool includeTasks = false);

        Task<User> SignUp(UserSignUp user);
    }
}
