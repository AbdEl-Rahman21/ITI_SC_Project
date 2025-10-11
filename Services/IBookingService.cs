using ITI_SC_Project.Models;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Services
{
    public interface IBookingService : IGenericService<Booking>
    {
        Task<decimal> CalculateBookingCostAsync(BookingViewModel bookingViewModel);
    }
}
