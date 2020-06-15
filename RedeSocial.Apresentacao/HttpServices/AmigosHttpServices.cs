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
    public class AmigosHttpServices : IAmigosServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ProjetoHttpOptions> _projetoHttpOptions;

        public AmigosHttpServices(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ProjetoHttpOptions> projetoHttpOptions)
        {
            _httpClientFactory = httpClientFactory;
            _projetoHttpOptions = projetoHttpOptions;

            _httpClient = httpClientFactory.CreateClient(projetoHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_projetoHttpOptions.CurrentValue.Timeout);
        }

        public async Task CreateAsync(AmigosModel amigosModel)
        {
            var path = $"{_projetoHttpOptions.CurrentValue.AmigosPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(amigosModel), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(path, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task UpdateAsync(AmigosModel amigosModel)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.AmigosPath}/{amigosModel.Id}";
            var httpContent = new StringContent(JsonConvert.SerializeObject(amigosModel), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task DeleteAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.AmigosPath}/{id}";

            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {

            }
        }

        public async Task<IEnumerable<AmigosModel>> GetAllAsync()
        {
            var path = $"{_projetoHttpOptions.CurrentValue.AmigosPath}";
            var result = await _httpClient.GetStringAsync(path);
            return JsonConvert.DeserializeObject<IEnumerable<AmigosModel>>(result);
        }

        public async Task<AmigosModel> GetByUsersAsync(string userLogado, string perfilAcessado)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.AmigosPath}/{userLogado}/{perfilAcessado}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<AmigosModel>(result);
        }

        public async Task<AmigosModel> GetByIdAsync(int id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.AmigosPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<AmigosModel>(result);
        }

        public async Task<IEnumerable<AmigosModel>> GetSolicitacoesPendentes(string userLogado)
        {
            try
            {
                var pathWithId = $"{_projetoHttpOptions.CurrentValue.AmigosPath}/pendentes/{userLogado}";
                var result = await _httpClient.GetStringAsync(pathWithId);
                return JsonConvert.DeserializeObject<IEnumerable<AmigosModel>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<AmigosModel>> GetListByUserAsync(string userLogado)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.AmigosPath}/usuario/{userLogado}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<IEnumerable<AmigosModel>>(result);
        }

        public async Task<IEnumerable<AmigosModel>> GetAllByUserAsync(string userLogado)
        {
            throw new NotImplementedException();
        }
    }
}
