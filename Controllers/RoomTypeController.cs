using Microsoft.AspNetCore.Mvc;
using ITI_SC_Project.Models;
using ITI_SC_Project.Services;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Controllers
{
    public class RoomTypeController(IGenericService<RoomType> roomTypeService) : Controller
    {
        private readonly IGenericService<RoomType> roomTypeService = roomTypeService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await roomTypeService.GetAllAsync<RoomTypeViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BasePrice")] RoomTypeViewModel roomTypeViewModel)
        {
            if (!ModelState.IsValid) return View(roomTypeViewModel);

            var result = await roomTypeService.CreateAsync(roomTypeViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                return View(roomTypeViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("RoomType/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var roomTypeViewModel = await roomTypeService.GetSingleAsync<RoomTypeViewModel>(rt => rt.Id == id);

            if (roomTypeViewModel == null) return NotFound();

            return View(roomTypeViewModel);
        }

        [HttpPost("RoomType/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BasePrice")] RoomTypeViewModel roomTypeViewModel)
        {
            if (id != roomTypeViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(roomTypeViewModel);

            var result = await roomTypeService.UpdateAsync(roomTypeViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                return View(roomTypeViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("RoomType/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var roomTypeViewModel = await roomTypeService.GetSingleAsync<RoomTypeViewModel>(rt => rt.Id == id);

            if (roomTypeViewModel == null) return NotFound();

            return View(roomTypeViewModel);
        }

        [HttpPost("RoomType/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await roomTypeService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
