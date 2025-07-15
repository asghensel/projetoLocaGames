using Microsoft.AspNetCore.Mvc;
using Senac.GerenciamentoJogo.Domain.Dtos.Request;
using Senac.GerenciamentoJogo.Domain.Dtos.Response;
using Senac.GerenciamentoJogo.Domain.Services;

namespace Senac.GerenciamentoJogo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogoController : Controller
    {
        private readonly IJogoService _jogoService;

        public JogoController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet("Obter-Todos")]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var jogosResponse = await _jogoService.ObterTodos();
                return Ok(jogosResponse);
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return NotFound(response);
            }
        }

        [HttpGet("{id}Obter-Detalhes")]
        public async Task<IActionResult> ObterDetalhesJogos([FromRoute] long id)
        {
            try
            {
                var jogoDetalhadoResponse = await _jogoService.ObterDetalhesJogo(id);
                if (jogoDetalhadoResponse == null)
                {
                    return NotFound(new ErroResponse { Mensagem = "Jogo não encontrado." });
                }
                return Ok(jogoDetalhadoResponse);
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return NotFound(response);

            }
        }

        [HttpPost("Cadastrar-Jogo")]
        public async Task<IActionResult> CadastrarJogo([FromBody] CadastrarRequest cadastrarRequest)
        {
            try
            {
                var cadastrarResponse = await _jogoService.CadastrarJogo(cadastrarRequest);
                return Ok(cadastrarResponse);
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }


        }

        [HttpDelete("{id}Deletar-Jogo")]
        public async Task<IActionResult> DeletarJogo([FromRoute] long id)
        {
            try
            {
                await _jogoService.DeletarJogo(id);
                return Ok(new { Mensagem = "Jogo deletado com sucesso." });
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpPut("{id}Atualizar-Jogo")]
        public async Task<IActionResult> AtualizarJogo([FromRoute] long id, [FromBody] AtualizarRequest atualizarRequest)
        {
            try
            {
                await _jogoService.AtualizarJogo(id, atualizarRequest);
                return Ok(new { Mensagem = "Jogo atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }

        }

        [HttpPut("{id}Alugar-Jogo")]
        public async Task<IActionResult> AlugarJogo([FromRoute] long id, [FromBody] AlugarRequest alugarRequest)
        {
            try
            {
                await _jogoService.AlugarJogo(id, alugarRequest);
                return Ok(new { Mensagem = "Jogo Alugar com sucesso." });
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }

        }

        [HttpPut("{id}Devolver-Jogo")]
        public async Task<IActionResult> DevolverJogo([FromRoute] long id, [FromBody] DevolverRequest devolverRequest)
        {
            try
            {
                await _jogoService.DevolverJogo(id, devolverRequest);
                return Ok(new { Mensagem = "Jogo atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                var response = new ErroResponse
                {
                    Mensagem = ex.Message
                };
                return BadRequest(response);
            }

        }
    }

}
