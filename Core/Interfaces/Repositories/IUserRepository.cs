using Core.Dto.UserDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByLoginAndPassword(UserAuth user, bool includeTasks = false);
    }
}
