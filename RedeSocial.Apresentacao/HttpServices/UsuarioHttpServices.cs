using System;
using System.Collections.Generic;
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
    public class UsuarioHttpServices:IUsuarioServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ProjetoHttpOptions> _projetoHttpOptions;
        public UsuarioHttpServices(IHttpClientFactory httpClientFactory, IOptionsMonitor<ProjetoHttpOptions> projetoHttpOptions)
        {
            _httpClientFactory = httpClientFactory;
            _projetoHttpOptions = projetoHttpOptions;

            _httpClient = httpClientFactory.CreateClient(projetoHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromMinutes(_projetoHttpOptions.CurrentValue.Timeout);
        }
        public async Task CreateAsync(UsuarioModel usuarioModel, string base64)
        {
            var createModel = new CreateAndUpdateHttpUsuarioModel
            {
                UsuarioModel = usuarioModel,
                ImageBase64 = base64
            };

            var path  = $"{_projetoHttpOptions.CurrentValue.UsuarioPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(createModel), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(path, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                
            }
        }

        public async Task UpdateAsync(UsuarioModel usuarioModel, string base64)
        {
            var updateModel = new CreateAndUpdateHttpUsuarioModel
            {
                UsuarioModel = usuarioModel,
                ImageBase64 = base64
            };

            var pathWithId  = $"{_projetoHttpOptions.CurrentValue.UsuarioPath}/{usuarioModel.IdentityUser}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");
           
            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);
           
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                
            }
        }

        public async Task DeleteAsync(string id)
        {
            var pathWithId  = $"{_projetoHttpOptions.CurrentValue.UsuarioPath}/{id}";

            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                
            }
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            var path = $"{_projetoHttpOptions.CurrentValue.UsuarioPath}";
            var result = await _httpClient.GetStringAsync(path);
            return JsonConvert.DeserializeObject<IEnumerable<UsuarioModel>>(result);
        }

        public async Task<UsuarioModel> GetByIdAsync(string id)
        {
            var pathWithId = $"{_projetoHttpOptions.CurrentValue.UsuarioPath}/{id}";
            var result = await _httpClient.GetStringAsync(pathWithId);
            return JsonConvert.DeserializeObject<UsuarioModel>(result);
        }

        public async Task<bool> GetByCpfAsync(long cpf)
        {
            throw new NotImplementedException();
        }
    }
}
