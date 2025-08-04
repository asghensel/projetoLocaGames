using Dapper;
using Senac.GerenciamentoJogo.Domain.Models;
using Senac.GerenciamentoJogo.Domain.Repositories;
using Senac.GerenciamentoJogo.Infra.Data.DataBaseConfigurations;

namespace Senac.GerenciamentoJogo.Infra.Data.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public JogoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AtualizarJogo(long id, Jogo jogo)
        {
            await _connectionFactory.CreateConnection()
             .QueryFirstOrDefaultAsync(
             @"
            UPDATE jogo
            SET
                titulo = @Titulo,
                descricao = @Descricao,
                categoria = @Categoria
            WHERE
                id = @Id
            ", jogo);
        }

        public async Task<long> CadastrarJogo(Jogo jogo)
        {

            var sql = @"
            INSERT INTO jogo
                (titulo, descricao, disponivel, categoria, responsavel, dataRetirada, isEmAtraso)
            OUTPUT INSERTED.id
            VALUES
                (@Titulo, @Descricao, @Disponivel, @Categoria, @Responsavel, @DataRetirada, @IsEmAtraso)
        ";

            var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleAsync<long>(sql, jogo);
        }


        public async Task DeletarJogo(long id)
        {
            {
                var sql = @"
            DELETE FROM Jogo
            WHERE id = @Id
        ";

                using var connection = _connectionFactory.CreateConnection();
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<Jogo> ObterDetalhesJogo(long id)
        {
            return await _connectionFactory.CreateConnection()
            .QueryFirstOrDefaultAsync<Jogo>(
            @"
            SELECT 
                j.id,
                j.titulo,
                j.descricao,
                j.disponivel,
                c.Id AS Categoria,
                j.responsavel,
                j.dataRetirada,
                j.isEmAtraso
            FROM Jogo j
            INNER JOIN TipoCategoria c ON c.Id = j.Categoria
            WHERE j.id = @Id
            ",
            new { Id = id }
            );
        }

        public async Task<IEnumerable<Jogo>> ObterTodos()
        {
            return await _connectionFactory.CreateConnection()
            .QueryAsync<Jogo>(
             @"
            SELECT 
                j.id,
                j.titulo,
                j.disponivel,
                c.Id AS Categoria,
                j.dataRetirada,
                j.isEmAtraso
            FROM Jogo j
            INNER JOIN TipoCategoria c ON c.Id = j.Categoria
             "
              );
        }

        public async Task AlugarJogo(long id, Jogo jogo)
        {
            await _connectionFactory.CreateConnection()
            .QueryFirstOrDefaultAsync(
               @"
            UPDATE Jogo
            SET
                responsavel = @Responsavel,
                disponivel = @Disponivel,
                dataRetirada = @DataRetirada
            WHERE
                Id = @Id
        ", jogo);
        }

        public async Task DevolverJogo(long id)
        {
            await _connectionFactory.CreateConnection()
            .QueryFirstOrDefaultAsync(
                @"
            UPDATE Jogo
            SET
                disponivel = 1,
                responsavel = NULL,
                dataRetirada = NULL
            WHERE
                Id = @Id
            ",
            new { Id = id }
            );
        }
    }
}
