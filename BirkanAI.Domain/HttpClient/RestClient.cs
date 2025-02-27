using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace BirkanAI.Domain.HttpClient
{
    public static class RestClient<Req, Res> where Req : class where Res : class
    {
        private static readonly System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient(); // Fully qualify HttpClient

        public static async Task<Res> PostAsync(Req request, string url)
        {
            try
            {
                HttpResponseMessage response =  _httpClient.PostAsJsonAsync(url, request).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadFromJsonAsync<Res>().Result;
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}

