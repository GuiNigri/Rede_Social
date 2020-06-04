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

        public HomeController(ILogger<HomeController> logger, IPostServices postServices, IUsuarioServices usuarioServices, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _postServices = postServices;
            _usuarioServices = usuarioServices;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var postLista = new List<PostViewModel>();
            var posts = await _postServices.GetAll();

            foreach (var post in posts)
            {
                   
                var postViewModel = await ConverterIdToNameAndModelToViewModel(post);

                postLista.Add(postViewModel);

            }

            var user = await _userManager.GetUserAsync(User);
            var usuarioLogado = await _usuarioServices.GetByIdAsync(user.Id);
            var usuarioLogadoNome = usuarioLogado.Nome + " " + usuarioLogado.Sobrenome;

            var homeViewModel = new HomeViewModel
            {
                NomePerfil = usuarioLogadoNome,
                FotoPerfil = usuarioLogado.FotoPerfil,
                ListaPost = postLista
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
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
                    FormatacaoTempo = formatoDeTempo
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
                //mes
                tempo /= 525600;
                formatoDeTempo = "Anos";
            }

            return (tempo, formatoDeTempo);
        }
    }
}
