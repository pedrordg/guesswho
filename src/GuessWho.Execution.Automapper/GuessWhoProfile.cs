using AutoMapper;
using GuessWho.Execution.Dtos;
using GuessWho.Models;

namespace GuessWho.Execution.Automapper
{
    public class GuessWhoProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationProfile" /> class.
        /// </summary>
        public GuessWhoProfile()
        {
            CreateMap<IdolEntity, IdolDto>()

                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.RowKey))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.ThemeName, src => src.MapFrom(x => x.ThemeName))
                .ForMember(dest => dest.ThemeId, src => src.MapFrom(x => x.PartitionKey))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
