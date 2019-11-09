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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web.Controllers
{
    public class RestaurantsController : Controller
    {
        RestaurantApi restaurantApi = new RestaurantApi();
        private readonly IHostingEnvironment hostingEnvironment;
        public RestaurantsController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0)
                return BadRequest("Id is null!");

            var restaurant = await restaurantApi.GetRestaurantById(id.Value);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                await restaurantApi.DeleteRestaurant(id.Value);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "An Unknown Error ocurred");
            }
            var restaurant = await restaurantApi.GetRestaurantById(id.Value);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
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
                        string uri = ImageSave(file, "mealImage");
                        model.ImageUrl = uri;
                    }
                }
                // meal contents images
                if(model.MealContents != null && model.MealContents.Any())
                {
                    foreach (var content in model.MealContents)
                    {
                        content.ImageUrl = Utility.FileHelper.SaveImage(model.MealImage, "MealContentImages");  
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
                Priority = category.Priority,
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
                    category.Priority = model.Priority;
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

        public async Task<IActionResult> DeleteRestaurantCategory(int? id)
        {
            if (id == 0)
                return BadRequest("Id is null!");

            var category = await restaurantApi.GetRestaurantCaregoryById(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("DeleteRestaurantCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestaurantCategoryConfirmed(int? id)
        {
            try
            {
                await restaurantApi.DeleteRestaurantCategory(id.Value);
                return RedirectToAction("GetAllRestaurantCategories");
            }
            catch
            {
                ModelState.AddModelError("", "An Unknown Error ocurred");
            }
            var category = await restaurantApi.GetRestaurantCaregoryById(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
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
                    //string uri = SaveRestaurantImageAndGetUri(files[i]);
                    string uri = ImageSave(files[i], "restaurant");
                    var img = model.Images[i];
                    img.ImageUrl = uri;
                    images.Add(img);
                } 
            }
        }

        public string ImageSave(IFormFile photo, string imageType)
        {
            string filePath = "";
            if (imageType == "restaurant")
            {
                filePath = Utility.FileHelper.SaveImage(photo, "RestaurantImage"); 
            }else if(imageType == "mealImage")
            {
                filePath = Utility.FileHelper.SaveImage(photo, "MealImages");
            }
            else if(imageType == "mealcontent")
            {
                filePath = Utility.FileHelper.SaveImage(photo, "MealContentImages");
            }
             
            return filePath;
        }

       
    }
}