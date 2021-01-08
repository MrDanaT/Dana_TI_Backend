using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TennisClub.UI
{
    public class WebAPI
    {
        private const string BASE_API_URL = "https://localhost:44356/api/";

        public static Task<HttpResponseMessage> GetCall(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string? apiUrl = BASE_API_URL + url;
            using (HttpClient? client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage>? response = client.GetAsync(apiUrl);
                response.Wait();
                return response;
            }
        }

        public static Task<HttpResponseMessage> PostCall<T>(string url, T model) where T : class
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string? apiUrl = BASE_API_URL + url;
            using (HttpClient? client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage>? response = client.PostAsJsonAsync(apiUrl, model);
                response.Wait();
                return response;
            }
        }

        public static Task<HttpResponseMessage> PutCall<T>(string url, T model)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string? apiUrl = BASE_API_URL + url;
            using (HttpClient? client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage>? response = client.PutAsJsonAsync(apiUrl, model);
                response.Wait();
                return response;
            }
        }

        public static Task<HttpResponseMessage> DeleteCall(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string? apiUrl = BASE_API_URL + url;
            using (HttpClient? client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage>? response = client.DeleteAsync(apiUrl);
                response.Wait();
                return response;
            }
        }
    }
}