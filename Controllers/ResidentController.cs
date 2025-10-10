using Microsoft.AspNetCore.Mvc;
using ITI_SC_Project.Models;
using ITI_SC_Project.Services;
using ITI_SC_Project.ViewModels;

namespace ITI_SC_Project.Controllers
{
    public class ResidentController(IGenericService<Resident> residentService) : Controller
    {
        private readonly IGenericService<Resident> residentService = residentService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await residentService.GetAllAsync<ResidentViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResidentId,Name,Email,Phone")] ResidentViewModel residentViewModel)
        {
            if (!ModelState.IsValid) return View(residentViewModel);

            var result = await residentService.CreateAsync(residentViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                return View(residentViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Resident/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var residentViewModel = await residentService.GetByIdAsync<ResidentViewModel>(id);

            if (residentViewModel == null) return NotFound();

            return View(residentViewModel);
        }

        [HttpPost("Resident/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResidentId,Name,Email,Phone")] ResidentViewModel residentViewModel)
        {
            if (id != residentViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(residentViewModel);

            var result = await residentService.UpdateAsync(residentViewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);

                return View(residentViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Resident/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var residentViewModel = await residentService.GetByIdAsync<ResidentViewModel>(id);

            if (residentViewModel == null) return NotFound();

            return View(residentViewModel);
        }

        [HttpPost("Resident/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await residentService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
