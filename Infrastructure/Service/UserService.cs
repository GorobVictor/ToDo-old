using AutoMapper;
using Core.Dto.UserDto;
using Core.Entities;
using Core.Enum;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<User> SignUp(UserSignUp user)
        {
            user.Email = user.Email.ToLower();

            if (userRepo.Get(x => x.Email == user.Email).Any())
            {
                throw new FriendlyException("Email busy", "email", HttpStatusCode.BadRequest, ErrorCode.SignUp);
            }

            return await userRepo.AddAsync(this.mapper.Map<User>(user));
        }
    }
}
