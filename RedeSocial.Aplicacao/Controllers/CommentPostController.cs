using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Exceptions;
using RedeSocial.Model.Interfaces.Services;
using RedeSocial.Model.UoW;

namespace RedeSocial.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentPostController : ControllerBase
    {
        private readonly ICommentPostServices _commentPostServices;
        private readonly IUnitOfWork _unitOfWork;

        public CommentPostController(ICommentPostServices commentPostServices, IUnitOfWork unitOfWork)
        {
            _commentPostServices = commentPostServices;
            _unitOfWork = unitOfWork;
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
                _unitOfWork.BeginTransaction();
                await _commentPostServices.CreateAsync(commentPostModel);
                await _unitOfWork.CommitAsync();
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

        [HttpGet("commentbyuser/{userId}")]
        public async Task<ActionResult<IEnumerable<CommentPostModel>>> GetCommentPostModel(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var commentPostModel = await _commentPostServices.GetCommentByUserAsync(userId);

            if (commentPostModel == null)
            {
                return NotFound();
            }

            return commentPostModel.ToList();
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

            _unitOfWork.BeginTransaction();
            await _commentPostServices.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return base.Ok();
        }
    }
}
