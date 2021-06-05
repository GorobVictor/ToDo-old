using AutoMapper;
using Core.Dto.TasksDto;
using Core.Dto.UserDto;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile(IMyAuthorizationServiceSingelton myAuthorizationSvc)
        {
            this.CreateMap<CreateTaskDto, Tasks>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => myAuthorizationSvc.UserIdAuthenticated))
                .ForMember(x => x.Status, x => x.MapFrom(y => false))
                .ForMember(x => x.ClosingTime, x => x.Ignore());

            this.CreateMap<UserSignUp, User>();
        }
    }
}
