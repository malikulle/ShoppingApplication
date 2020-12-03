using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingApp.Entities.Dtos;
using ShoppingApp.Entities.Models;

namespace ShoppingApp.WebMVC.AutoMapper.Profiles
{
    public class UserProfile  :Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserAddDto>();

            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}
