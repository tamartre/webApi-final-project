
using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using Tamar_Sheva_Project;
using Zxcvbn;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
           User Result = await _userService.Get(id);
            if (Result != null)
            {
                return Ok(Result);
            }
            return BadRequest();

        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto user)
        {
            User newUser = _mapper.Map<UserDto,User>(user);

            User userResualt = await _userService.Register(newUser);
            if (userResualt != null)
            {
                return Ok(userResualt);
            }
            else if (userResualt == null)
            {
                return NoContent();
            }

            return BadRequest();

        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User userResualt = await _userService.Login(user);
            if (userResualt != null)
            {
                return Ok(userResualt);
            }

            return Unauthorized();

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] User user)
        {
            User u = await _userService.Update(id, user);
            if (u != null)
                return Ok(u);
            return NoContent();

        }

        [HttpPost("password")]
        public ActionResult Password([FromBody] object password)
        {
            var res = Zxcvbn.Core.EvaluatePassword(password.ToString());
            if (res.Score >= 2)

                return Ok(res.Score);

            return Accepted(res.Score);

        }
    }
}
