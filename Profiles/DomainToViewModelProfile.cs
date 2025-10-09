using AutoMapper;
using ITI_SC_Project.Models;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Profiles
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<BoardingType, BoardingTypeViewModel>().ReverseMap();

            CreateMap<Booking, BookingViewModel>()
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.Room.RoomType.Name))
                .ForMember(dest => dest.ResidentName, opt => opt.MapFrom(src => src.Resident.Name))
                .ForMember(dest => dest.BoardingTypeName, opt => opt.MapFrom(src => src.BoardingType.Name))
                .ReverseMap();

            CreateMap<Resident, ResidentViewModel>()
                .ForMember(dest => dest.InternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ResidentId))
                .ReverseMap();

            CreateMap<Room, RoomViewModel>()
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.Name))
                .ReverseMap();

            CreateMap<RoomType, RoomTypeViewModel>()
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.Price))
                .ReverseMap();
        }
    }
}
