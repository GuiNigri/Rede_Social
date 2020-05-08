using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedeSocial.Model.Entity;


namespace RedeSocial.Apresentacao.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> PostToApiAsync(string Uri, Object model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://localhost:44325/");

            var perfil = JsonConvert.SerializeObject(model);

            var content = new StringContent(perfil, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(Uri, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return base.Ok();
            }
            else
            {
                return base.ValidationProblem();
            }
        }

        public async Task<UsuarioModel> GetFromApiAsync(string Uri)
        {
            UsuarioModel usuarioModel = null;

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://localhost:44325/");

            var response = await httpClient.GetAsync(Uri);

            if (response.IsSuccessStatusCode)
            {
                usuarioModel = await response.Content.ReadAsAsync<UsuarioModel>();
            }

            return usuarioModel;
        }

        public async Task<IActionResult> PutToApiAsync(string Uri, Object model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://localhost:44325/");

            var perfil = JsonConvert.SerializeObject(model);

            var content = new StringContent(perfil, Encoding.UTF8, "application/json");

            var response = httpClient.PutAsync(Uri, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return base.Ok();
            }
            else
            {
                return base.ValidationProblem();
            }
        }

        public async Task<IActionResult> DeleteAsync(string Uri)
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://localhost:44325/");

            var response = httpClient.DeleteAsync(Uri).Result;

            if (response.IsSuccessStatusCode)
            {
                return base.Ok();
            }
            else
            {
                return base.ValidationProblem();
            }
        }

        //public async Task<bool> GetFromApiCpfAsync(string Uri)
        //{
        //    var resposta = false;
        //
        //    var httpClient = _httpClientFactory.CreateClient();
        //
        //    httpClient.BaseAddress = new Uri("https://localhost:44325/");
        //
        //    var response = await httpClient.GetAsync(Uri);
        //
        //    if (response.IsSuccessStatusCode)
        //    {
        //        resposta =  await response.Content.ReadAsAsync<bool>();
        //    }
        //
        //    return resposta;
        //}

    }
}