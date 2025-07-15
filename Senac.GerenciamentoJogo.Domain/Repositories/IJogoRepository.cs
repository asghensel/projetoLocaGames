
namespace Senac.GerenciamentoJogo.Domain.Repositories
{
    public interface IJogoRepository
    {
        Task<IEnumerable<object>> ObterTodos();
    }
}
