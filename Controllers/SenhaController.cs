using Atendimento_API.Interfaces;
using Atendimento_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atendimento_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SenhaController : ControllerBase
    {
        private readonly ISenhaRepository _senhaService;

        public SenhaController(ISenhaRepository senhaService)
        {
            _senhaService = senhaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Senha>>> GetAll()
        {
            var comunicacoes = await _senhaService.GetAllAsync();
            if (comunicacoes == null)
            {
                return NotFound();
            }
            return Ok(comunicacoes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Senha>> GetById(int id)
        {
            var comunicacao = await _senhaService.GetByIdAsync(id);
            if (comunicacao == null)
            {
                return NotFound();
            }
            return Ok(comunicacao);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Senha>> Create(Senha senha)
        {
            try
            {
                await _senhaService.AddAsync(senha);
                return CreatedAtAction(nameof(GetById), new { id = senha.id }, senha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Senha senha)
        {
            if (id != senha.id)
            {
                return BadRequest();
            }

            try
            {
                await _senhaService.UpdateAsync(senha);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var senha = await _senhaService.GetByIdAsync(id);
            if (senha == null)
            {
                return NotFound();
            }

            try
            {
                await _senhaService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
