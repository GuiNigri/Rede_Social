using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Apresentacao.Models;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    public class AmigosController : Controller
    {
        private readonly IAmigosServices _amigosServices;
        private readonly IPostServices _postServices;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsuarioServices _usuarioServices;


        public AmigosController(IAmigosServices amigosServices, IPostServices postServices, UserManager<IdentityUser> userManager, IUsuarioServices usuarioServices)
        {
            _amigosServices = amigosServices;
            _postServices = postServices;
            _userManager = userManager;
            _usuarioServices = usuarioServices;
        }

        // GET: Amigos
        [HttpGet]
        public async Task<IActionResult> Perfil(string user)
        {
            var identityUser = await _userManager.GetUserAsync(User);

            if (user == identityUser.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            var postLista = new List<PostViewModel>();

            var posts = await _postServices.GetPostsByUserAsync(user);

           foreach (var post in posts)
           {
               var postViewModel = await ConverterIdToNameAndModelToViewModel(post);
           
               postLista.Add(postViewModel);
           }

           var StatusAmizade = 0;

           var usuarioPerfil = await _usuarioServices.GetByIdAsync(user);

            

           var usuarioLogado = await _usuarioServices.GetByIdAsync(identityUser.Id);

           var amizade = await _amigosServices.GetByIdAsync(usuarioLogado.IdentityUser,user);

            StatusAmizade = amizade?.StatusAmizade ?? 0;

           var perfilViewModel = new PerfilViewModel
           {
               IdentityUserLogado = identityUser.Id,
               IdentityUserPerfil = usuarioPerfil.IdentityUser,
               NomePerfil = usuarioPerfil.Nome + " " + usuarioPerfil.Sobrenome,
               FotoPerfil = usuarioPerfil.FotoPerfil,
               ListaPost = postLista,
               StatusAmizade = StatusAmizade
           };


            return View("Perfil", perfilViewModel);
        }


        // POST: Amigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("user")] string user)
        {
            if (ModelState.IsValid)
            {
                var identityUser = await _userManager.GetUserAsync(User);

                var amigosModel = new AmigosModel
                {
                    UserId1 = user,
                    UserId2 = identityUser.Id,
                    StatusAmizade = 1,
                    DataInicioAmizade = DateTime.Now
                };

                await _amigosServices.CreateAsync(amigosModel);

                return RedirectToAction(nameof(Perfil));
            }
            return View();
        }


        // POST: Amigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            return RedirectToAction(nameof(Perfil));
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
    }
}
