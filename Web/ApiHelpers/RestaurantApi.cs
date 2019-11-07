using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.DTO;
using Web.Models;

namespace Web.ApiHelpers
{
    public class RestaurantApi
    {
        string baseUrl = "https://localhost:44390/api/v1/";
        public async Task CreateRestaurant(CreateRestaurantViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("restaurants", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task UpdateRestaurant(CreateRestaurantViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("restaurants", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteRestaurant(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.DeleteAsync($"restaurants/delete/{Id}");
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task<List<RestaurantCategoryResponseDTO>> GetAllRestaurantCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("restaurants/categories");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<RestaurantCategoryResponseDTO>>();
                return result;
            }
        }

        public async Task CreateRestaurantCategories(AddRestaurantCategoryViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("restaurants/categories/create", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task<RestaurantCategoryResponseDTO> GetRestaurantCaregoryById(int categoryId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"restaurants/categories/{categoryId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<RestaurantCategoryResponseDTO>();
                return result;
            }
        }

        public async Task UpdateRestaurantCategory(RestaurantCategoryResponseDTO data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync($"restaurants/categories/edit/{data.Id}", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteRestaurantCategory(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.DeleteAsync($"restaurants/categories/delete/{Id}");
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task<List<AreaResponseDTO>> GetAllAreas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("restaurants/areas");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<AreaResponseDTO>>();
                return result;
            }
        }

        public async Task<List<RestaurantBasicResponseDTO>> GetAllRestaurants()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("restaurants/basic-details");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<RestaurantBasicResponseDTO>>();
                return result;
            }
        }

        public async Task<RestaurantBasicResponseDTO> GetRestaurantById(int restaurantId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"restaurants/{restaurantId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<RestaurantBasicResponseDTO>();
                return result;
            }
        }

        public async Task<CreateRestaurantViewModel> GetRestaurantByrestaurantId(int restaurantId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"restaurants/{restaurantId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<CreateRestaurantViewModel>();
                return result;
            }
        }

        public async Task<List<MealTypeResponseDTO>> GetMealTypesForRestaurant(int restaurantId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"restaurants/meal-types/{restaurantId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealTypeResponseDTO>>();
                return result;
            }
        }

        public async Task<List<MealCategoryResponseDTO>> GetAllMealCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("restaurants/meal-categories");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealCategoryResponseDTO>>();
                return result;
            }
        }
        public async Task<List<ImageResponseDTO>> GetImageForRestaurant(int restaurantId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"restaurants/image/{restaurantId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<ImageResponseDTO>>();
                return result;
            }
        }

        public async Task AddnewMeal(AddMealViewModel meal)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(meal), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("restaurants/add-meal", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }


    }
}
