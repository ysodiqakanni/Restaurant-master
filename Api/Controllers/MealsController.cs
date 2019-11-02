using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DTO;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        IMealService mealService;
        IMealCategoryService mealCategoryService;
        IRestaurantService restaurantService;
        public MealsController(IMealService _mealService, IMealCategoryService _mealCategoryService, IRestaurantService _restaurantService)
        {
            mealService = _mealService;
            restaurantService = _restaurantService;
            mealCategoryService = _mealCategoryService;
        }
        
        [HttpPost]
        [Route("")]
        public IActionResult AddMeal([FromBody] MealCreateRequestDTO mealRequest)
        {
            if (mealRequest == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            try
            {
                var meal = new Meal
                {
                    Name = mealRequest.Name,
                    Description = mealRequest.Description,
                    Price = mealRequest.Price,
                    GeneralPriority = mealRequest.GeneralPriority,
                    LocalPriority = mealRequest.LocalPriority,
                    ImageUrl = mealRequest.ImageUrl,
                    MealCategoryId = mealRequest.MealCategoryId,
                    RestaurantId = mealRequest.RestaurantId,
                    MealTypeId = mealRequest.MealTypeId
                };
                mealService.AddMeal(meal);
                return Ok("Meal added!");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while saving meal");
            }

        }

        // meal categories
        [HttpGet("categories")]
        public IActionResult GetMealCategories()
        {
            var mealCategories = mealCategoryService.GetAllMealCategories();
            return Ok(mealCategories);
        }

        [HttpPost]
        [Route("category")]
        public IActionResult AddMealCategory([FromBody] MealCategoryCreateRequestDTO mealCategory)
        {
            if (mealCategory == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");
            try
            {
                MealCategory obj = new MealCategory
                {
                    Name = mealCategory.Name,
                    Priority = mealCategory.Priority,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                };
                var response = mealCategoryService.AddMealCategory(obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log ex
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "An error occured while saving meal category" });
            }
        }

        [HttpPut]
        [Route("category/{id}")]
        public async Task<IActionResult> UpdateMealCategory([FromBody] MealCategoryCreateRequestDTO mealCategory, [FromRoute] int id)
        {
            if (mealCategory == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");
            try
            {
                var obj = await mealCategoryService.GetMealCategoryById(id);
                if (obj == null)
                    return BadRequest("Record not found in our database");

                obj.Name = mealCategory.Name;
                obj.Priority = mealCategory.Priority;
                obj.DateUpdated = DateTime.Now;
                 
                var response = mealCategoryService.EditMealCategory(obj);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log ex
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "An error occured while updating meal category" });
            }
        }

        // meal types
        [HttpGet]
        [Route("types/{restaurantId}")]
        public IActionResult GetMealTypes([FromRoute] int restaurantId)
        {
            var mealTypes = restaurantService.GetAllMealTypesByRestaurantId(restaurantId);
            return Ok(mealTypes);
        }

    }
}