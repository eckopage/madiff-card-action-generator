using AutoMapper;
using CardService.Application.Models;
using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardService.Api.Mappings
{
    public class CardMappingProfile : Profile
    {
        public CardMappingProfile()
        {
            CreateMap<CardDetails, CardDetailsDto>()
                .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType.ToString()))
                .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.CardStatus.ToString()))
                .ForMember(dest => dest.IsPinSet, opt => opt.MapFrom(src => src.IsPinSet));
        }
    }
}