using AutoMapper;
using GuessWho.Execution.Dtos;
using GuessWho.Execution.Dtos.Player;
using GuessWho.Models;
using System;
using System.Linq;

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

            CreateMap<PlayerEntity, PlayerDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.PartitionKey))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CreatePlayerDto, PlayerEntity>()
                .ForMember(dest => dest.PartitionKey, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.RowKey, src => src.MapFrom(x => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<AddFriendDto, PlayerRelationEntity>()
                .ForMember(dest => dest.PartitionKey, src => src.MapFrom(x => x.PlayerId))
                .ForMember(dest => dest.RowKey, src => src.MapFrom(x => x.FriendId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<RemoveFriendDto, PlayerRelationEntity>()
                .ForMember(dest => dest.PartitionKey, src => src.MapFrom(x => x.PlayerId))
                .ForMember(dest => dest.RowKey, src => src.MapFrom(x => x.FriendId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
