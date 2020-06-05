using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Apresentacao.Models;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Exceptions;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Apresentacao.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioController(IUsuarioServices usuarioServices,UserManager<IdentityUser> userManager)
        {
            _usuarioServices = usuarioServices;
            _userManager = userManager;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sobrenome,Cpf,DataNascimento,Email,Password,ConfirmPassword,IdentityUser")] UsuarioViewModel usuarioViewModel, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = new IdentityUser { UserName = usuarioViewModel.Email, Email = usuarioViewModel.Email }; 
                    var result = await _userManager.CreateAsync(user, usuarioViewModel.Password);

                    if (result.Succeeded)
                    { 
                        usuarioViewModel.IdentityUser = user.Id;

                        var usuarioModel = ConvertViewModelToModel(usuarioViewModel);
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
        public async Task<IActionResult> Edit(string id)
        {
            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            var usuarioEditViewModel = ConvertModelToEditViewModel(usuarioModel);

            return View(usuarioEditViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nome,Sobrenome,Cpf,FotoPerfil,DataNascimento,IdentityUser")] UsuarioEditViewModel usuarioEditViewModel, IFormFile ImageFile)
        {
            if (id != usuarioEditViewModel.IdentityUser)
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

        private static UsuarioEditViewModel ConvertModelToEditViewModel(UsuarioModel usuarioModel)
        {
            var usuarioEditViewModel = new UsuarioEditViewModel
            {
                IdentityUser = usuarioModel.IdentityUser,
                Cpf = usuarioModel.Cpf,
                DataNascimento = usuarioModel.DataNascimento,
                Nome = usuarioModel.Nome,
                Sobrenome = usuarioModel.Sobrenome,
                FotoPerfil = usuarioModel.FotoPerfil
          
            };

            return usuarioEditViewModel;
        }

        private static UsuarioModel ConvertViewModelToModel(UsuarioViewModel usuarioViewModel)
        {
            var usuarioModel = new UsuarioModel
            {
                FotoPerfil = null,
                IdentityUser = usuarioViewModel.IdentityUser,
                Cpf = usuarioViewModel.Cpf,
                DataNascimento = usuarioViewModel.DataNascimento,
                Nome = usuarioViewModel.Nome,
                Sobrenome = usuarioViewModel.Sobrenome
          
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
                FotoPerfil = usuarioEditViewModel.FotoPerfil
          
            };

            return usuarioModel;
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

    }
}
