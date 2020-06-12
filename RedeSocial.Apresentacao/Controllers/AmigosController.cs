using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    public class AmigosController : ControllerBase
    {
        private readonly IAmigosServices _amigosServices;


        public AmigosController(IAmigosServices amigosServices, UserManager<IdentityUser> userManager, IUsuarioServices usuarioServices, IPostServices postServices, ICommentPostServices commentPostServices) 
            : base(userManager, usuarioServices, postServices, commentPostServices, amigosServices)
        {
            _amigosServices = amigosServices;
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
                var userId = await GetUserIdentityAsync();

                var amigosModel = new AmigosModel
                {
                    UserIdSolicitado = user,
                    UserIdSolicitante = userId,
                    StatusAmizade = 1,
                    DataInicioAmizade = DateTime.Now
                };

                await _amigosServices.CreateAsync(amigosModel);

                return RedirectToAction("Perfil","Usuario", new { userPerfil = user});
            }
            return RedirectToAction("Perfil", "Usuario");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("id")] int id)
        {
            if (ModelState.IsValid)
            {
                var amigoModel = await _amigosServices.GetByIdAsync(id);
                amigoModel.StatusAmizade = 2;

                await _amigosServices.UpdateAsync(amigoModel);

                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Index", "Home");
        }


        // POST: Amigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("id")] int id)
        {
            await _amigosServices.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }

    }
}
