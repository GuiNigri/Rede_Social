using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Exceptions;
using RedeSocial.Model.Interfaces.Services;

namespace RedeSocial.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikePostController : ControllerBase
    {
        private readonly ILikePostServices _likePostServices;

        public LikePostController(ILikePostServices likePostServices)
        {
            _likePostServices = likePostServices;
        }
        [HttpPost]
        public async Task<ActionResult<LikePostModel>> PostLikePostModel([Bind("Id, IdentityUser,PostModelId")] LikePostModel likePostModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _likePostServices.CreateAsync(likePostModel);
            }
            catch (ModelValidationExceptions e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }

            return base.Ok();
        }

        [HttpGet("list/{id}")]
        public async Task<ActionResult<IEnumerable<LikePostModel>>> GetListLikePostModel(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var likePostModel = await _likePostServices.GetPostByIdAsync(id);

            if (likePostModel == null)
            {
                return NotFound();
            }

            return likePostModel.ToList();
        }

        [HttpGet("{userId}/{idPost}")]
        public async Task<ActionResult<LikePostModel>> GetListLikePostModel(string userId, int idPost)
        {
            if (idPost <= 0 || userId == null)
            {
                return NotFound();
            }

            var likePostModel = await _likePostServices.GetStatusAsync(userId,idPost);

            return likePostModel;
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LikePostModel>> DeleteLikePostModel(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var likePostModel = await _likePostServices.GetByIdAsync(id);

            if (likePostModel == null)
            {
                return NotFound();
            }

            await _likePostServices.DeleteAsync(id);

            return base.Ok();
        }
    }
}
