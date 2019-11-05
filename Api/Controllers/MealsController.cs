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
    [Route("api/v1/[controller]")] 
    [ApiController]
    public class MealsController : ControllerBase
    {
        IMealService mealService;
        IMealCategoryService mealCategoryService;
        IRestaurantService restaurantService;
        IMealTypeService mealTypeService;
        public MealsController(IMealService _mealService, IMealCategoryService _mealCategoryService, IRestaurantService _restaurantService, IMealTypeService _mealTypeService)
        {
            mealService = _mealService;
            restaurantService = _restaurantService;
            mealCategoryService = _mealCategoryService;
            mealTypeService = _mealTypeService;
        }

        [HttpGet] 
        public IActionResult GetAll()
        {
            var data = mealService.GetMeals().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });

            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var data = mealService.GetMealById(id);
            var response = new MealResponseDTO
            {
                Description = data.Description,
                LocalPriority = data.LocalPriority,
                GeneralPriority = data.GeneralPriority,
                Name = data.Name,
                Price = data.Price,
                ImageUrl = data.ImageUrl
            };
            if(data.MealContents != null && data.MealContents.Any())
            {
                var x = new List<MealContent>();
                foreach (var item in data.MealContents)
                {
                    x.Add(item);
                }
                response.MealContents = x;
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("bycategory/{id}")]
        public IActionResult GetByCategoryId([FromRoute]int categoryId)
        {
            var data = mealService.GetMealByCategoryId(categoryId);
            
            return Ok(data);
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
                if(mealRequest.MealContents != null && mealRequest.MealContents.Any())
                {
                    meal.MealContents = mealRequest.MealContents;
                }
                mealService.AddMeal(meal);
                return Ok("Meal added!");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while saving meal");
            }

        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateMeal([FromBody] MealCreateRequestDTO mealRequest)
        {
            if (mealRequest == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            try
            {
                var meal = mealService.GetMealById(mealRequest.Id);
                if (meal == null)
                    return NotFound();

                meal.Name = mealRequest.Name;
                meal.Description = mealRequest.Description;
                meal.Price = mealRequest.Price;
                meal.GeneralPriority = mealRequest.GeneralPriority;
                meal.LocalPriority = mealRequest.LocalPriority;
                meal.ImageUrl = mealRequest.ImageUrl;
                meal.MealCategoryId = mealRequest.MealCategoryId;
                meal.RestaurantId = mealRequest.RestaurantId;
                meal.MealTypeId = meal.MealTypeId;


                List<MealContent> mealContents = null;
                if (mealRequest.MealContents != null && mealRequest.MealContents.Any())
                {
                    mealContents = mealRequest.MealContents;
                }
                mealService.EditMeal(meal, mealContents);
                return Ok("Meal updated!");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while saving meal");
            }

        }

        // meal categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetMealCategories()
        {
            var mealCategories = await mealCategoryService.GetAllMealCategories();
            return Ok(mealCategories);
        }

        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> GetMealCategoryById([FromRoute] int id)
        {
            var category = await mealCategoryService.GetMealCategoryById(id);
            return Ok(category);
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
            var mealTypes = mealTypeService.GetAllMealTypesByRestaurantId(restaurantId);
            return Ok(mealTypes);
        }
        [HttpGet]
        [Route("types/ById/{Id}")]
        public IActionResult GetMealTypesById([FromRoute] int Id)
        {
            var mealTypes = mealTypeService.GetMealTypeById(Id);
            return Ok(mealTypes);
        }

        [HttpGet]
        [Route("types")]
        public async Task<IActionResult> GetAllMealTypes()
        {
            var mealTypes = await mealTypeService.GetAllMealTypes();
            return Ok(mealTypes);
        }

        [HttpPost("types/create")]
        public async Task<IActionResult> AddMealType([FromBody]MealType mealType)
        {
            if (mealType == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            var meal = new MealType()
            {
                Name = mealType.Name,
                RestaurantId = mealType.RestaurantId,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };
            var mealCreated = await mealTypeService.AddMealType(meal);

            return Ok(mealCreated);
        }

        [HttpPut("types/edit")]
        public IActionResult EditMealType([FromBody]MealType mealType)
        {
            if (mealType == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            try
            {
                var mealTypeResponse = mealTypeService.GetMealTypeById(mealType.Id);
                if (mealTypeResponse == null)
                    return NotFound();

                mealTypeResponse.Name = mealType.Name;
                mealTypeResponse.DateUpdated = DateTime.Now;
                mealTypeResponse.RestaurantId = mealType.RestaurantId;

                mealTypeService.EditMealType(mealType);
                return Ok("Meal Type Updated");
            }
            catch(Exception ex)
            {
                return BadRequest("An error occured while updating meal type");
            }

        }
    }
}