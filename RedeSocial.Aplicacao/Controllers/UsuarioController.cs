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
using RedeSocial.Model.Exceptions;

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

            if (id == null)
            {
                return NotFound();
            }

            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        [HttpGet("search/{termoInputado}")]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetSearchUsuario(string termoInputado)
        {

            if (termoInputado == null)
            {
                return NotFound();
            }

            var usuarioModel = await _usuarioServices.GetFiltroAsync(termoInputado);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel.ToList();
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(string id, [Bind("UsuarioModel, ImageBase64")] CreateAndUpdateHttpUsuarioModel createUsuarioModel)
        {
            var usuarioModel = createUsuarioModel.UsuarioModel;
            var imageBase64 = createUsuarioModel.ImageBase64;

            if (id != usuarioModel.IdentityUser)
            {
                return BadRequest();
            }

            try
            {

                await _usuarioServices.UpdateAsync(usuarioModel,imageBase64);
            }
            catch (ModelValidationExceptions e)
            {

                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);

            } 
            catch (System.Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel([Bind("UsuarioModel, ImageBase64")] CreateAndUpdateHttpUsuarioModel createUsuarioModel)
        {

            var usuarioModel = createUsuarioModel.UsuarioModel;
            var imageBase64 = createUsuarioModel.ImageBase64;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioServices.CreateAsync(usuarioModel,imageBase64);
            }
            catch (ModelValidationExceptions e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }

            return base.Ok();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> DeleteUsuarioModel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuarioModel = await _usuarioServices.GetByIdAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            await _usuarioServices.DeleteAsync(id);

            return base.Ok();
        }

    }
}
