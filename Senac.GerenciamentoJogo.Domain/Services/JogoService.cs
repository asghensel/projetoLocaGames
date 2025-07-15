using Senac.GerenciamentoJogo.Domain.Dtos.Request;
using Senac.GerenciamentoJogo.Domain.Dtos.Response.Jogo;
using Senac.GerenciamentoJogo.Domain.Repositories;

namespace Senac.GerenciamentoJogo.Domain.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public Task AlugarJogo(long id, AlugarRequest alugarRequest)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarJogo(long id, AtualizarRequest atualizarRequest)
        {
            throw new NotImplementedException();
        }

        public Task<CadastrarResponse> CadastrarJogo(CadastrarRequest cadastrarRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeletarJogo(long id)
        {
            throw new NotImplementedException();
        }

        public Task DevolverJogo(long id, DevolverRequest devolverRequest)
        {
            throw new NotImplementedException();
        }

        public Task<DetalhesJogoResponse> ObterDetalhesJogo(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodosJogosResponse>> ObterTodos()
        {
            var jogos = await _jogoRepository.ObterTodos();
            var jogosResponse = jogos.Select(x => new TodosJogosResponse
            {
                Titulo = x.Titulo, 
                Id = x.Id,
            });
        }
    }
}
