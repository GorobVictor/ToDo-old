using AutoMapper;
using Core.Dto.Tasks;
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
            this.CreateMap<UserSignOut, User>()
                .ForMember(x => x.Email, x => x.MapFrom(y => y.Email.ToLower()))
                .ForMember(x => x.Role, x => x.MapFrom(y => UserRole.User));

            this.CreateMap<CreateTaskDto, Tasks>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => myAuthorizationSvc.UserIdAuthenticated))
                .ForMember(x => x.Status, x => x.MapFrom(y => false))
                .ForMember(x => x.ClosingTime, x => x.Ignore());
        }
    }
}
