using AutoMapper;
using Core.Entities;
using Core.Models;

namespace WebApi.AutoMappers;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegister, User>().ReverseMap();
    }
}
