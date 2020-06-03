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

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
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
        public async Task<IActionResult> Create([Bind("IdentityUser")] string identityUser)
        {
            var usuarioViewModel = new UsuarioViewModel
            {
                IdentityUser = identityUser
            };

            return View("Index", usuarioViewModel);
        }

        // POST: Veiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> Create([Bind("Nome ,Sobrenome,Cpf,DataNascimento,IdentityUser")] UsuarioViewModel usuarioViewModel, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imageBase64;
                    using (var ms = new MemoryStream())
                    {
                        ImageFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        imageBase64 = Convert.ToBase64String(fileBytes);
                    }

                    var usuarioModel = ConvertViewModelToModel(usuarioViewModel);
                    await _usuarioServices.CreateAsync(usuarioModel, imageBase64);
                    return true;

                }
                catch (ModelValidationExceptions e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return false;
                }
            }

            return false;
        }

        // POST: Veiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> Edit(string id, [Bind("Id,Modelo,Cor,Ano,MarcaModelId,ImagemUri")] UsuarioViewModel usuarioViewModel, string newImage)
        {
            if (id != usuarioViewModel.IdentityUser)
            {
                return false;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = ConvertViewModelToModel(usuarioViewModel);
                    await _usuarioServices.UpdateAsync(usuarioModel);
                    return true;
                }
                catch (ModelValidationExceptions e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);

                    return false;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _usuarioServices.GetByIdAsync(id) == null)
                    {
                        return false;
                    }
                    
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }


            return false;
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
                Sobrenome = usuarioModel.Sobrenome
          
            };

            return usuarioViewModel;
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

    }
}
