using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RedeSocial.Apresentacao.Areas.Identity.Pages.Account;
using RedeSocial.Data.Context;
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {

        public UsuarioController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IActionResult> Index()
        {
            return null;
        }

        // GET: Usuario/Details/5
        public async Task<UsuarioModel> Details(string id)
        {
            return await GetFromApiAsync($"api/usuario/{id}");
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Cpf,DataNascimento,FotoPerfil,IdentityUser")] UsuarioModel usuarioModel)
        {
            return await PostToApiAsync("api/usuario", usuarioModel);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            return null;
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Sobrenome,Cpf,DataNascimento,FotoPerfil,IdentityUser")] UsuarioModel usuarioModel)
        {
            return null;
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(string identity)
        {
            return null;
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string identity)
        {
            return null;
        }

    }
}
