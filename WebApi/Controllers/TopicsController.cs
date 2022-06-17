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
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _service;
        public TopicsController(ITopicService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TopicModel>>> Get()
        {
            try
            {
                var topics = await _service.GetAllAsync();
                return Ok(topics);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("tags")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetTags()
        {
            try
            {
                var tags = await _service.GetTags();
                return Ok(tags);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    [HttpGet("{tags}&{publicationDate}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TopicModel>>> GetByFilter(IEnumerable<int> tags, DateTime? publicationDate)
        {
            try
            {
                FilterModel filter = new FilterModel
                {
                    TagIds = tags,
                    PublicationDate = publicationDate
                };

                var topics = await _service.GetByFilterAsync(filter);
                return Ok(topics);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TopicModel>> GetById(int id)
        {
            try
            {
                TopicModel topic = await _service.GetByIdAsync(id);
                return Ok(topic);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("sort/likes")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TopicModel>>> GetSortedByLikes()
        {
            try
            {
                var topics = await _service.SortByLikes();
                return Ok(topics);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("sort/date")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TopicModel>>> GetSortedByDate()
        {
            try
            {
                var topics = await _service.SortByPublicationDate();
                return Ok(topics);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TopicModel value)
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

        [HttpPost("tag")]
        public async Task<ActionResult> AddTag([FromBody] TopicTagModel tag)
        {
            try
            {
                await _service.AddTag(tag);
                return CreatedAtAction(nameof(AddTag), new { CompositeKey = Tuple.Create(tag.TagId, tag.TopicId) }, tag);
            }
            catch (ForumException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int Id, [FromBody] TopicModel value)
        {
            try
            {
                var findingTopic = await _service.GetByIdAsync(Id);

                if (findingTopic is null)
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
                await _service.ComplainAboutTopicAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
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

        [HttpDelete("tag")]
        public async Task<ActionResult> RemoveTag([FromBody] TopicTagModel tag)
        {
            try
            {
                await _service.RemoveTag(tag);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
