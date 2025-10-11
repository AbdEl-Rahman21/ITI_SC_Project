using ITI_SC_Project.Helpers;
using ITI_SC_Project.Models;
using ITI_SC_Project.Services;
using ITI_SC_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_SC_Project.Controllers
{
    public class BookingController(IBookingService bookingService, IGenericService<Room> roomService, IGenericService<BoardingType> boardingTypeService) : Controller
    {
        private readonly IBookingService bookingService = bookingService;
        private readonly IGenericService<Room> roomService = roomService;
        private readonly IGenericService<BoardingType> boardingTypeService = boardingTypeService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await bookingService.GetAllAsync<BookingViewModel>());
        }

        [HttpGet]
        public IActionResult SelectDates(int residentId, string residentName, string residentCode)
        {
            var selectDatesViewModel = new SelectDatesViewModel
            {
                ResidentId = residentId,
                ResidentName = residentName,
                ResidentCode = residentCode,
                CheckIn = DateOnly.FromDateTime(DateTime.Today),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
            };

            return View(selectDatesViewModel);
        }

        [HttpPost]
        public IActionResult SelectDates(SelectDatesViewModel selectDatesViewModel)
        {
            if (!ModelState.IsValid) return View(selectDatesViewModel);

            return RedirectToAction(nameof(Create), new
            {
                selectDatesViewModel.ResidentId,
                selectDatesViewModel.ResidentName,
                selectDatesViewModel.ResidentCode,
                CheckInDate = selectDatesViewModel.CheckIn.ToString("yyyy-MM-dd"),
                CheckOutDate = selectDatesViewModel.CheckOut.ToString("yyyy-MM-dd")
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create(BookingViewModel bookingViewModel)
        {
            var options = new QueryOptions<Room>
            {
                Includes = { r => r.Bookings },
                Filter = r => !r.Bookings.Any(b => (bookingViewModel.CheckInDate < b.CheckOutDate) && (bookingViewModel.CheckOutDate > b.CheckInDate))
            };

            ViewBag.rooms = new SelectList(await roomService.GetAllAsync<RoomViewModel>(options), "Id", "RoomNumber");

            ViewBag.boardingtypes = new SelectList(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking([Bind("Id,CheckInDate,CheckOutDate,RoomId,ResidentId,BoardingTypeId,RoomNumber,ResidentCode,ResidentName,BoardingTypeName")] BookingViewModel bookingViewModel)
        {
            if (!ModelState.IsValid)
            {
                var options = new QueryOptions<Room>
                {
                    Includes = { r => r.Bookings },
                    Filter = r => !r.Bookings.Any(b => (bookingViewModel.CheckInDate < b.CheckOutDate) && (bookingViewModel.CheckOutDate > b.CheckInDate))
                };

                ViewBag.rooms = new SelectList(await roomService.GetAllAsync<RoomViewModel>(options), "Id", "RoomNumber");

                ViewBag.boardingtypes = new SelectList(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>(), "Id", "Name");

                return View(bookingViewModel);
            }

            bookingViewModel.TotalCost = await bookingService.CalculateBookingCostAsync(bookingViewModel);

            var result = await bookingService.CreateAsync(bookingViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                var options = new QueryOptions<Room>
                {
                    Includes = { r => r.Bookings },
                    Filter = r => !r.Bookings.Any(b => (bookingViewModel.CheckInDate < b.CheckOutDate) && (bookingViewModel.CheckOutDate > b.CheckInDate))
                };

                ViewBag.rooms = new SelectList(await roomService.GetAllAsync<RoomViewModel>(options), "Id", "RoomNumber");

                ViewBag.boardingtypes = new SelectList(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>(), "Id", "Name");

                return View(bookingViewModel);
            }

            TempData["Success"] = $"Booking Cost: {bookingViewModel.TotalCost}";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Booking/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bookingViewModel = await bookingService.GetSingleAsync<BookingViewModel>(b => b.Id == id);

            if (bookingViewModel == null) return NotFound();

            var options = new QueryOptions<Room>
            {
                Includes = { r => r.Bookings },
                Filter = r => !r.Bookings.Any(b => (bookingViewModel.CheckInDate < b.CheckOutDate) && (bookingViewModel.CheckOutDate > b.CheckInDate))
            };

            ViewBag.rooms = new SelectList(await roomService.GetAllAsync<RoomViewModel>(options), "Id", "RoomNumber");

            ViewBag.boardingtypes = new SelectList(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>(), "Id", "Name");

            return View(bookingViewModel);
        }

        [HttpPost("Booking/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckInDate,CheckOutDate,RoomId,ResidentId,BoardingTypeId,RoomNumber,ResidentCode,ResidentName,BoardingTypeName")] BookingViewModel bookingViewModel)
        {
            if (id != bookingViewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                var options = new QueryOptions<Room>
                {
                    Includes = { r => r.Bookings },
                    Filter = r => !r.Bookings.Any(b => (bookingViewModel.CheckInDate < b.CheckOutDate) && (bookingViewModel.CheckOutDate > b.CheckInDate))
                };

                ViewBag.rooms = new SelectList(await roomService.GetAllAsync<RoomViewModel>(options), "Id", "RoomNumber");

                ViewBag.boardingtypes = new SelectList(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>(), "Id", "Name");

                return View(bookingViewModel);
            }

            bookingViewModel.TotalCost = await bookingService.CalculateBookingCostAsync(bookingViewModel);

            var result = await bookingService.UpdateAsync(bookingViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                var options = new QueryOptions<Room>
                {
                    Includes = { r => r.Bookings },
                    Filter = r => !r.Bookings.Any(b => (bookingViewModel.CheckInDate < b.CheckOutDate) && (bookingViewModel.CheckOutDate > b.CheckInDate))
                };

                ViewBag.rooms = new SelectList(await roomService.GetAllAsync<RoomViewModel>(options), "Id", "RoomNumber");

                ViewBag.boardingtypes = new SelectList(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>(), "Id", "Name");

                return View(bookingViewModel);
            }

            TempData["Success"] = $"Booking Cost: {bookingViewModel.TotalCost}";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Booking/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bookingViewModel = await bookingService.GetSingleAsync<BookingViewModel>(b => b.Id == id);

            if (bookingViewModel == null) return NotFound();

            return View(bookingViewModel);
        }

        [HttpPost("Booking/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await bookingService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
