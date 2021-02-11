using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Model.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Apresentacao.HttpServices
{
    public class PostHttpServices : IPostServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ProjetoHttpOptions> _projetoHttpOptions;

        public PostHttpServices(IHttpClientFactory httpClientFactory, IOptionsMonitor<ProjetoHttpOptions> projetoHttpOptions)
        {
            _httpClientFactory = httpClientFactory;
            _projetoHttpOptions = projetoHttpOptions;

            _httpClient = httpClientFactory.CreateClient(projetoHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_projetoHttpOptions.CurrentValue.Timeout);
        }

        public async Task CreateAsync(PostModel postModel)
        {
            var path  = $"{_projetoHttpOptions.CurrentValue.PostPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(path, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                
            }
        }

        public async Task DeleteAsync(int id, string uri = null)
        {
            var pathWithId  = $"{_projetoHttpOptions.CurrentValue.PostPath}/{id}";

            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                
            }
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            var path = $"{_projetoHttpOptions.CurrentValue.PostPath}";
            var result = await _httpClient.GetStringAsync(path);
            return JsonConvert.DeserializeObject<IEnumerable<PostModel>>(result);
        }

        public async Task<PostModel> GetByIdAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.PostPath}/ById/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<PostModel>(result);
        }

        public async Task<IEnumerable<PostModel>> GetPostsByUserAsync(string id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.PostPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<IEnumerable<PostModel>>(result);
        }

        public Task UpdateAsync(PostModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
