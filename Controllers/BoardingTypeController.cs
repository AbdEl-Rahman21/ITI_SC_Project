using Microsoft.AspNetCore.Mvc;
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
            if (!ModelState.IsValid) return View(boardingTypeViewModel);

            var result = await boardingTypeService.CreateAsync(boardingTypeViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                return View(boardingTypeViewModel);
            }

            return RedirectToAction(nameof(Index));
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

            if (!ModelState.IsValid) return View(boardingTypeViewModel);

            var result = await boardingTypeService.UpdateAsync(boardingTypeViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                return View(boardingTypeViewModel);
            }

            return RedirectToAction(nameof(Index));
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
            var result = await boardingTypeService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
