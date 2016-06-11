using AutoMapper;
using SpaNotes.Entities;
using SpaNotes.Web.Models;

namespace SpaNotes.Web.Infrastructure.Mappings
{
    public class DomainModelToDto : Profile
    {
        public DomainModelToDto()
            : base("DomainModelToDto")
        { }

        protected override void Configure()
        {
            Mapper.CreateMap<Note, NoteDto>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForSourceMember(s => s.UserId, opts => opts.Ignore());
        }
    }
}
