using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Model.Options;

namespace RedeSocial.Apresentacao.HttpServices
{
    public class CommentPostHttpServices:ICommentPostServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ProjetoHttpOptions> _projetoHttpOptions;

        public CommentPostHttpServices(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ProjetoHttpOptions> projetoHttpOptions)
        {
            _httpClientFactory = httpClientFactory;
            _projetoHttpOptions = projetoHttpOptions;

            _httpClient = httpClientFactory.CreateClient(projetoHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_projetoHttpOptions.CurrentValue.Timeout);
        }

        public async Task CreateAsync(CommentPostModel postModel)
        {
            var path = $"{_projetoHttpOptions.CurrentValue.CommentPostPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(path, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.CommentPostPath}/{id}";

            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task<CommentPostModel> GetByIdAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.CommentPostPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<CommentPostModel>(result);
        }


        public async Task<IEnumerable<CommentPostModel>> GetPostByIdAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.CommentPostPath}/list/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<IEnumerable<CommentPostModel>>(result);
        }

        public async Task<IEnumerable<CommentPostModel>> GetCommentByUserAsync(string userId)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.CommentPostPath}/commentbyuser/{userId}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<IEnumerable<CommentPostModel>>(result);
        }

        public async Task UpdateAsync(CommentPostModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentPostModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}
