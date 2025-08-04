
using Senac.GerenciamentoJogo.Domain.Models;

namespace Senac.GerenciamentoJogo.Domain.Repositories
{
    public interface IJogoRepository
    {
        Task AtualizarJogo(long id, Jogo jogo);
        Task<long> CadastrarJogo(Jogo jogo);
        Task DeletarJogo(long id);
        Task<Jogo> ObterDetalhesJogo(long id);
        Task<IEnumerable<Jogo>> ObterTodos();
        Task AlugarJogo(long id, Jogo jogo);
        Task DevolverJogo(long id);
    }
}
