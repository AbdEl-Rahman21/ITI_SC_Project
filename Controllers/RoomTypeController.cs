using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITI_SC_Project.Models;
using ITI_SC_Project.Services;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Controllers
{
    public class RoomTypeController(IGenericService<RoomType> genericService) : Controller
    {
        private readonly IGenericService<RoomType> genericService = genericService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await genericService.GetAllAsync<RoomTypeViewModel>());
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
            if (ModelState.IsValid)
            {
                await genericService.CreateAsync(roomTypeViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(roomTypeViewModel);
        }

        [HttpGet("RoomType/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var roomTypeViewModel = await genericService.GetByIdAsync<RoomTypeViewModel>(id);

            if (roomTypeViewModel == null) return NotFound();

            return View(roomTypeViewModel);
        }

        [HttpPost("RoomType/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BasePrice")] RoomTypeViewModel roomTypeViewModel)
        {
            if (id != roomTypeViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await genericService.UpdateAsync(roomTypeViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RoomTypeExists(roomTypeViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(roomTypeViewModel);
        }

        [HttpGet("RoomType/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var roomTypeViewModel = await genericService.GetByIdAsync<RoomTypeViewModel>(id);

            if (roomTypeViewModel == null) return NotFound();

            return View(roomTypeViewModel);
        }

        [HttpPost("RoomType/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await genericService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoomTypeExists(int id)
        {
            var roomTypeViewModel = await genericService.GetByIdAsync<RoomTypeViewModel>(id);

            return roomTypeViewModel == null;
        }
    }
}
