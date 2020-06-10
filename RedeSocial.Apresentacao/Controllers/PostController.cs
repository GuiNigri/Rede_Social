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

        public PostController(IAmigosServices amigosServices, UserManager<IdentityUser> userManager, IUsuarioServices usuarioServices, IPostServices postServices) : base(userManager, usuarioServices, postServices, amigosServices)
        {
            _postServices = postServices;
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

                var postModel = await _postServices.GetByidAsync(id);

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

    }
}
