using AtmEquityProject.Dto;
using AtmEquityProject.Helper;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AtmEquityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        //Authentification
        [HttpPost("authentification")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username ou mot de passe incorrect" });

            return Ok(response);
        }
        //Pour l'enregistrement d'un nouvel user
        [HttpPost]
        [ProducesResponseType(201)]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] UserDto userObj)
        {
            var userMapped = _mapper.Map<User>(userObj);
            return Ok(await _userService.AddAndUpdateUser(userMapped));
        }

        // Pour la modification des informations de user par son id
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        {
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        [HttpGet("connected")]
        public IActionResult GetData()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userInfo = _userService.GetUserInfo(token);
            if (userInfo == null)
                return NotFound();
            // Utilisation des informations de l'utilisateur
            return Ok(userInfo);
        }
    }
}
