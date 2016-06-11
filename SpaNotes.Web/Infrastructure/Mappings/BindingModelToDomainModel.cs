using AutoMapper;
using SpaNotes.Entities;
using SpaNotes.Web.Models;

namespace SpaNotes.Web.Infrastructure.Mappings
{
    public class BindingModelToDomainModel : Profile
    {
        public BindingModelToDomainModel()
            : base("BindingModelToDomainModel")
        { }

        protected override void Configure()
        {
            Mapper.CreateMap<NoteBindingModel, Note>();
        }
    }
}
