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
    //[Authorize]
    public class AtmController : Controller
    {
        private readonly IAtm? _atmRepository;
        private readonly IMapper _mapper;
        public AtmController(IAtm atmRepository, IMapper mapper)
        {
            _atmRepository = atmRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Atm>))]
        public IActionResult GetAtms()
        {
            var atms = _mapper.Map<List<AtmDto>>(_atmRepository.GetAtms());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(atms);
        }
        [HttpGet("{accountNum}")]
        [ProducesResponseType(200, Type = typeof(Atm))]
        [ProducesResponseType(400)]
        public IActionResult GetAtm(int accountNum)
        {
            if (!_atmRepository.AtmExists(accountNum))
                return NotFound();
            var atm = _mapper.Map<AtmDto>(_atmRepository.GetAtm(accountNum));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(atm);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAtm([FromBody] AtmDto atmData)
        {
            if (atmData == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            var atmAlreadyExists = _atmRepository.GetAtms()
                .Where(c => c.NumCompte == atmData.NumCompte)
                .FirstOrDefault();
            if (atmAlreadyExists != null)
            {
                ModelState.AddModelError("", "Ce numéro de compte existe déjà");
                return StatusCode(422, ModelState);
            }
            var atmToCreate = _mapper.Map<Atm>(atmData);
            if (!_atmRepository.CreateAtm(atmToCreate))
            {
                ModelState.AddModelError("", "Problème lors de l'enregistrement");
                return StatusCode(500, ModelState);
            }
            return Ok(new {message = "Enregistrement de l'ATM effectué avec succès" });
        }
        [HttpPut("{atmId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAtm(int atmId, [FromBody] AtmDto atmData)
        {
            if (atmData == null)
                return BadRequest(ModelState);
            if (atmId != atmData.Id)
                return BadRequest(ModelState);
            if (!_atmRepository.AtmExistsWithId(atmId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var atmMap = _mapper.Map<Atm>(atmData);
            if (!_atmRepository.UpdateAtm(atmMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("delete/{atmId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAtm(int atmId)
        {
            if (!_atmRepository.CheckAtmById(atmId))
            {
                return NotFound();
            }
            var atmToDelete = _atmRepository.GetAtmById(atmId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_atmRepository.DeleteAtm(atmToDelete))
            {
                ModelState.AddModelError("", "L'opération n'a pas abouti");
            }
            return NoContent();
        }


    }
}
