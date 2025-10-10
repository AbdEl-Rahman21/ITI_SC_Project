using ITI_SC_Project.Helpers;
using ITI_SC_Project.Models;
using ITI_SC_Project.Services;
using ITI_SC_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ITI_SC_Project.Controllers
{
    public class RoomController(IGenericService<Room> roomService, IGenericService<RoomType> roomTypeService) : Controller
    {
        private readonly IGenericService<Room> roomService = roomService;
        private readonly IGenericService<RoomType> roomTypeService = roomTypeService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var options = new QueryOptions<Room>
            {
                Includes = { r => r.RoomType }
            };

            return View(await roomService.GetAllAsync<RoomViewModel>(options));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.roomtypes = new SelectList(await roomTypeService.GetAllAsync<RoomTypeViewModel>(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomNumber,RoomTypeId")] RoomViewModel roomViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.roomtypes = new SelectList(await roomTypeService.GetAllAsync<RoomTypeViewModel>(), "Id", "Name");

                return View(roomViewModel);
            }

            var result = await roomService.CreateAsync(roomViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                ViewBag.roomtypes = new SelectList(await roomTypeService.GetAllAsync<RoomTypeViewModel>(), "Id", "Name");

                return View(roomViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Room/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var roomViewModel = await roomService.GetByIdAsync<RoomViewModel>(id);

            if (roomViewModel == null) return NotFound();

            ViewBag.roomtypes = new SelectList(await roomTypeService.GetAllAsync<RoomTypeViewModel>(), "Id", "Name");

            return View(roomViewModel);
        }

        [HttpPost("Room/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomNumber,RoomTypeId")] RoomViewModel roomViewModel)
        {
            if (id != roomViewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.roomtypes = new SelectList(await roomTypeService.GetAllAsync<RoomTypeViewModel>(), "Id", "Name");

                return View(roomViewModel);
            }

            var result = await roomService.UpdateAsync(roomViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                ViewBag.roomtypes = new SelectList(await roomTypeService.GetAllAsync<RoomTypeViewModel>(), "Id", "Name");

                return View(roomViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Room/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var roomViewModel = await roomService.GetByIdAsync<RoomViewModel>(id);

            if (roomViewModel == null) return NotFound();

            return View(roomViewModel);
        }

        [HttpPost("Room/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await roomService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
