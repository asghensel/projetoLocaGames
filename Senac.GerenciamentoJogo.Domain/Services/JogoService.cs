using System.ComponentModel;
using Senac.GerenciamentoJogo.Domain.Dtos.Request;
using Senac.GerenciamentoJogo.Domain.Dtos.Response.Jogo;
using Senac.GerenciamentoJogo.Domain.Models;
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

        public async Task AlugarJogo(long id, AlugarRequest alugarRequest)
        {
            var jogo = await _jogoRepository.ObterDetalhesJogo(id);
            ValidarSeExiste(jogo, id);
            if (!jogo.Disponivel)
                throw new Exception($"Jogo com ID {id} não está disponível para aluguel.");
            jogo.Disponivel = false;
            jogo.DataRetirada = CalcularDataEntrega(jogo.Categoria);
            jogo.Responsavel = alugarRequest.Responsavel;
            jogo.IsEmAtraso = false;

            await _jogoRepository.AlugarJogo(id, jogo);
        }

        public async Task AtualizarJogo(long id, AtualizarRequest atualizarRequest)
        {
            if (!Enum.TryParse(atualizarRequest.TipoCategoria, true, out TipoCategoria categoria))
                throw new ArgumentException($"Categoria inválida: {atualizarRequest.TipoCategoria}");
            var jogo = await _jogoRepository.ObterDetalhesJogo(id);
            ValidarSeExiste(jogo, id);

            jogo.Titulo = atualizarRequest.Titulo;
            jogo.Descricao = atualizarRequest.Descricao;
            jogo.Categoria = categoria;

            await _jogoRepository.AtualizarJogo(id, jogo);

        }

        public async Task<CadastrarResponse> CadastrarJogo(CadastrarRequest cadastrarRequest)
        {
            if(!Enum.TryParse(cadastrarRequest.TipoCategoria,true, out TipoCategoria categoria))
                throw new ArgumentException($"Categoria inválida: {cadastrarRequest.TipoCategoria}");
            
            var jogo = new Jogo
            {
                Titulo = cadastrarRequest.Titulo,
                Descricao = cadastrarRequest.Descricao,
                Disponivel = cadastrarRequest.Disponivel,
                Responsavel = cadastrarRequest.Responsavel,
                Categoria = categoria,
                DataRetirada = null,
                IsEmAtraso = false
            };
            long id = await _jogoRepository.CadastrarJogo(jogo);
            return new CadastrarResponse {Id = id } ;
        }

        public async Task DeletarJogo(long id)
        {
            var jogo = await _jogoRepository.ObterDetalhesJogo(id);
            if (jogo == null)
            {
                throw new Exception($"Jogo não encontrado com id {id}");
            }
            await _jogoRepository.DeletarJogo(id);
        }

        public async Task DevolverJogo(long id)
        {
            var jogo = await _jogoRepository.ObterDetalhesJogo(id);
            ValidarSeExiste(jogo, id);

            if (jogo.Disponivel)
                throw new Exception($"Jogo com ID {id} já está disponível para aluguel.");
            jogo.Disponivel = true;
            jogo.DataRetirada = null;
            jogo.Responsavel = null;
            jogo.IsEmAtraso = false;
            await _jogoRepository.DevolverJogo(id);
        }




        public async Task<DetalhesJogoResponse> ObterDetalhesJogo(long id)
        {
            var jogo = await _jogoRepository.ObterDetalhesJogo(id);
            if (jogo == null)
            {
                throw new Exception($"Jogo não encontrado com id {id}");
            }
            var jogoResponse = new DetalhesJogoResponse
            {
                Id = jogo.Id,
                Titulo = jogo.Titulo,
                Descricao = jogo.Descricao,
                TipoCategoria = jogo.Categoria.ToString(),
                Responsavel = jogo.Responsavel,
                DataRetirada = jogo.DataRetirada,
                Disponivel = jogo.Disponivel,
                IsEmAtraso = jogo.DataRetirada.HasValue && jogo.DataRetirada.Value < DateTime.Now

            };
            return jogoResponse;
        }

        public async Task<IEnumerable<TodosJogosResponse>> ObterTodos()
        {
            var jogos = await _jogoRepository.ObterTodos();
            var jogosResponse = jogos.Select(x => new TodosJogosResponse
            { Id = x.Id, Titulo = x.Titulo });

            return jogosResponse;
        }
        private void ValidarSeExiste(Jogo jogo, long id)
        {
            if (jogo == null)
                throw new Exception($"Jogo com ID {id} não encontrado.");
        }
        private DateTime CalcularDataEntrega(TipoCategoria categoria)
        {
            return categoria switch
            {
                TipoCategoria.Bronze => DateTime.Now.AddDays(9),
                TipoCategoria.Prata => DateTime.Now.AddDays(6),
                TipoCategoria.Ouro => DateTime.Now.AddDays(3),
                _ => throw new Exception("Categoria inválida.")
            };
        }
    }
}
