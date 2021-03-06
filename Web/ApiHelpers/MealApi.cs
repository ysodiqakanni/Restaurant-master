﻿using Newtonsoft.Json;
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
    public class MealApi
    {
        string baseUrl = "https://localhost:44390/api/v1/";
        public async Task<List<MealDTO>> GetAllMeals()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("meals");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealDTO>>();
                return result;
            }
        }

        public async Task<AddMealViewModel> GetMealById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"meals/{id}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<AddMealViewModel>();
                return result;
            }
        }
        public async Task UpdateMeal(AddMealViewModel meal)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(meal), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("meals", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task AddMealCategory(CreateMealCategoryViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("meals/category", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task<MealCategoryViewModel> GetMealCategoryById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"meals/category/{id}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<MealCategoryViewModel>();
                return result;
            }
        }
        public async Task UpdateMealCategory(CreateMealCategoryViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync($"meals/category/{data.Id}", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task<List<MealTypeDTO>> GetAllMealType()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("meals/types");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealTypeDTO>>();
                return result;
            }
        }
        public async Task<List<MealTypeDTO>> GetAllMealTypeByRestaurantId(int restaurantId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"meals/types/{restaurantId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealTypeDTO>>();
                return result;
            }
        }
        public async Task<MealTypeDTO> GetAllMealTypeById(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"meals/types/ById/{Id}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<MealTypeDTO>();
                return result;
            }
        }
        public async Task AddMealType(CreateMealTypeViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("meals/types/create", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task UpdateMealType(CreateMealTypeViewModel mealType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(mealType), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("meals/types/edit", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
    }
}
