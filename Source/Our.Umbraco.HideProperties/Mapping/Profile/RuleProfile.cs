namespace Our.Umbraco.HideProperties.Mapping.Profile
{
    using System.Linq;

    using AutoMapper;

    public class RuleProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.Pocos.Rule, Models.Rule>()
                .ForMember(x => x.Id, opt => opt.ResolveUsing(src => src.Id))
                .ForMember(x => x.Key, opt => opt.ResolveUsing(src => src.Key))
                .ForMember(x => x.IsActive, opt => opt.ResolveUsing(src => src.IsActive))
                .ForMember(x => x.ContentTypeAlias, opt => opt.ResolveUsing(src => src.ContentTypeAlias))
                .ForMember(x => x.Tabs, opt => opt.ResolveUsing(src => !string.IsNullOrEmpty(src.Tabs) ? src.Tabs.Split(',') : Enumerable.Empty<string>()))
                .ForMember(x => x.Properties, opt => opt.ResolveUsing(src => !string.IsNullOrEmpty(src.Properties) ? src.Properties.Split(',') : Enumerable.Empty<string>()))
                .ForMember(x => x.UserGroups, opt => opt.ResolveUsing(src => !string.IsNullOrEmpty(src.UserGroups) ? src.UserGroups.Split(',') : Enumerable.Empty<string>()))
                .ForMember(x => x.IsDeleted, opt => opt.ResolveUsing(src => src.IsDeleted));

            Mapper.CreateMap<Models.Rule, Models.Pocos.Rule>()
                .ForMember(x => x.Id, opt => opt.ResolveUsing(src => src.Id))
                .ForMember(x => x.Key, opt => opt.ResolveUsing(src => src.Key))
                .ForMember(x => x.IsActive, opt => opt.ResolveUsing(src => src.IsActive))
                .ForMember(x => x.ContentTypeAlias, opt => opt.ResolveUsing(src => src.ContentTypeAlias))
                .ForMember(x => x.Tabs, opt => opt.ResolveUsing(src => src.Tabs != null && src.Tabs.Any() ? string.Join(",", src.Tabs) : string.Empty))
                .ForMember(x => x.Properties, opt => opt.ResolveUsing(src => src.Properties != null && src.Properties.Any() ? string.Join(",", src.Properties) : string.Empty))
                .ForMember(x => x.UserGroups, opt => opt.ResolveUsing(src => src.UserGroups != null && src.UserGroups.Any() ? string.Join(",", src.UserGroups) : string.Empty))
                .ForMember(x => x.IsDeleted, opt => opt.ResolveUsing(src => src.IsDeleted));
        }
    }
}
