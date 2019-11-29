using ABAC.DAL.Entities;
using ABAC.DAL.ViewModels;
using AutoMapper;

namespace ABAC.WebApp.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCredentials>();
            CreateMap<User, UserInfo>();
            CreateMap<Resource, ResourceInfo>();
        }
    }
}
