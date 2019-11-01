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
    public class AreaApi
    {
        string baseUrl = "https://localhost:44390/api/v1/";

        public async Task<List<AreaBasicResponseDTO>> GetAllAreas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("area");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<AreaBasicResponseDTO>>();
                return result;
            }
        }

        public async Task<AreaBasicResponseDTO> GetAreaById(int areaId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"area/{areaId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<AreaBasicResponseDTO>();
                return result;
            }
        }

        public async Task CreateArea(CreateAreaViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("area", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task UpdateArea(AreaBasicResponseDTO data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("area", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
    }
}
