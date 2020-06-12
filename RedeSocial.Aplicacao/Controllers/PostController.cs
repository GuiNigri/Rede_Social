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
        private readonly ICommentPostServices _commentPostServices;

        public PostController(IPostServices postServices, ICommentPostServices commentPostServices)
        {
            _postServices = postServices;
            _commentPostServices = commentPostServices;
        }

        // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPost]
        public async Task<ActionResult<PostModel>> PostPostModel([Bind("Id, IdentityUser,Texto,UriImage,Privacidade")] PostModel postModel)
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
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostModel()
        {
            var posts = await _postServices.GetAllAsync();
            return posts.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostModel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var posts = await _postServices.GetPostsByUserAsync(id);

            return posts.ToList();
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<PostModel>> GetPostModel(int id)
        {
            if (id <=0)
            {
                return NotFound();
            }
            var post = await _postServices.GetByIdAsync(id);

            if(post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostModel>> DeletePostModel(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var post = await _postServices.GetByIdAsync(id);

            if(post == null)
            {
                return NotFound();
            }

            await _postServices.DeleteAsync(id,post.UriImage);

            return post;
        }



    }
}
