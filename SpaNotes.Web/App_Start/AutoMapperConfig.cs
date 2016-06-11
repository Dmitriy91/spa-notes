using AutoMapper;
using SpaNotes.Web.Infrastructure.Mappings;

namespace SpaNotes.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappingProfiles()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BindingModelToDomainModel>();
                cfg.AddProfile<DomainModelToDto>();
            });

            Mapper.AssertConfigurationIsValid<BindingModelToDomainModel>();
        }
    }
}
