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
    public class CommentPostController : ControllerBase
    {
        private readonly ICommentPostServices _commentPostServices;

        public CommentPostController(ICommentPostServices commentPostServices)
        {
            _commentPostServices = commentPostServices;
        }

        [HttpPost]
        public async Task<ActionResult<CommentPostModel>> PostCommentPostModel([Bind("Id, IdentityUser,PostModelId,Comment")] CommentPostModel commentPostModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _commentPostServices.CreateAsync(commentPostModel);
            }
            catch (ModelValidationExceptions e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }

            return base.Ok();
        }

        [HttpGet("list/{id}")]
        public async Task<ActionResult<IEnumerable<CommentPostModel>>> GetListCommentPostModel(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var commentPostModel = await _commentPostServices.GetPostByIdAsync(id);

            if (commentPostModel == null)
            {
                return NotFound();
            }

            return commentPostModel.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentPostModel>> GetCommentPostModel(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var commentPostModel = await _commentPostServices.GetByIdAsync(id);

            if (commentPostModel == null)
            {
                return NotFound();
            }

            return commentPostModel;
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> DeleteCommentPostModel(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var commentPostModel = await _commentPostServices.GetByIdAsync(id);

            if (commentPostModel == null)
            {
                return NotFound();
            }

            await _commentPostServices.DeleteAsync(id);

            return base.Ok();
        }
    }
}
