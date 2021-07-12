using Core.Dto.UserDto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ToDoContext context)
            : base(context)
        {

        }

        public User GetUserByLoginAndPassword(UserAuth user, bool includeTasks = false)
        {
            var result = this.Get(x => x.Email.ToLower() == user.Email.ToLower() && user.Password == user.Password);

            if (includeTasks)
                result = result
                    .Include(x => x.Tasks.Where(x => !x.TaskGroupId.HasValue).OrderByDescending(x => x.UpdatedAt).Take(200))
                    .Include(x => x.TaskGroups)
                        .ThenInclude(x=>x.Tasks.OrderByDescending(x => x.UpdatedAt).Take(200));

            return result.FirstOrDefault();
        }

        public User GetUserByUserId(long userId, bool includeTasks = false)
        {
            var result = this.Get(x => x.Id == userId);

            if (includeTasks)
                result = result
                    .Include(x => x.Tasks.Where(x => !x.TaskGroupId.HasValue).OrderByDescending(x => x.UpdatedAt).Take(200))
                    .Include(x => x.TaskGroups)
                        .ThenInclude(x => x.Tasks.OrderByDescending(x => x.UpdatedAt).Take(200));

            return result.FirstOrDefault();
        }
    }
}
