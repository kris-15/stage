using AtmEquityProject.Helper;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtmEquityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
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
        [Authorize]
        public async Task<IActionResult> Post([FromBody] User userObj)
        {
            userObj.Id = 0;
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        // Pour la modification des informations de user par son id
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        {
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }
    }
}
