using Atendimento_API.Interfaces;
using Atendimento_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atendimento_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioRepository _relatorioService;

        public RelatorioController(IRelatorioRepository relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Relatorio>>> GetAll()
        {
            var comunicacoes = await _relatorioService.GetAllAsync();
            if (comunicacoes == null)
            {
                return NotFound();
            }
            return Ok(comunicacoes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Relatorio>> GetById(int id)
        {
            var comunicacao = await _relatorioService.GetByIdAsync(id);
            if (comunicacao == null)
            {
                return NotFound();
            }
            return Ok(comunicacao);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Relatorio>> Create(Relatorio relatorio)
        {
            try
            {
                await _relatorioService.AddAsync(relatorio);
                return CreatedAtAction(nameof(GetById), new { id = relatorio.id }, relatorio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Relatorio relatorio)
        {
            if (id != relatorio.id)
            {
                return BadRequest();
            }

            try
            {
                await _relatorioService.UpdateAsync(relatorio);
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
            var relatorio = await _relatorioService.GetByIdAsync(id);
            if (relatorio == null)
            {
                return NotFound();
            }

            try
            {
                await _relatorioService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
