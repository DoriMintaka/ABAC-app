using ABAC.DAL.Entities;
using ABAC.DAL.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace ABAC.WebApp.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCredentials>();
            CreateMap<User, UserInfo>();
            CreateMap<Resource, ResourceInfo>();
            CreateMap<KeyValuePair<string, string>, Attribute>()
                .ConvertUsing(kvp => new Attribute { Name = kvp.Key, Value = kvp.Value });

            CreateMap<Attribute, KeyValuePair<string, string>>()
                .ConvertUsing(attr => new KeyValuePair<string, string>(attr.Name, attr.Value));
        }
    }
}
