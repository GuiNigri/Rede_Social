using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        // GET: Veiculo
        public async Task<List<UsuarioViewModel>> GetAllAsync()
        {
            var listaUsuarios = await _usuarioServices.GetAllAsync();

            var listaViewModelUsuario = listaUsuarios.Select(ConvertModelToViewModel).ToList();

            return listaViewModelUsuario;

        }

        public async Task<UsuarioViewModel> GetByIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            if (usuarioModel == null)
            {
                return null;
            }

            var usuarioViewModel = ConvertModelToViewModel(usuarioModel);

            return usuarioViewModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Create()
        {


            return View("Index");
        }

        // POST: Veiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            var usuarioEditViewModel = ConvertModelToEditViewModel(usuarioModel);

            return View(usuarioEditViewModel);

        }

        // POST: Veiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Modelo,Cor,Ano,MarcaModelId,ImagemUri")] UsuarioViewModel usuarioViewModel, string newImage)
        {
            if (id != usuarioViewModel.IdentityUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = ConvertViewModelToModel(usuarioViewModel);
                    await _usuarioServices.UpdateAsync(usuarioModel);

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

        // POST: Veiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(string id)
        {
            try
            {
                await _usuarioServices.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        private static UsuarioViewModel ConvertModelToViewModel(UsuarioModel usuarioModel)
        {
            var usuarioViewModel = new UsuarioViewModel
            {
                IdentityUser = usuarioModel.IdentityUser,
                Cpf = usuarioModel.Cpf,
                DataNascimento = usuarioModel.DataNascimento,
                Nome = usuarioModel.Nome,
                Sobrenome = usuarioModel.Sobrenome,
                FotoPerfil = usuarioModel.FotoPerfil
          
            };

            return usuarioViewModel;
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
