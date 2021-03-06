﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DTO;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        IRestaurantService restaurantService;
        IMealService mealService;
        IMealCategoryService mealCategoryService;
        private readonly IMapper mapper;
        public RestaurantsController(IRestaurantService _restaurantService, IMapper _mapper, IMealService _mealService, IMealCategoryService _mealCategoryService)
        {
            restaurantService = _restaurantService;
            mapper = _mapper;
            mealService = _mealService;
            mealCategoryService = _mealCategoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurants = restaurantService.GetAll();
            var response = new List<RestaurantResponseDTO>();
            if (restaurants == null || !restaurants.Any())
                return Ok(response);
            response = GetRestaurantResponseAll(restaurants);
            return Ok(response.ToList()); 
        }

        [HttpGet("basic-details")]
        public IActionResult GetAllRestaurantBasic()
        {
            var all = restaurantService.GetAllWithBasicDetails();
            return Ok(all);
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById([FromRoute]int Id)
        {
            var restaurant = restaurantService.GetRestaurantById(Id);
            if (restaurant == null)
                return NotFound();
            return Ok(GetRestaurantResponse(restaurant));
        }

        [HttpPost]
        public IActionResult AddRestaurant([FromBody] RestaurantCreateRequestDTO restaurantRequest)
        {
            if (restaurantRequest == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            var restaurant = new Restaurant
            {
                Name = restaurantRequest.Name,
                Address = restaurantRequest.Address,
                Longitude = restaurantRequest.Longitude,
                Latitude = restaurantRequest.Latitude,
                PhoneNumber = restaurantRequest.PhoneNumber,
                Priority = restaurantRequest.Priority,
                RestaurantCategoryId = restaurantRequest.RestaurantCategoryId,
                AreaId = restaurantRequest.AreaId,
                Description = restaurantRequest.Description,
               
            };

            // save images
            if(restaurantRequest.Images != null && restaurantRequest.Images.Any())
            {
                foreach (var image in restaurantRequest.Images)
                {
                    image.DateCreated = DateTime.Now;
                    image.DateUpdated = DateTime.Now;
                }
                restaurant.RestaurantImages = restaurantRequest.Images; 
            }

            // save the working hours
            if (restaurantRequest.WorkingHours != null && restaurantRequest.WorkingHours.Any())
            {
                foreach (var hour in restaurantRequest.WorkingHours)
                {
                    hour.DateCreated = DateTime.Now;
                    hour.DateUpdated = DateTime.Now;
                }
                restaurant.WorkingHours = restaurantRequest.WorkingHours;
            }

            // commit to db
            var response = restaurantService.CreateNewRestaurant(restaurant);

            return Ok(response);
        }

        [HttpPut]
        public IActionResult EditRestaurant([FromBody] RestaurantUpdateRequestDTO restaurantRequest)
        {
            if (restaurantRequest == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            Restaurant restaurant = restaurantService.GetRestaurantById(restaurantRequest.Id);
            if(restaurant == null)
                return BadRequest("Record not found in our database");

            restaurant.Name = restaurantRequest.Name;
            restaurant.PhoneNumber = restaurantRequest.PhoneNumber;
            restaurant.Latitude = restaurantRequest.Latitude;
            restaurant.Longitude = restaurantRequest.Longitude;
            restaurant.Address = restaurantRequest.Address; 
            restaurant.DateUpdated = DateTime.Now;
            restaurant.Description = restaurantRequest.Description;
            restaurant.Priority = restaurantRequest.Priority;
            restaurant.RestaurantCategoryId = restaurantRequest.RestaurantCategoryId;
            restaurant.AreaId = restaurantRequest.AreaId;

            // save images
            var images = new List<RestaurantImage>();
            if (restaurantRequest.Images != null && restaurantRequest.Images.Any())
            {
                foreach (var image in restaurantRequest.Images)
                {
                    image.DateUpdated = DateTime.Now;
                }
                images = restaurantRequest.Images;
            }

            // save the working hours
            var hours = new List<WorkingHour>();
            if (restaurantRequest.WorkingHours != null && restaurantRequest.WorkingHours.Any())
            {
                foreach (var hour in restaurantRequest.WorkingHours)
                {
                    hour.DateUpdated = DateTime.Now;
                }
                hours = restaurantRequest.WorkingHours;
            }

            // commit to db
            var response = restaurantService.UpdateRestaurant(restaurant, images, hours);

            return Ok(response);
        }

        [HttpDelete("delete/{Id}")]
        public IActionResult DeleteRestaurant(int Id = 0)
        {
            if (Id == 0)
                return BadRequest("RestaurantId is null!");
            Restaurant restaurant = restaurantService.GetRestaurantById(Id);
            if (restaurant == null)
                return NotFound();
            restaurantService.DeleteRestaurant(restaurant);

            return Ok();
        }

        [HttpGet("meal-categories")]
        public async Task<IActionResult> GetMealCategories()
        {
            var mealCategories = await mealCategoryService.GetAllMealCategories();
            return Ok(mealCategories);
        }

        [HttpGet]
        [Route("meal-types/{restaurantId}")]
        public IActionResult GetMealTypes([FromRoute] int restaurantId)
        {
            var mealTypes = restaurantService.GetAllMealTypesByRestaurantId(restaurantId);
            return Ok(mealTypes);
        }

        [HttpPost]
        [Route("add-meal")]
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
                    foreach (var content in mealRequest.MealContents)
                    {
                        content.DateCreated = DateTime.Now;
                        content.DateUpdated = DateTime.Now;
                    }
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

        //[HttpGet]
        //[Route("image/{Id}")]
        //public IActionResult GetRestaurantImageById([FromRoute]int Id = 0)
        //{
        //    if (Id == 0)
        //        return BadRequest("Image is null!");

        //    var images = restaurantService.GetAllImagesByRestaurantId(Id);
        //    if (images == null)
        //        return NotFound();

        //    return Ok(images);
        //}

        //[HttpGet]
        //[Route("work/{Id}")]
        //public IActionResult GetRestaurantWorkById([FromRoute]int Id = 0)
        //{
        //    if (Id == 0)
        //        return BadRequest("Image is null!");

        //    var work = restaurantService.GetWorkingHoursByRestaurantId(Id);
        //    if (work == null)
        //        return NotFound();

        //    return Ok(work);
        //}

        [HttpGet("categories")]
        public IActionResult GetRestaurantCategories()
        {
            var categories = restaurantService.GetRestaurantCategories();
            return Ok(categories);
        }

        [HttpGet]
        [Route("categories/{Id}")]
        public IActionResult GetRestaurantCategoryById([FromRoute]int Id=0)
        {
            if (Id == 0)
                return BadRequest("Category is null!");

            var categories = restaurantService.GetRestaurantCategoryById(Id);
            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpPost("categories/create")]
        public IActionResult AddRestaurantCategory([FromBody]RestaurantCategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            var category = new RestaurantCategory()
            {
                Name = categoryCreateDTO.Name,
                Priority = categoryCreateDTO.Priority,
            };
            var categoryCreated = restaurantService.CreateNewRestaurantCategory(category);

            return Ok(categoryCreated);
        }

        [HttpPut("categories/edit/{Id}")]
        public IActionResult EditResturantCategory([FromBody] RestaurantCategoryUpdateDTO restaurantRequest,int id)
        {
            if (restaurantRequest == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            RestaurantCategory restaurantCategory = restaurantService.GetRestaurantCategoryById(id);
            if(restaurantCategory == null)
                return NotFound();

            restaurantCategory.Name = restaurantRequest.Name;
            restaurantCategory.Priority = restaurantRequest.Priority;
            restaurantCategory.DateUpdated = DateTime.Now;

            var response = restaurantService.UpdateRestaurantCategory(restaurantCategory);
            return Ok(response);
        }

        [HttpDelete("categories/delete/{Id}")]
        public IActionResult DeleteRestaurantCategory(int Id=0)
        {
            if (Id == 0)
                return BadRequest("Category is null!");
            RestaurantCategory restaurantCategory = restaurantService.GetRestaurantCategoryById(Id);
            if (restaurantCategory == null)
                return NotFound();
            restaurantService.DeleteRestaurantCategory(restaurantCategory);

            return Ok();
        }

        [HttpGet("areas")]
        public IActionResult GetAreas()
        {
            var areas = restaurantService.GetAreas();
            return Ok(areas);
        }

        [HttpGet]
        [Route("areas/{Id}")]
        public IActionResult GetRestaurantByAreaId([FromRoute]int Id = 0)
        {
            if (Id == 0)
                return BadRequest("Area is null!");

            var categories = restaurantService.GetRestaurantByAreaId(Id);
            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        #region Private Methods
        private RestaurantResponseDTO GetRestaurantResponse(Restaurant rest)
        {
            var resp = new RestaurantResponseDTO
            {
                ID = rest.Id,
                Name = rest.Name,
                Address = rest.Address,
                Description = rest.Description,
                Latitude = rest.Latitude,
                Longitude = rest.Longitude,
                PhoneNumber = rest.PhoneNumber,
                Priority = rest.Priority,
                DateCreated = rest.DateCreated,
                DateUpdated = rest.DateUpdated,
                Area = rest.Area?.Name,
                Category = rest.RestaurantCategory?.Name,
                AreaId = rest.AreaId == null ? 0 : rest.AreaId.Value,
                RestaurantCategoryId = rest.RestaurantCategoryId == null ? 0 : rest.RestaurantCategoryId.Value,
                Meals = rest.Meals
            };

            // get images
            if (rest.RestaurantImages != null && rest.RestaurantImages.Any())
            {
                var images = new List<ImageResponseDTO>();
                foreach (var img in rest.RestaurantImages)
                {
                    images.Add(
                        new ImageResponseDTO
                        {
                            ID = img.Id,
                            ImagePriority = img.ImagePriority,
                            ImageUrl = img.ImageUrl
                        });
                }
                resp.Images = images;
            }

            // get working hours
            if (rest.WorkingHours != null && rest.WorkingHours.Any())
            {
                var workingImages = new List<WorkingHourResponseDTO>();
                foreach (var hour in rest.WorkingHours)
                {
                    workingImages.Add(
                        new WorkingHourResponseDTO
                        {
                            ID = hour.Id,
                            Day = hour.Day,
                            FromTime = hour.FromTime,
                            ToTime = hour.ToTime
                        });
                }
                resp.WorkingHours = workingImages;
            }
            // get Meals
            if (rest.Meals != null && rest.Meals.Any())
            {
                var meals = new List<Meal>();
                foreach (var meal in rest.Meals)
                {
                    meals.Add(
                        new Meal
                        {
                            Id = meal.Id,
                            DateCreated = meal.DateCreated,
                            DateUpdated =  meal.DateUpdated,
                            ImageUrl = meal.ImageUrl,
                            GeneralPriority = meal.GeneralPriority,
                            LocalPriority = meal.LocalPriority,
                            MealCategory = meal.MealCategory,
                            MealContents = meal.MealContents,
                            MealType = meal.MealType,
                            Name = meal.Name,
                            Price = meal.Price,
                            RestaurantId = meal.RestaurantId,
                            Description = meal.Description,
                            MealCategoryId = meal.MealCategoryId,
                            MealTypeId = meal.MealTypeId
                        });
                }
                resp.Meals = meals;
            }
            return resp;
        }

        private List<RestaurantResponseDTO> GetRestaurantResponseAll(List<Restaurant> restaurants)
        {
            var result = new List<RestaurantResponseDTO>();
            foreach (var item in restaurants)
            {
                result.Add(GetRestaurantResponse(item));
            }
            return result;
        }
        #endregion
    }
}