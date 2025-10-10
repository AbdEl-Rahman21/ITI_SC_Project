using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITI_SC_Project.Models;
using ITI_SC_Project.Services;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Controllers
{
    public class BoardingTypeController(IGenericService<BoardingType> boardingTypeService) : Controller
    {
        private readonly IGenericService<BoardingType> boardingTypeService = boardingTypeService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await boardingTypeService.GetAllAsync<BoardingTypeViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PriceModifier")] BoardingTypeViewModel boardingTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                await boardingTypeService.CreateAsync(boardingTypeViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(boardingTypeViewModel);
        }

        [HttpGet("BoardingType/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var boardingTypeViewModel = await boardingTypeService.GetByIdAsync<BoardingTypeViewModel>(id);

            if (boardingTypeViewModel == null) return NotFound();

            return View(boardingTypeViewModel);
        }

        [HttpPost("BoardingType/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PriceModifier")] BoardingTypeViewModel boardingTypeViewModel)
        {
            if (id != boardingTypeViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await boardingTypeService.UpdateAsync(boardingTypeViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BoardingTypeExists(boardingTypeViewModel.Id))
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

            return View(boardingTypeViewModel);
        }

        [HttpGet("BoardingType/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var boardingTypeViewModel = await boardingTypeService.GetByIdAsync<BoardingTypeViewModel>(id);

            if (boardingTypeViewModel == null) return NotFound();

            return View(boardingTypeViewModel);
        }

        [HttpPost("BoardingType/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await boardingTypeService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BoardingTypeExists(int id)
        {
            var boardingTypeViewModel = await boardingTypeService.GetByIdAsync<BoardingTypeViewModel>(id);

            return boardingTypeViewModel == null;
        }
    }
}
