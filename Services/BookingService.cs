using AutoMapper;
using ITI_SC_Project.Models;
using ITI_SC_Project.Repositories;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Services
{
    public class BookingService(IMapper mapper, IUnitOfWork unitOfWork, IGenericService<Room> roomService, IGenericService<RoomType> roomTypeService, IGenericService<BoardingType> boardingTypeService)
        : GenericService<Booking>(mapper, unitOfWork), IBookingService
    {
        private readonly IGenericService<Room> roomService = roomService;
        private readonly IGenericService<RoomType> roomTypeService = roomTypeService;
        private readonly IGenericService<BoardingType> boardingTypeService = boardingTypeService;

        public async Task<decimal> CalculateBookingCostAsync(BookingViewModel bookingViewModel)
        {
            int nights = (bookingViewModel.CheckOutDate.DayNumber - bookingViewModel.CheckInDate.DayNumber);

            var roomViewModel = await roomService.GetByIdAsync<RoomViewModel>(bookingViewModel.RoomId);

            var roomTypeViewModel = await roomTypeService.GetByIdAsync<RoomTypeViewModel>(roomViewModel.RoomTypeId);

            decimal roomPrice = roomTypeViewModel.BasePrice;

            var boardingTypeViewModel = await boardingTypeService.GetByIdAsync<BoardingTypeViewModel>(bookingViewModel.BoardingTypeId);

            decimal boardingTypeModifier = boardingTypeViewModel.PriceModifier / 100;

            return Math.Round(nights * roomPrice * (1 + boardingTypeModifier), 2);
        }
    }
}
