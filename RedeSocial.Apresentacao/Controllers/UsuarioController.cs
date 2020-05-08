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
using RedeSocial.Model.Entity;

namespace RedeSocial.Apresentacao.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {

        public UsuarioController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        // GET: Usuario/Details/5
        public async Task<UsuarioModel> Details(string id)
        {
            return await GetFromApiAsync($"api/usuario/{id}");
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


        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Sobrenome,Cpf,DataNascimento,FotoPerfil,IdentityUser")] UsuarioModel usuarioModel)
        {
            return await PutToApiAsync($"api/usuario/{id}", usuarioModel);
        }


        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            return await DeleteAsync($"api/usuario/{id}");
        }

        // Tentar implementar para proximo TP
        //
       // [AllowAnonymous]
       // [AcceptVerbs("GET")]
       // public async Task<IActionResult> CheckCpf(long cpfInput)
       // {
       //
       //     if (await GetFromApiCpfAsync($"api/usuario/{cpfInput}"))
       //     {
       //         return Json($"ISBN {cpfInput} já existe!");
       //     }
       //
       //     return Json(true);
       // }

    }
}
