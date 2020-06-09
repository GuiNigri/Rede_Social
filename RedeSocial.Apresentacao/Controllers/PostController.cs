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
    public class PostController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPostServices _postServices;

        public PostController(UserManager<IdentityUser> userManager, IPostServices postServices)
        {
            _userManager = userManager;
            _postServices = postServices;
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile customFile, string message, int privacidade)
        {
            try
            {
                var userIdentity = await RecuperarIdentityUser();

                var postModel = new PostModel
                {
                    IdentityUser = userIdentity,
                    Privacidade = privacidade,
                    Texto = message,
                    UriImage = ConvertIFormFileToBase64(customFile),
                    DataPostagem = DateTime.Now
                    
                };

                await _postServices.CreateAsync(postModel);

                return RedirectToAction("Index","Home");
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
                var userIdentity = await RecuperarIdentityUser();

                var postModel = await _postServices.GetByidAsync(id);
                var userIdPost = postModel.IdentityUser;

                if(userIdentity == userIdPost)
                {

                    await _postServices.DeleteAsync(id,null);
                }

                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index","Home");
            }
        }

        private static string ConvertIFormFileToBase64(IFormFile image)
        {
            if(image != null)
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

        private async Task<string> RecuperarIdentityUser()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;

        }
    }
}
