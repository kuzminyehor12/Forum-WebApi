using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CommentModel>>> Get()
        {
            try
            {
                var comments = await _service.GetAllAsync();
                return Ok(comments);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> GetById(int id)
        {
            try
            {
                var comment = await _service.GetByIdAsync(id);
                return Ok(comment);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CommentModel value)
        {
            try
            {
                await _service.AddAsync(value);
                return CreatedAtAction(nameof(Add), new { id = value.Id }, value);
            }
            catch (ForumException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int Id, [FromBody] CommentModel value)
        {
            try
            {
                var findingComment = await _service.GetByIdAsync(Id);

                if (findingComment is null)
                    return NotFound();

                await _service.UpdateAsync(value);
                return Ok();
            }
            catch (ForumException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/complain")]
        public async Task<ActionResult> Complain(int id)
        {
            try
            {
                await _service.ComplainAboutCommentAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
