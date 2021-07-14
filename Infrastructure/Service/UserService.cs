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
        ITaskRepository taskRepo { get; set; }
        ITaskGroupRepository taskGroupRepo { get; set; }

        public UserService(
            IMapper mapper,
            IUserRepository userRepo,
            ITaskRepository taskRepo,
            ITaskGroupRepository taskGroupRepo
            )
        {
            this.userRepo = userRepo;
            this.taskRepo = taskRepo;
            this.taskGroupRepo = taskGroupRepo;
            this.mapper = mapper;
        }

        public User GetUserByLoginAndPassword(UserAuth user, bool includeTasks = false)
        {
            return this.userRepo.GetUserByLoginAndPassword(user, includeTasks);
        }

        public User GetUserByUserId(long userId, bool includeTasks = false)
        {
            return this.userRepo.GetUserByUserId(userId, includeTasks);
        }

        public async Task<User> SignUp(UserSignUp user)
        {
            user.Email = user.Email.ToLower();

            if (userRepo.Get(x => x.Email == user.Email).Any())
            {
                throw new FriendlyException("Email busy", "email", HttpStatusCode.BadRequest, ErrorCode.SignUp);
            }

            var response = await userRepo.AddAsync(this.mapper.Map<User>(user));

            await taskGroupRepo.AddAsync(new TaskGroup("ToDo", true, response.Id));
            await taskGroupRepo.AddAsync(new TaskGroup("All", true, response.Id));

            return response;
        }
    }
}
