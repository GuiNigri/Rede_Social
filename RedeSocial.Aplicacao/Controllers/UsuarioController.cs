using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Data.Context;
using RedeSocial.Model.Entity;

namespace RedeSocial.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly RedeSocialContext _context;

        public UsuarioController(RedeSocialContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarioModel()
        {
            return await _context.UsuarioModel.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioModel(string id)
        {
            var usuarioModel = await _context.UsuarioModel.FindAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel([Bind("Id,Nome,Sobrenome,Cpf,DataNascimento,FotoPerfil,IdentityUser")] string id, UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.IdentityUser)
            {
                return BadRequest();
            }

            _context.Entry(usuarioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            _context.UsuarioModel.Add(usuarioModel);
            try
            {
                await _context.SaveChangesAsync();
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

            return CreatedAtAction("GetUsuarioModel", new { id = usuarioModel.IdentityUser }, usuarioModel);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> DeleteUsuarioModel(string id)
        {
            var usuarioModel = await _context.UsuarioModel.FindAsync(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            _context.UsuarioModel.Remove(usuarioModel);
            await _context.SaveChangesAsync();

            return usuarioModel;
        }

        private bool UsuarioModelExists(string id)
        {
            return _context.UsuarioModel.Any(e => e.IdentityUser == id);
        }
    }
}
