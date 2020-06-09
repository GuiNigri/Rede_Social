using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeSocial.Apresentacao.Models;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostServices _postServices;
        private readonly IUsuarioServices _usuarioServices;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAmigosServices _amigosServices;

        public HomeController(ILogger<HomeController> logger, IPostServices postServices, IUsuarioServices usuarioServices, UserManager<IdentityUser> userManager, IAmigosServices amigosServices)
        {
            _logger = logger;
            _postServices = postServices;
            _usuarioServices = usuarioServices;
            _userManager = userManager;
            _amigosServices = amigosServices;
        }

        public async Task<IActionResult> Index()
        {
            var solicitacoes = await GetSolicitacoesAmizade();

            var posts = await GetPostsAsync();

            var user = await _userManager.GetUserAsync(User);
            var usuarioLogado = await _usuarioServices.GetByIdAsync(user.Id);
            var usuarioLogadoNome = usuarioLogado.Nome + " " + usuarioLogado.Sobrenome;

            var homeViewModel = new HomeViewModel
            {
                IdentityUser = user.Id,
                NomePerfil = usuarioLogadoNome,
                FotoPerfil = usuarioLogado.FotoPerfil,
                ListaPost = posts,
                SolicitacoesPendentes = solicitacoes
            };

            return View(homeViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<PostViewModel> ConverterIdToNameAndModelToViewModel(PostModel postModel)
        {
            var usuario = await _usuarioServices.GetByIdAsync(postModel.IdentityUser);

            var nomeUsuario = usuario.Nome + " " + usuario.Sobrenome;

            (var tempo, var formatoDeTempo) = DefinirTempoPostagem(postModel.DataPostagem);

            var postViewModel = new PostViewModel
            {
                Id = postModel.Id,
                NomeCompleto = nomeUsuario,
                Privacidade = postModel.Privacidade,
                Texto = postModel.Texto,
                UriImage = postModel.UriImage,
                TempoDaPostagem = tempo,
                FormatacaoTempo = formatoDeTempo,
                IdentityUser = postModel.IdentityUser,
                FotoPerfil = usuario.FotoPerfil
            };

            return postViewModel;
        }

        private static (int, string) DefinirTempoPostagem(DateTime dataPostagem)
        {

            var tempoDaPostagem = DateTime.Now - dataPostagem;

            var tempo = (int)tempoDaPostagem.TotalMinutes;

            var formatoDeTempo = "min";

            if (tempo >= 60 && tempo < 1440)
            {
                //hora
                tempo /= 60;
                formatoDeTempo = "Horas";
            }
            else if (tempo >= 1440 && tempo < 43800)
            {
                //dia
                tempo /= 1440;
                formatoDeTempo = "Dias";
            }
            else if (tempo >= 43800 && tempo < 525600)
            {
                //mes
                tempo /= 43800;
                formatoDeTempo = "Meses";
            }
            else if (tempo > 525600)
            {
                //ano
                tempo /= 525600;
                formatoDeTempo = "Anos";
            }

            return (tempo, formatoDeTempo);
        }

        private async Task<List<PostViewModel>> GetPostsAsync()
        {
            var postLista = new List<PostViewModel>();

            var posts = await _postServices.GetAllAsync();

            foreach (var post in posts)
            {
                var postViewModel = await ConverterIdToNameAndModelToViewModel(post);

                postLista.Add(postViewModel);
            }

            return postLista;
        }

        private async Task<IEnumerable<AmigosSolicitacoesViewModel>> GetSolicitacoesAmizade()
        {
            var listaFormatada = new List<AmigosSolicitacoesViewModel>();

            var user = await _userManager.GetUserAsync(User);

            var solicitacoes = await _amigosServices.GetSolicitacoesPendentes(user.Id);

            foreach (var solicitacao in solicitacoes)
            {
                var amigosSolicitacoesViewModel = await ConverterIdToNameAndModelToViewModel(solicitacao);

                listaFormatada.Add(amigosSolicitacoesViewModel);
            }

            return listaFormatada;

        }

        public async Task<AmigosSolicitacoesViewModel> ConverterIdToNameAndModelToViewModel(AmigosModel amigosModel)
        {
            var usuario = await _usuarioServices.GetByIdAsync(amigosModel.UserId2);

            var nomeUsuario = usuario.Nome + " " + usuario.Sobrenome;

            var amigosSolicitacoesViewModel = new AmigosSolicitacoesViewModel()
            {
                Id = amigosModel.Id,
                NomeCompleto = nomeUsuario,
                Perfil = usuario.IdentityUser
            };

            return amigosSolicitacoesViewModel;
        }
    }
}
