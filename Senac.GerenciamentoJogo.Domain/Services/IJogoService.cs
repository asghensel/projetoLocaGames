using Senac.GerenciamentoJogo.Domain.Dtos.Request;
using Senac.GerenciamentoJogo.Domain.Dtos.Response.Jogo;

namespace Senac.GerenciamentoJogo.Domain.Services
{
    public interface IJogoService
    {
        Task AlugarJogo(long id, AlugarRequest alugarRequest);
        Task AtualizarJogo(long id, AtualizarRequest atualizarRequest);
        Task<CadastrarResponse> CadastrarJogo(CadastrarRequest cadastrarRequest);
        Task DeletarJogo(long id);
        Task DevolverJogo(long id, DevolverRequest devolverRequest);
        Task<DetalhesJogoResponse> ObterDetalhesJogo(long id);
        Task<IEnumerable<TodosJogosResponse>> ObterTodos();
    }
}
