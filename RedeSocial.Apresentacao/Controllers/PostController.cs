using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Apresentacao.Models;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPostServices _postServices;

        public PostController(UserManager<IdentityUser> UserManager, IPostServices postServices)
        {
            _userManager = UserManager;
            _postServices = postServices;
        }

        public async Task<IEnumerable<PostModel>> GetAll()
        {
            return await _postServices.GetAll();
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile customFile, string message, int privacidade)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userIdentity = user.Id;

                var postModel = new PostModel
                {
                    IdentityUser = user.Id,
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
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private static string ConvertIFormFileToBase64(IFormFile image)
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
    }
}
