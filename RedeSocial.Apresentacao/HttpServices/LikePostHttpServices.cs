using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Model.Options;

namespace RedeSocial.Apresentacao.HttpServices
{
    public class LikePostHttpServices: ILikePostServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ProjetoHttpOptions> _projetoHttpOptions;

        public LikePostHttpServices(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ProjetoHttpOptions> projetoHttpOptions)
        {
            _httpClientFactory = httpClientFactory;
            _projetoHttpOptions = projetoHttpOptions;

            _httpClient = httpClientFactory.CreateClient(projetoHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_projetoHttpOptions.CurrentValue.Timeout);
        }
        public async Task CreateAsync(LikePostModel likePostModel)
        {
            var path = $"{_projetoHttpOptions.CurrentValue.LikePostPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(likePostModel), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(path, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.LikePostPath}/{id}";

            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task<IEnumerable<LikePostModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LikePostModel>> GetPostByIdAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.LikePostPath}/list/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<IEnumerable<LikePostModel>>(result);
        }

        public async Task<LikePostModel> GetStatusAsync(string userId, int idPost)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.LikePostPath}/{userId}/{idPost}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<LikePostModel>(result);
        }

        public async Task<IEnumerable<LikePostModel>> GetLikeByUserAsync(string userId)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.LikePostPath}/likebyuser/{userId}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<IEnumerable<LikePostModel>>(result);
        }

        public async Task UpdateAsync(LikePostModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<LikePostModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
