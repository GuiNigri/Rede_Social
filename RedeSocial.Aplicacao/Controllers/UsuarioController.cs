using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RedeSocial.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<IEnumerable<UsuarioModel>> GetUsuarioModel()
        {
            return await _usuarioServices.GetAllAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioModel(string id)
        {
            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        //[HttpGet("{cpf}")]
        //public async Task<ActionResult<bool>> GetUsuarioModel(long cpf)
        //{
        //   var resposta = await _usuarioServices.GetByCpfAsync(cpf);
        //
        //   if (resposta)
        //   {
        //       return true;
        //   }
        //   else
        //   {
        //       return false;
        //   }
        //
        //}

        // PUT: api/Usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(string id, [Bind("Id,Nome,Sobrenome,Cpf,DataNascimento,FotoPerfil,IdentityUser")] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.IdentityUser)
            {
                return BadRequest();
            }

            try
            {
                await _usuarioServices.UpdateAsync(usuarioModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel([Bind("Id,Nome,Sobrenome,Cpf,DataNascimento,FotoPerfil,IdentityUser")] UsuarioModel usuarioModel)
        {
            try
            {
                await _usuarioServices.CreateAsync(usuarioModel);
            }
            catch (DbUpdateException)
            {
                if (UsuarioModelExists(usuarioModel.IdentityUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return base.Ok();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> DeleteUsuarioModel(string id)
        {
            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            await _usuarioServices.DeleteAsync(usuarioModel);

            return base.Ok();
        }

        private bool UsuarioModelExists(string id)
        {
            return _usuarioServices.UsuarioModelExists(id);
        }
    }
}
