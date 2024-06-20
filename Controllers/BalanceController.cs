using AtmEquityProject.Dto;
using AtmEquityProject.Helper;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;
using AtmEquityProject.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AtmEquityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BalanceController : Controller
    {
        public readonly IBalance _balanceRepository;
        public readonly IAtm _atmRepository;
        public readonly IMapper _mapper;
        public BalanceController(IBalance balanceRepository, IAtm atmRepository, IMapper mapper)
        {
            _balanceRepository = balanceRepository;
            _atmRepository = atmRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Balance>))]
        public IActionResult GetBalances()
        {
            var balances = _mapper.Map<List<BalanceDto>>(_balanceRepository.GetBalances());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(balances);
        }
        [HttpGet("{idBalance}")]
        [ProducesResponseType(200, Type = typeof(Balance))]
        [ProducesResponseType(400)]
        public IActionResult GetBalance(int idBalance)
        {
            if (!_balanceRepository.BalanceExists(idBalance))
                return NotFound();
            var balance = _mapper.Map<BalanceDto>(_balanceRepository.GetBalance(idBalance));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(balance);
        }
        [HttpPut("{balanceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBalance(int balanceId, [FromBody] BalanceDto balanceData)
        {
            if (balanceData == null)
                return BadRequest(ModelState);
            if (balanceId != balanceData.Id)
                return BadRequest(ModelState);
            if (!_balanceRepository.BalanceExists(balanceId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var balanceMap = _mapper.Map<Balance>(balanceData);
            if (!_balanceRepository.UpdateBalance(balanceMap))
            {
                ModelState.AddModelError("", "Problème lors de l'enregstrement");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("delete/{idBalance}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBalance(int idBalance)
        {
            if (!_balanceRepository.BalanceExists(idBalance))
            {
                return NotFound();
            }
            var balanceToDelete = _balanceRepository.GetBalance(idBalance);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_balanceRepository.DeleteBalance(balanceToDelete))
            {
                ModelState.AddModelError("", "L'opération n'a pas abouti");
            }
            return NoContent();
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAtm([FromQuery] int idAtm, [FromBody] BalanceDto balanceData)
        {
            if (balanceData == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            var balanceToCreate = _mapper.Map<Balance>(balanceData);

            balanceToCreate.IdAtm = _atmRepository.GetAtmById(idAtm);

            if (!_balanceRepository.CreateBalance(balanceToCreate))
            {
                ModelState.AddModelError("", "Problème lors de l'enregistrement");
                return StatusCode(500, ModelState);
            }
            return Ok(new { message = "Ajout de la balance effectué avec succès" });

        }
    }
}
