using AutoMapper;
using LiveShow.Domain.Entitis;
using LiveShow.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Infrastructure
{
    public class CustomizeProfile: Profile
    {
        public CustomizeProfile()
        {
            CreateMap<ShowRoom, ShowRoomDto>();
            CreateMap<ShowRoomDto, ShowRoom>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserDto, RegisterDto>();
            CreateMap<RegisterDto, UserDto>();
        }
    }
}
