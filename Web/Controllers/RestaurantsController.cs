using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.ApiHelpers;
using Web.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class RestaurantsController : Controller
    {
        RestaurantApi restaurantApi = new RestaurantApi();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var restaurant = await restaurantApi.GetAllRestaurants();
            return View(restaurant.ToList());
        }

        public async Task<IActionResult> Create()
        {
            // get all areas
            var areas = await restaurantApi.GetAllAreas();
            // get all restaurant categories
            var restaurantCategories = await restaurantApi.GetAllRestaurantCategories();

            ViewBag.RestaurantCategoryId = restaurantCategories;
            ViewBag.AreaId = areas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRestaurantViewModel model)
        {
            if (ModelState.IsValid)
            {
                // get the images
                // or use IFormFiles instead of HtppPostedFileBase
                var images = new List<ImageViewModel>();
                var files = Request.Form.Files;
                GetRestaurantImages(model, images, files);

                // get the working hours
                var workingHours = new List<WorkingHourViewModel>();
                GetWorkingHours(model, workingHours);
                model.WorkingHours = workingHours;

                // save
                await restaurantApi.CreateRestaurant(model);
                return RedirectToAction("Success");
            }

            var areas = await restaurantApi.GetAllAreas();
            var restaurantCategories = await restaurantApi.GetAllRestaurantCategories();

            ViewBag.RestaurantCategoryId = restaurantCategories;
            ViewBag.AreaId = areas;
            return View(model);
        }



        public async Task<IActionResult> AddMeal()
        {
            var allRestaurants = await restaurantApi.GetAllRestaurants();
            var mealCategories = await restaurantApi.GetAllMealCategories();
            ViewBag.RestaurantId = allRestaurants;
            ViewBag.MealCategoryId = mealCategories;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(AddMealViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    string uri = SaveMealImageAndGetUri(file);
                    model.ImageUrl = uri;
                }

                await restaurantApi.AddnewMeal(model);
                return RedirectToAction("Success");
            }
            var allRestaurants = await restaurantApi.GetAllRestaurants();
            var mealCategories = await restaurantApi.GetAllMealCategories();

            ViewBag.RestaurantId = allRestaurants;
            ViewBag.MealCategoryId = mealCategories;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurantCategories()
        {
            var restaurantCategories = await restaurantApi.GetAllRestaurantCategories();
            return View(restaurantCategories);
        }

        public async Task<IActionResult> AddRestaurantCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRestaurantCategory(AddRestaurantCategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // save
                    await restaurantApi.CreateRestaurantCategories(model);
                    return RedirectToAction("GetAllRestaurantCategories");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Failed to Edit a Area";
                    return RedirectToAction("AddRestaurantCategory");
                }

            }
            return View(model);
        }

        public async Task<IActionResult> EditRestaurantCategory([FromRoute] int id = 0)
        {
            if (id == 0)
                return BadRequest("Id is null!");
            var category = await restaurantApi.GetRestaurantCaregoryById(id);

            if (category == null)
                return NotFound();

            EditRestaurantCategoryViewModel editAreaViewModel = new EditRestaurantCategoryViewModel()
            {
                Name = category.Name,
            };

            return View(editAreaViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> EditRestaurantCategory(EditRestaurantCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = await restaurantApi.GetRestaurantCaregoryById(model.Id);
                    if (category == null)
                        return NotFound();

                    category.Name = model.Name;
                    await restaurantApi.UpdateRestaurantCategory(category);
                    return RedirectToAction("GetAllRestaurantCategories");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Failed to Edit a Area";
                    return RedirectToAction("EditRestaurantCategory", new { id = model.Id });
                }
            }
            return View(model);
        }

        public async Task<IActionResult> GetMealTypesForRestaurant(int restaurantId)
        {
            if (restaurantId < 1)
                return Json(new { success = false });
            var mealTypes = await restaurantApi.GetMealTypesForRestaurant(restaurantId);
            return Json(new { success = true, msg = mealTypes });
        }


        public IActionResult Categories()
        {
            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }


















        private void GetRestaurantImages(CreateRestaurantViewModel model, List<ImageViewModel> images, Microsoft.AspNetCore.Http.IFormFileCollection files)
        {
            if (files != null && files.Any())
            {
                if (model.Images == null || !model.Images.Any() || model.Images.Count != files.Count)
                    return;
                for (int i = 0; i < files.Count; i++)
                {
                    // save file and get the uri
                    string uri = SaveRestaurantImageAndGetUri(files[i]);
                    var img = model.Images[i];
                    img.ImageUrl = uri;
                    images.Add(img);
                } 
            }
        }

        private string SaveRestaurantImageAndGetUri(IFormFile formFile)
        {
            return "/path/RestaurantImages/" + formFile.FileName;
        }
        private string SaveMealImageAndGetUri(IFormFile formFile)
        {
            return "/path/MealImages/" + formFile.FileName;
        }

        //private string SaveImageAndGetUri()
        //{
        //    return "/path/dummyuri";
        //}

        private static void GetWorkingHours(CreateRestaurantViewModel model, List<WorkingHourViewModel> workingHours)
        {
            if (!String.IsNullOrEmpty(model.MondayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Monday",
                    FromTime = $"{model.MondayFromHour}:{(String.IsNullOrEmpty(model.MondayFromMinute) ? "00" : model.MondayFromMinute)}",
                    ToTime = $"{model.MondayToHour}:{(String.IsNullOrEmpty(model.MondayToMinute) ? "00" : model.MondayToMinute)}"
                };
                workingHours.Add(wHour);
            }
            if (!String.IsNullOrEmpty(model.TuesdayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Tuesday",
                    FromTime = $"{model.TuesdayFromHour}:{(String.IsNullOrEmpty(model.TuesdayFromMinute) ? "00" : model.TuesdayFromMinute)}",
                    ToTime = $"{model.TuesdayToHour}:{(String.IsNullOrEmpty(model.TuesdayToMinute) ? "00" : model.TuesdayToMinute)}"
                };
                workingHours.Add(wHour);
            }

            if (!String.IsNullOrEmpty(model.WednesdayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Wednesday",
                    FromTime = $"{model.WednesdayFromHour}:{(String.IsNullOrEmpty(model.WednesdayFromMinute) ? "00" : model.WednesdayFromMinute)}",
                    ToTime = $"{model.WednesdayToHour}:{(String.IsNullOrEmpty(model.WednesdayToMinute) ? "00" : model.WednesdayToMinute)}"
                };
                workingHours.Add(wHour);
            }

            if (!String.IsNullOrEmpty(model.ThursdayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Thursday",
                    FromTime = $"{model.ThursdayFromHour}:{(String.IsNullOrEmpty(model.ThursdayFromMinute) ? "00" : model.ThursdayFromMinute)}",
                    ToTime = $"{model.ThursdayToHour}:{(String.IsNullOrEmpty(model.ThursdayToMinute) ? "00" : model.ThursdayToMinute)}"
                };
                workingHours.Add(wHour);
            }

            if (!String.IsNullOrEmpty(model.FridayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Friday",
                    FromTime = $"{model.FridayFromHour}:{(String.IsNullOrEmpty(model.FridayFromMinute) ? "00" : model.FridayFromMinute)}",
                    ToTime = $"{model.FridayToHour}:{(String.IsNullOrEmpty(model.FridayToMinute) ? "00" : model.FridayToMinute)}"
                };
                workingHours.Add(wHour);
            }

            if (!String.IsNullOrEmpty(model.SaturdayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Saturday",
                    FromTime = $"{model.SaturdayFromHour}:{(String.IsNullOrEmpty(model.SaturdayFromMinute) ? "00" : model.SaturdayFromMinute)}",
                    ToTime = $"{model.SaturdayToHour}:{(String.IsNullOrEmpty(model.SaturdayToMinute) ? "00" : model.SaturdayToMinute)}"
                };
                workingHours.Add(wHour);
            }

            if (!String.IsNullOrEmpty(model.SaturdayFromHour))
            {
                var wHour = new WorkingHourViewModel
                {
                    Day = "Saturday",
                    FromTime = $"{model.SaturdayFromHour}:{(String.IsNullOrEmpty(model.SaturdayFromMinute) ? "00" : model.SaturdayFromMinute)}",
                    ToTime = $"{model.SaturdayToHour}:{(String.IsNullOrEmpty(model.SaturdayToMinute) ? "00" : model.SaturdayToMinute)}"
                };
                workingHours.Add(wHour);
            }
        }
    }
}