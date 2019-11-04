﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiHelpers;
using Web.Models;

namespace Web.Controllers
{
    public class MealsController : Controller
    {
        MealApi mealApi = new MealApi();
        RestaurantApi restaurantApi = new RestaurantApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var meals = await mealApi.GetAllMeals();
            return View(meals.ToList());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var meal = await mealApi.GetMealById(id.Value);

            var allRestaurants = await restaurantApi.GetAllRestaurants();
            var mealCategories = await restaurantApi.GetAllMealCategories();
            ViewBag.RestaurantId = new SelectList(allRestaurants, "Id", "Name", meal.RestaurantId);
            // ViewBag.RestaurantId = allRestaurants;
            ViewBag.MealCategoryId = new SelectList(mealCategories, "Id", "Name", meal.MealCategoryId); // mealCategories;

            return View(meal); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddMealViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Any())
                    {
                        var file = model.MealImage;
                        if (file != null && file.Length > 0)
                        {
                            string uri = SaveMealImageAndGetUri(file);
                            model.ImageUrl = uri;
                        }
                    }
                    // meal contents images
                    if (model.MealContents != null && model.MealContents.Any())
                    {
                        foreach (var content in model.MealContents)
                        {
                            content.ImageUrl = SaveMealContentImageAndGetUri(model.MealImage);
                        }
                    }

                    await mealApi.UpdateMeal(model);
                    return RedirectToAction("Success");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occured during update!");
                }
            }
            var allRestaurants = await restaurantApi.GetAllRestaurants();
            var mealCategories = await restaurantApi.GetAllMealCategories();

            ViewBag.RestaurantId = allRestaurants;
            ViewBag.MealCategoryId = mealCategories;
            return View(model);
        }
        public IActionResult AddMealType()
        {
            return View(); 
        }

        public async Task<IActionResult> Categories()
        {
            var model = await restaurantApi.GetAllMealCategories();// new List<MealCategoryViewModel>();
            return View(model);
        }

        public IActionResult AddMealCategory()
        {

            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMealCategory(CreateMealCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await mealApi.AddMealCategory(model);
                }
            }
            catch (Exception ex)
            {
                // Log error here
                ModelState.AddModelError("", "An unknown error occured!");
            }
            
            return View(model);
        }

        public async Task<IActionResult> EditMealCategory(int? id)
        {
            if (id == null)
                return BadRequest();
            var mealCategory = await mealApi.GetMealCategoryById(id.Value);
            if (mealCategory == null)
                return NotFound();
            var model = new CreateMealCategoryViewModel
            {
                Id = mealCategory.Id,
                Name = mealCategory.Name,
                Priority = mealCategory.Priority
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMealCategory(CreateMealCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await mealApi.AddMealCategory(model);
                }
            }
            catch (Exception ex)
            {
                // Log error here
                ModelState.AddModelError("", "An unknown error occured!");
            }

            return View(model);
        }


        private string SaveMealImageAndGetUri(IFormFile formFile)
        {
            return "/path/MealImages/" + formFile.FileName;
        }
        private string SaveMealContentImageAndGetUri(IFormFile formFile)
        {
            return "/path/MealContentImages/" + formFile.FileName;
        }

    }
}