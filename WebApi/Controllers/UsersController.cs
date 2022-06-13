using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UserManager<UserCredentials> _userManager;
        private readonly SignInManager<UserCredentials> _signInManager;
        public IConfiguration Configuration { get; }
        public UsersController(IUserService service, 
            UserManager<UserCredentials> userManager, 
            SignInManager<UserCredentials> signInManager,
            IConfiguration config)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = config;
        }

        [HttpGet]
        [AuthorizeWithRole(Roles.Moderator)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            try
            {
                var users = await _service.GetAllAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            try
            {
                var user = await _service.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/topics")]
        public async Task<ActionResult<IEnumerable<TopicModel>>> GetTopicsByUserId(int id)
        {
            try
            {
                var topics = await _service.GetTopicsByUserIdAsync(id);
                return Ok(topics);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/responses")]
        public async Task<ActionResult<IEnumerable<ResponseModel>>> GetResponsesByUserId(int id)
        {
            try
            {
                var responses = await _service.GetResponsesByUserIdAsync(id);
                return Ok(responses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/comments")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetCommentsByUserId(int id)
        {
            try
            {
                var comments = await _service.GetCommentsByUserIdAsync(id);
                return Ok(comments);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("/response/like/add")]
        public async Task<ActionResult> LikeResponse([FromBody] LikerResponseModel model)
        {
            try
            {
                await _service.LikeResponseAsync(model);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("/topic/like/add")]
        public async Task<ActionResult> LikeTopic([FromBody] LikerTopicModel model)
        {
            try
            {
                await _service.LikeTopicAsync(model);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("/response/like/remove")]
        public async Task<ActionResult> RemoveLikeResponse([FromBody] LikerResponseModel model)
        {
            try
            {
                await _service.RemoveLikeResponseAsync(model);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("/topic/like/remove")]
        public async Task<ActionResult> RemoveLikeTopic([FromBody] LikerTopicModel model)
        {
            try
            {
                await _service.RemoveLikeTopicAsync(model);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("/token")]
        public async Task<ActionResult> GetToken([FromBody] AuthenticationModel model)
        {
            var credentials = await _service.GetCredentials();
            var user = credentials.FirstOrDefault(c => c.Email == model.Email);

            var users = await _service.GetAllAsync();
            var userDetail = users.FirstOrDefault(u => u.UserCredentialsId == user.Id);

            if(user is null)
            {
                return Unauthorized();
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (signInResult.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Role, userDetail.ToString())
                    }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])), 
                        SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(tokenString);
            }

            return Ok("Try again!");
        }

        [HttpPost("/register")]
        public async Task<ActionResult> Register([FromBody] UserModel model)
        {
            UserCredentials credentials = new UserCredentials
            {
                Email = model.Email,
                UserName = model.Nickname,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(credentials, model.Password);

            if (result.Succeeded)
            {
                try
                {
                    await _service.AddAsync(model);
                    return Ok(new { Result = "Success Registered" });
                }
                catch (ForumException)
                {
                    return BadRequest();
                }
            }

            StringBuilder errorBuilder = new StringBuilder();
            foreach (var item in result.Errors)
            {
                errorBuilder.Append(item.Description + "\r\n");
            }

            return Ok(new { Result = $"Register Fail: {errorBuilder.ToString()}" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int Id, [FromBody] UserModel value)
        {
            try
            {
                var findingUser = await _service.GetByIdAsync(Id);

                if (findingUser is null)
                    return NotFound();

                await _service.UpdateAsync(value);
                return Ok();
            }
            catch (ForumException)
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
