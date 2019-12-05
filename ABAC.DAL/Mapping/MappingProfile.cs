using ABAC.DAL.Entities;
using ABAC.DAL.RuleParser;
using ABAC.DAL.ViewModels;
using AutoMapper;
using Newtonsoft.Json;

namespace ABAC.DAL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCredentials>();
            CreateMap<User, UserInfo>();
            CreateMap<Resource, ResourceInfo>();
            CreateMap<Rule, RuleInfo>().ForMember(r => r.Value, options => options.MapFrom(r => JsonConvert.DeserializeObject<RuleNode>(r.Value)));
            CreateMap<RuleInfo, Rule>().ForMember(r => r.Value, options => options.MapFrom(r => JsonConvert.SerializeObject(r.Value)));
        }
    }
}
