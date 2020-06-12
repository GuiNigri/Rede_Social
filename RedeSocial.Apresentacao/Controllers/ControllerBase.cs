using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RedeSocial.Apresentacao.Models;
using RedeSocial.Apresentacao.Models.Amigos;
using RedeSocial.Apresentacao.Models.Posts;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IPostServices _postServices;
        private readonly ICommentPostServices _commentPostServices;
        private readonly IAmigosServices _amigosServices;

        public ControllerBase(UserManager<IdentityUser> userManager, IUsuarioServices usuarioServices, IPostServices postServices, ICommentPostServices commentPostServices, IAmigosServices amigosServices)
        {
            _userManager = userManager;
            _usuarioServices = usuarioServices;
            _postServices = postServices;
            _commentPostServices = commentPostServices;
            _amigosServices = amigosServices;
        }


        public async Task<string> GetUserIdentityAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        public async Task<UsuarioModel> GetUsuarioModelAsync(string userId)
        {
            return await _usuarioServices.GetByIdAsync(userId);
        }

        public async Task<List<PostViewModel>> GetPostsAsync(string userId)
        {
            IEnumerable<PostModel> posts;

            var postLista = new List<PostViewModel>();
            
            if (userId == null)
            {
                posts = await _postServices.GetAllAsync();
            }
            else
            {
                posts = await _postServices.GetPostsByUserAsync(userId);
            }
            

            foreach (var post in posts)
            {
                var postViewModel = await ConverterIdToNameAndModelToViewModel(post);

                postLista.Add(postViewModel);
            }

            return postLista;
        }

        private async Task<PostViewModel> ConverterIdToNameAndModelToViewModel(PostModel postModel)
        {
            var usuario = await GetUsuarioModelAsync(postModel.IdentityUser);

            var listaComentarios = await GetListCommentByIdPost(postModel.Id);

           var ( tempo, formatoDeTempo) = DefinirTempoPostagem(postModel.DataPostagem);

            return new PostViewModel(postModel, usuario, tempo, formatoDeTempo, listaComentarios);
        }
        public async Task<AmigosViewModel> ConverterIdToNameAndModelToViewModel(AmigosModel amigosModel)
        {
            UsuarioModel usuario;
            var userId = await GetUserIdentityAsync();

            if (userId == amigosModel.UserIdSolicitado)
            {
                usuario = await GetUsuarioModelAsync(amigosModel.UserIdSolicitante);
            }
            else
            {
                usuario = await GetUsuarioModelAsync(amigosModel.UserIdSolicitado);
            }

            return new AmigosViewModel(amigosModel, usuario);
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

        public async Task<IEnumerable<AmigosViewModel>> GetAmigosAsync()
        {
            var listaFormatada = new List<AmigosViewModel>();

            var user = await _userManager.GetUserAsync(User);

            var amigos = await _amigosServices.GetListByUserAsync(user.Id);

            foreach (var amigo in amigos)
            {
                var amigosViewModel = await ConverterIdToNameAndModelToViewModel(amigo);

                listaFormatada.Add(amigosViewModel);
            }

            return listaFormatada;

        }

        public string ConvertIFormFileToBase64(IFormFile image)
        {
            if (image != null)
            {
                string imageBase64;
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    imageBase64 = Convert.ToBase64String(fileBytes);
                }

                return imageBase64;
            }
            return null;
        }

        public async Task<(List<PostViewModel>,UsuarioModel,IEnumerable<AmigosViewModel>)> HomeIndexAndUsuarioPerfilBase(string userPerfil, string userIdLogado)
        {
            var posts = await GetPostsAsync(userPerfil);

            var usuarioLogado = await GetUsuarioModelAsync(userIdLogado);
            var listaDeAmigos = await GetAmigosAsync();

            return (posts, usuarioLogado, listaDeAmigos);
        }

        public async Task<IEnumerable<CommentPostViewModel>> GetListCommentByIdPost(int idPost)
        {
            var commentList = new List<CommentPostViewModel>();
            var listaComentarios = await _commentPostServices.GetPostByIdAsync(idPost);

            foreach (var comentario in listaComentarios)
            {
                var usuarioModel = await _usuarioServices.GetByIdAsync(comentario.IdentityUser);
                var (tempo, formato) = DefinirTempoPostagem(comentario.DataDoComment);

                commentList.Add(new CommentPostViewModel(comentario,usuarioModel,tempo,formato));
            }

            return commentList;
        }
    }
}
