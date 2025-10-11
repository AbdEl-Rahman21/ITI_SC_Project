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
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.Room.RoomType.Name))
                .ForMember(dest => dest.ResidentCode, opt => opt.MapFrom(src => src.Resident.ResidentId))
                .ForMember(dest => dest.ResidentName, opt => opt.MapFrom(src => src.Resident.Name))
                .ForMember(dest => dest.BoardingTypeName, opt => opt.MapFrom(src => src.BoardingType.Name))
                .ReverseMap()
                .ForMember(dest => dest.Room, opt => opt.Ignore())
                .ForMember(dest => dest.Resident, opt => opt.Ignore())
                .ForMember(dest => dest.BoardingType, opt => opt.Ignore());

            CreateMap<Resident, ResidentViewModel>().ReverseMap();

            CreateMap<Room, RoomViewModel>()
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.Name))
                .ReverseMap()
                .ForMember(dest => dest.RoomType, opt => opt.Ignore());

            CreateMap<RoomType, RoomTypeViewModel>()
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.Price))
                .ReverseMap();
        }
    }
}
