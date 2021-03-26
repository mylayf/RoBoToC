using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebUI.ApiProcess
{
    public static class ApiHelper
    {
        static string BaseUrl = "https://localhost:44331/";
        static HttpClient httpClient = new HttpClient();
        public static async Task<HttpResponseMessage> Post(HttpContent content, CancellationToken cancellationToken, bool TokenRequiring = true, string path = "Add")
        {
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            var response = await httpClient.PostAsync(path, content);
            var test = await response.Content.ReadAsStringAsync();
            return response;
        }
        public static async Task<HttpResponseMessage> Put(HttpContent content, CancellationToken cancellationToken, string path = "Update")
        {
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            var response = await httpClient.PutAsync(path, content);
            return response;
        }
        public static async Task<HttpResponseMessage> Delete(int Id, CancellationToken cancellationToken, string path = "Delete")
        {
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            var response = await httpClient.DeleteAsync(path + "/" + Id);
            return response;
        }
        public static async Task<HttpResponseMessage> GetAll(CancellationToken cancellationToken, string path = "GetAll")
        {
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            var response = await httpClient.GetAsync(path);
            return response;
        }
        public static async Task<HttpResponseMessage> GetById(int Id, CancellationToken cancellationToken, string path = "Edit")
        {
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "");
            var response = await httpClient.GetAsync(path + "/" + Id);
            return response;
        }
        public static HttpContent SerializeContent(dynamic entity)
        {
            return new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        }
    }
}
