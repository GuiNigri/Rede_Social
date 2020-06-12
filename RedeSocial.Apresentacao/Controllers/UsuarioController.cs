using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Apresentacao.Models.Usuarios;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Exceptions;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAmigosServices _amigosServices;

        public UsuarioController(IAmigosServices amigosServices, UserManager<IdentityUser> userManager, IUsuarioServices usuarioServices, IPostServices postServices,
            ICommentPostServices commentPostServices, ILikePostServices likePostServices)
            : base(userManager, usuarioServices, postServices, commentPostServices, amigosServices, likePostServices)
        {
            _usuarioServices = usuarioServices;
            _userManager = userManager;
            _amigosServices = amigosServices;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Index");
        }

        // GET: Amigos
        [HttpGet]
        public async Task<IActionResult> Perfil(string userPerfil)
        {
            var userIdLogado = await GetUserIdentityAsync();

            if (userPerfil == userIdLogado || userPerfil == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var statusAmizade = 0;
            var idAmizade = 0;

            var usuarioPerfil = await GetUsuarioModelAsync(userPerfil);

            var(postList,usuarioLogado,amigosList) = await HomeIndexAndUsuarioPerfilBase(userPerfil, userIdLogado);

            var amigosModel = await _amigosServices.GetByUsersAsync(usuarioLogado.IdentityUser, userPerfil);

            var amigosModelPerfil = await _amigosServices.GetListByUserAsync(usuarioPerfil.IdentityUser);

            if (amigosModel != null)
            {
                statusAmizade = amigosModel.StatusAmizade;
                idAmizade = amigosModel.Id;
            }


            return View("Perfil", new PerfilViewModel
                (userIdLogado,usuarioPerfil,postList,statusAmizade,amigosList,idAmizade,amigosModelPerfil.Count()));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sobrenome,Cpf,DataNascimento,Email,Password,ConfirmPassword,IdentityUser")] UsuarioCreateViewModel usuarioCreateViewModel, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = new IdentityUser { UserName = usuarioCreateViewModel.Email, Email = usuarioCreateViewModel.Email }; 
                    var result = await _userManager.CreateAsync(user, usuarioCreateViewModel.Password);

                    if (result.Succeeded)
                    { 
                        usuarioCreateViewModel.IdentityUser = user.Id;

                        var usuarioModel = ConvertViewModelToModel(usuarioCreateViewModel);
                        await _usuarioServices.CreateAsync(usuarioModel, ConvertIFormFileToBase64(ImageFile));

                        return RedirectToAction("Index","Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    

                }
                catch (ModelValidationExceptions e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var usuarioModel = await GetUsuarioModelAsync(userId);

            return View(new UsuarioEditViewModel(usuarioModel));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Cpf,FotoPerfil,DataNascimento,IdentityUser")] UsuarioEditViewModel usuarioEditViewModel, IFormFile ImageFile)
        {
            if (id <=0 )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = ConvertEditViewModelToModel(usuarioEditViewModel);

                    await _usuarioServices.UpdateAsync(usuarioModel,ConvertIFormFileToBase64(ImageFile));

                    return RedirectToAction("Index","Home");
                }
                catch (ModelValidationExceptions e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _usuarioServices.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }


            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task DeleteConfirmed(string id)
        {
            try
            {
                await _usuarioServices.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(string termoInputado)
        {
            var listaFiltrada = await _usuarioServices.GetFiltroAsync(termoInputado);

            var usuarioDetailsViewModel = new UsuarioDetailsViewModel
            {
                listaUsuarios = listaFiltrada
            };

            return View("Details", usuarioDetailsViewModel);

        }

        private static UsuarioModel ConvertViewModelToModel(UsuarioCreateViewModel usuarioCreateViewModel)
        {
            var usuarioModel = new UsuarioModel
            {
                FotoPerfil = null,
                IdentityUser = usuarioCreateViewModel.IdentityUser,
                Cpf = usuarioCreateViewModel.Cpf,
                DataNascimento = usuarioCreateViewModel.DataNascimento,
                Nome = usuarioCreateViewModel.Nome,
                Sobrenome = usuarioCreateViewModel.Sobrenome
          
            };

            return usuarioModel;
        }
        private static UsuarioModel ConvertEditViewModelToModel(UsuarioEditViewModel usuarioEditViewModel)
        {
            var usuarioModel = new UsuarioModel
            {
                IdentityUser = usuarioEditViewModel.IdentityUser,
                Cpf = usuarioEditViewModel.Cpf,
                DataNascimento = usuarioEditViewModel.DataNascimento,
                Nome = usuarioEditViewModel.Nome,
                Sobrenome = usuarioEditViewModel.Sobrenome,
                FotoPerfil = usuarioEditViewModel.FotoPerfil,
                Id = usuarioEditViewModel.Id
          
            };

            return usuarioModel;
        }

    }
}
