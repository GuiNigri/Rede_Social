using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Exceptions;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }
                // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostModel>> PostUsuarioModel([Bind("Id, IdentityUser,Texto,UriImage,Privacidade")] PostModel postModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _postServices.CreateAsync(postModel);
            }
            catch (ModelValidationExceptions e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }

            return base.Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<PostModel>> GetPostModel()
        {
            return await _postServices.GetAll();
        }
    }
}
