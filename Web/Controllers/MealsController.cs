using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiHelpers;
using Web.DTO;
using Web.Models;

namespace Web.Controllers
{
    public class MealsController : Controller
    {
        MealApi mealApi = new MealApi();
        RestaurantApi restaurantApi = new RestaurantApi();
        private readonly IHostingEnvironment hostingEnvironment;
        public MealsController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

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
                            string uri = ImageSave(file, "mealImage");
                            model.ImageUrl = uri;
                        }
                    }
                    // meal contents images
                    if (model.MealContents != null && model.MealContents.Any())
                    {
                        foreach (var content in model.MealContents)
                        {
                            content.ImageUrl = ImageSave(model.MealImage, "mealcontent");
                        }
                    }

                    await mealApi.UpdateMeal(model);
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occured during update!");
                    return View(model);
                }
            }
            var allRestaurants = await restaurantApi.GetAllRestaurants();
            var mealCategories = await restaurantApi.GetAllMealCategories();

            ViewBag.RestaurantId = new SelectList(allRestaurants, "Id", "Name", model.RestaurantId);
            ViewBag.MealCategoryId = new SelectList(mealCategories, "Id", "Name", model.MealCategoryId);
            return View(model);
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
                    return RedirectToAction("Categories");
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
                    await mealApi.UpdateMealCategory(model);
                    return RedirectToAction("Categories");
                }
            }
            catch (Exception ex)
            {
                // Log error here
                ModelState.AddModelError("", "An unknown error occured!");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Types()
        {
            var meals = await mealApi.GetAllMealType();
            return View(meals.ToList());
        }
        public async Task<IActionResult> AddMealType()
        {
            ViewBag.Restaurant = await restaurantApi.GetAllRestaurants();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMealType(CreateMealTypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await mealApi.AddMealType(model);
                    return RedirectToAction("Types");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Restaurant = await restaurantApi.GetAllRestaurants();
                ModelState.AddModelError("", "An unknown error occured!");
            }

            return View(model);
        }

        public async Task<IActionResult> EditMealType(int ? id)
        {
            if (id == null)
                return BadRequest();
            var mealType = await mealApi.GetAllMealTypeById(id.Value);
            if (mealType == null)
                return NotFound();
            var model = new CreateMealTypeViewModel
            {
                Id = mealType.Id,
                Name = mealType.Name,
                RestaurantId = mealType.RestaurantId
            };
            
            var selected = await restaurantApi.GetRestaurantById(mealType.RestaurantId);
            ViewBag.Selected = selected;
            var fetchedRestaurant = await restaurantApi.GetAllRestaurants();
            List<RestaurantBasicResponseDTO> New = fetchedRestaurant.ToList();
            New.RemoveAll(r => r.Id == selected.Id);
            //New.Remove(selected);
            ViewBag.Restaurant = New;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMealType(CreateMealTypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await mealApi.UpdateMealType(model);
                    return RedirectToAction("Types");
                }
            }
            catch (Exception ex)
            {
                // Log error here
                ModelState.AddModelError("", "An unknown error occured!");
            }

            return View(model);
        }
        public string ImageSave(IFormFile photo, string to)
        {
            string filePath = "";
            if (to == "mealImage")
            {
                filePath = SaveMealImageAndGetUri(photo);
            }
            else if (to == "mealcontent")
            {
                filePath = SaveMealContentImageAndGetUri(photo);
            }

            // copy the file to wwwroot/images folder
            photo.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }
        private string SaveMealImageAndGetUri(IFormFile formFile)
        {
            string uniqueFileName = "";
            //string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images/MealImage");
            string uploadsFolder = "wwwroot/Images/MealImage";
            Directory.CreateDirectory(uploadsFolder);
            // To make sure the file name is unique we are appending a new
            // GUID value and and an underscore to the file name
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "MealImage", uniqueFileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            return filePath;
        }

        private string SaveMealContentImageAndGetUri(IFormFile formFile)
        {
            string uniqueFileName = "";
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images/MealContentImages");
            Directory.CreateDirectory(uploadsFolder);
            // To make sure the file name is unique we are appending a new
            // GUID value and and an underscore to the file name
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            return filePath;
            //return "/path/MealContentImages/" + formFile.FileName;
        }

        //private string SaveMealImageAndGetUri(IFormFile formFile)
        //{
        //    return "/path/MealImages/" + formFile.FileName;
        //}
        //private string SaveMealContentImageAndGetUri(IFormFile formFile)
        //{
        //    return "/path/MealContentImages/" + formFile.FileName;
        //}

    }
}