using AutoMapper;
using Core.Dto.UserDto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class UserService : IUserService
    {
        IMapper mapper { get; set; }

        IUserRepository userRepo { get; set; }

        public UserService(
            IMapper mapper,
            IUserRepository userRepo
            )
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        public User GetUserByLoginAndPassword(UserAuth user, bool includeTasks = false)
        {
            return this.userRepo.GetUserByLoginAndPassword(user, includeTasks);
        }

        public async Task<User> SignOut(UserSignOut user)
        {
            return await userRepo.AddAsync(this.mapper.Map<User>(user));
        }
    }
}
