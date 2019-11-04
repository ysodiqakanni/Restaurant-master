using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.ApiHelpers;
using Web.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class RestaurantsController : Controller
    {
        RestaurantApi restaurantApi = new RestaurantApi();

        public async Task<IActionResult> Index()
        {
            var restaurants = await restaurantApi.GetAllRestaurants();
            return View(restaurants);
        }
        public IActionResult Success()
        {
            return View();
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var restaurant = await restaurantApi.GetRestaurantByrestaurantId(id.Value);
            if (restaurant == null)
                return NotFound();

            // get all areas
            var areas = await restaurantApi.GetAllAreas();
            // get all restaurant categories
            var restaurantCategories = await restaurantApi.GetAllRestaurantCategories();

            ViewBag.RestaurantCategoryId = new SelectList(restaurantCategories, "Id", "Name", restaurant.RestaurantCategoryId);
            ViewBag.AreaId = new SelectList(areas, "Id", "Name", restaurant.AreaId);
            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateRestaurantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var images = new List<ImageViewModel>();
                var files = Request.Form.Files;
                GetRestaurantImages(model, images, files); 

                // save
                await restaurantApi.UpdateRestaurant(model);
                return RedirectToAction("Success");
            }
            // get all areas
            var areas = await restaurantApi.GetAllAreas();
            // get all restaurant categories
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
                if(model.MealContents != null && model.MealContents.Any())
                {
                    foreach (var content in model.MealContents)
                    {
                        content.ImageUrl = SaveMealContentImageAndGetUri(model.MealImage);
                    }
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
        private string SaveMealContentImageAndGetUri(IFormFile formFile)
        {
            return "/path/MealContentImages/" + formFile.FileName;
        }
    }
}