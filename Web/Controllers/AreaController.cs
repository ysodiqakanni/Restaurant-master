using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.ApiHelpers;
using Web.Models;

namespace Web.Controllers
{
    public class AreaController : Controller
    {
        AreaApi areaApi = new AreaApi();


        public IActionResult Success()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var areas = await areaApi.GetAllAreas();
            return View(areas.ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // save
                await areaApi.CreateArea(model);
                return RedirectToAction("Success");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit([FromRoute] int id = 0)
        {
            if (id == 0)
                return BadRequest("Id is null!");
            var area = await areaApi.GetAreaById(id);

            if (area == null)
                return NotFound();

            EditAreaViewModel editAreaViewModel = new EditAreaViewModel()
            {
                Name = area.Name,
            };

            ViewBag.Error = TempData["AreaEditError"];
            return View(editAreaViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var area = await areaApi.GetAreaById(model.Id);
                    if (area == null)
                        return NotFound();

                    area.Name = model.Name;
                    await areaApi.UpdateArea(area);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    TempData["AreaEditError"] = "Failed to Edit a Area";
                    return RedirectToAction("Edit", new { id = model.Id });
                }
            }
            return View(model);
        }
    }

}
