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
    public class ResponsesController : ControllerBase
    {
        private readonly IResponseService _service;
        public ResponsesController(IResponseService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResponseModel>>> Get()
        {
            try
            {
                var responses = await _service.GetAllAsync();
                return Ok(responses);
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
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("/sort/activity")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResponseModel>>> GetSortedByLikes()
        {
            try
            {
                var responses = await _service.SortByLikes();
                return Ok(responses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("/sort/publicationDate")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResponseModel>>> GetSortedByDate()
        {
            try
            {
                var responses = await _service.SortByPublicationDate();
                return Ok(responses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ResponseModel value)
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
        public async Task<ActionResult> Update(int Id, [FromBody] ResponseModel value)
        {
            try
            {
                var findingResponse = await _service.GetByIdAsync(Id);

                if (findingResponse is null)
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
                await _service.ComplainAboutResponseAsync(id);
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
