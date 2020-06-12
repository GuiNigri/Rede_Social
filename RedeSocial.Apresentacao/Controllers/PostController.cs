using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;
        private readonly ICommentPostServices _commentPostServices;

        public PostController(IAmigosServices amigosServices, UserManager<IdentityUser> userManager, IUsuarioServices usuarioServices, IPostServices postServices, ICommentPostServices commentPostServices)
            : base(userManager, usuarioServices, postServices, commentPostServices, amigosServices)
        {
            _postServices = postServices;
            _commentPostServices = commentPostServices;
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile customFile, string message, int privacidade)
        {
            try
            {
                var userId = await GetUserIdentityAsync();

                var postModel = new PostModel
                {
                    IdentityUser = userId,
                    Privacidade = privacidade,
                    Texto = message,
                    UriImage = ConvertIFormFileToBase64(customFile),
                    DataPostagem = DateTime.Now

                };

                await _postServices.CreateAsync(postModel);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: PostController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var userId = await GetUserIdentityAsync();

                var postModel = await _postServices.GetByIdAsync(id);

                if (userId == postModel.IdentityUser)
                {

                    await _postServices.DeleteAsync(id, null);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string comment, int idPost)
        {
            try
            {
                var userId = await GetUserIdentityAsync();

                var commentPostModel = new CommentPostModel
                {
                    Comment = comment,
                    PostModelId = idPost,
                    IdentityUser = userId,
                    DataDoComment = DateTime.Now
                };

               await _commentPostServices.CreateAsync(commentPostModel);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var userId = await GetUserIdentityAsync();

                var commentPostModel = await _commentPostServices.GetByIdAsync(id);

                if (userId == commentPostModel.IdentityUser)
                {

                    await _commentPostServices.DeleteAsync(id);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }
}
